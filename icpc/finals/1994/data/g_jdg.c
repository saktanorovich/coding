/*
	Judge
	
	Problem:	VTAS - Vessel Trafic Advisory System
	Author:		Michal Kicmer
	Date:		spring 1998
	Note:           not working
*/

#include <stdlib.h>
#include <string.h>
#include <stdio.h>

#define MAXVES	35

int passings;
int tpass=0;
int tenc=0;
int encounters;
int vess=0;
int invalids=0;
int tinv=0;

int vesscnt;

typedef struct n {
 struct n *l, *r;
 char *c;
} node;

void add(node *tree, int *arr, char a, char b) {
 node **where = &tree;
 int i;
 for (i = 0; i< MAXVES; i++, arr++) {
  if (*arr) {	/* pridej doprava */
   if ((*where)->r) {
    *where = (*where)->r;
    continue;
   } else {
    (*where)->r = (node *) calloc(1, sizeof(node));
    *where = (*where)->r;
    (*where)->c = NULL;
    (*where)->l = (*where)->r = NULL;
    continue;
   }
  } else {	/* pridej doleva */
   if ((*where)->l) {
    *where = (*where)->l;
    continue;
   } else {
    (*where)->l = (node *) calloc(1, sizeof(node));
    *where = (*where)->l;
    (*where)->c = NULL;
    (*where)->l = (*where)->r = NULL;
    continue;
   }
  }
 }
 if (!(*where)->c) {
   (*where)->c = (char *) calloc(24, sizeof(char));
   for (i=0; i<24; i++) (*where)->c[i] = 0;
 }
 for (i=0; i<24; i+=2)
  if (!(*where)->c[i]) { (*where)->c[i] = a; (*where)->c[i+1] = b; break; }
  /* kvuli nasobnemu potkavani dvou stejnych lodi */
}

int find(node *tree, int *arr, char aa, char bb) {
 int i;
 for (i=0; i<MAXVES; i++, arr++) {
  if (!tree) return 0;
  if (*arr) tree = tree->r;
  else tree = tree->l;
 }
 if (tree->c)
  for (i=0; i<24; i+=2)
   if ((tree->c[i] == aa) && (tree->c[i+1] == bb)) return 1;
 return 0;
}

void del(node *tree) {
 if (tree->l) del(tree->l);
 if (tree->r) del(tree->r);
 free(tree);
}

void destroy(node *tree) {
 if (tree->l) del(tree->l);
 if (tree->r) del(tree->r);
}

typedef struct {
  char name[21];
  float f;
  int dep;
  int len;
  int index;
} vesentry;

vesentry vessels[MAXVES];

#define	MAXTAB	400

int cnt=0;
char *tab[MAXTAB];

#define MAXENC	200
#define MAXPASS	200

char buf[200], line[200], errmsg[400];
node passtree = { NULL, NULL };
node enctree = { NULL, NULL };

void Stop(int exitcode, char *msg) {

  int i;
  for (i=0; i<cnt; i++) free(tab[i]);

  destroy(&passtree);
  destroy(&enctree);

  if (msg) printf("%s\n", msg);
  exit(exitcode);
}

#define cp1	"** Warning ** Close passing of "
#define cp2	" with "
#define cp3	" at Waypoint "

void ClosePassing() {
 int len;
 char f, t;
 vesentry *ves;
 int vesflg[MAXVES];
 for (len = 0; len<MAXVES; len++) vesflg[len] = 0;
 strcpy(line, buf);

 if (strncmp(line, cp1, 31)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 1st field does not match with <%s>. (this %d)\n", buf, cp1, tpass+1);
  Stop(-1, errmsg);
 }
 len = 31;
 f = line[len]-'A';
 vesflg[f] = 1;

 ves = vessels+f;
 if (strncmp(line+len, ves->name, ves->len)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 1st vessel name does not match with <%s>. (this %d)\n", buf, ves->name, tpass+1);
  Stop(-1, errmsg);
 }
 len += ves->len;
 if (strncmp(line+len, cp2, 6)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 2nd field does not match with <%s>. (this %d)\n", buf, cp2, tpass+1);
  Stop(-1, errmsg);
 }
 len +=6;
 f = line[len]-'A';
 if (vesflg[f]) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Vessel <%s> is twice in this line. (this %d)\n", buf, ves->name, tpass+1);
  Stop(-1, errmsg);
 }
 vesflg[f] = 1;
 ves = vessels+f;
 if (strncmp(line+len, ves->name, ves->len)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 2nd vessel name does not match with <%s>. (this %d)\n", buf, ves->name, tpass+1);
  Stop(-1, errmsg);
 }
 len += ves->len;
 while (1) {
  if (strncmp(line+len, cp2, 6)) break;
  len += 6;
  f = line[len]-'A';
  if (vesflg[f]) {
   sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Vessel <%s> is twice in this line. (this %d)\n", buf, ves->name, tpass+1);
   Stop(-1, errmsg);
  }
  vesflg[f] = 1;
  ves = vessels+f;
  if (strncmp(line+len, ves->name, ves->len)) {
   sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Xth vessel name does not match with <%s>. (this %d)\n", buf, ves->name, tpass+1);
   Stop(-1, errmsg);
  }
  len += ves->len;
 }
 if (strncmp(line+len, cp3, 13)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 3rd field does not match with <%s>. (this %d)\n", buf, cp3, tpass+1);
  Stop(-1, errmsg);
 }
 len += 13;
 f = line[len];
 len++;
 if (line[len]) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Some extra characters at the end <%s>. (this %d)\n", buf, line+len, tpass+1);
  Stop(-1, errmsg);
 }	/* neni konec radky */
 if (!find(&passtree, vesflg, f, 0)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: This passing is not known. (this %d)\n", buf, tpass+1);
  Stop(-1, errmsg);
 }
 tpass++;
 printf("Close passing #%d ok.\n", tpass); 
 if (!ExpectEnd()) {
  if (!gets(line) && passings) {
   sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Unexpected End of file found at the end of line. (this %d)\n", buf, tpass+1);
   Stop(-1, errmsg);
  }
  if (line[0]) {
   sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Line does not end with new-line. (this %d)\n", buf, tpass+1);
   Stop(-1, errmsg);
  }
 }
}

#define ent1	"entering system at "
#define ent2	" with a planned speed of "
#define ent3	"knots"
#define ent4	"**> Invalid Route Plan for Vessel: "

void Entering() {
 char *tmp = line;
 char *name = line;
 int len, v;
 char time[5];
 float f;
 strcpy(line, buf);
 tmp = strchr(tmp, 32);
 *tmp = 0;
 tmp++;
 if (strncmp(tmp, ent1, 19)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 1st field does not match with <%s>. (this %d)\n", buf, ent1, vess+1);
  Stop(-1, errmsg);
 }
 v = name[0]-'A';
 sprintf(time, "%04d", vessels[v].dep);
 if (strcmp(name, vessels[v].name)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Vessel name does not match with <%s>. (this %d)\n", buf, vessels[v].name, vess+1);
  Stop(-1, errmsg);
 }
 len = 19+vessels[v].len+1;         /* za mezeru za jmenem */
 if (strncmp(line+len, time, 4)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Vessel departure does not match vith <%s>. (this %d)\n", buf, time, vess+1);
  Stop(-1, errmsg);
 }
 len += 4;
 if (strncmp(line+len, ent2, 25)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 2nd field does not match with <%s>. (this %d)\n", buf, ent2, vess+1);
  Stop(-1, errmsg);
 }
 len += 25;
 if (1 != sscanf(line+len, "%e", &f)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Error occured when reading vessel speed. (this %d)\n", buf, vess+1);
  Stop(-1, errmsg);
 }
 if (f != vessels[v].f) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Vessel speed does not match vith <%e>. (this %d)\n", buf, f, vess+1);
  Stop(-1, errmsg);
 }
 tmp = strchr(line+len, 32);
 tmp++;
 if (strncmp(tmp, ent3, 5)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 3rd field does not match with <%s>. (this %d)\n", buf, ent3, vess+1);
  Stop(-1, errmsg);
 }
 if (*(tmp+5)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Line does not end with new-line. (this %d)\n", buf, vess+1);
  Stop(-1, errmsg);
 }
 if (!gets(line)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: End of file found at the end of line. (this %d)\n", buf, vess+1);
  Stop(-1, errmsg);
 }
 len = vessels[v].index;
 if (len<0) {
  if (strncmp(ent4, line, 35)) {
   sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 4th field does not match with <%s>. (this %d, invalid %d)\n", buf, ent3, vess+1, tinv+1);
   Stop(-1, errmsg);
  }
  if (strncmp(vessels[v].name, line+35, vessels[v].len)) {
   sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Vessel name in invalid path does not match with <%s>. (this %d, invalid %d)\n", buf, vessels[v].name, vess+1, tinv+1);
   Stop(-1, errmsg);
  }
  if (*(line+35+vessels[v].len)) {
   sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Line does not end with new-line. (this %d, invalid %d)\n", buf, vess+1, tinv+1);
   Stop(-1, errmsg);
  }
  tinv++;
 } else {
  if (strcmp(tab[len], line)) {
   sprintf(errmsg, "ERROR after line <%s>\nMESSAGE: Title of timetable does not match. (this %d)\nSample:<%s>\nInput: <%s>\n", buf, vess+1,/* tinv+1,*/ tab[len], line);
   Stop(-1, errmsg);
  }
  if (!gets(line)) {
   sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: End of file found at the end of line. (this %d, invalid %d)\n", buf, vess+1, tinv+1);
   Stop(-1, errmsg);
  }
  if (strcmp(tab[len+1], line)) {
   sprintf(errmsg, "ERROR after line <%s>\nMESSAGE: Timetable does not match. (this %d)\nSample:<%s>\nInput: <%s>\n", buf, vess+1, /*tinv+1,*/ tab[len+1], line);
   Stop(-1, errmsg);
  }
 }
 vess++;
 printf("Entering #%d ok.\n", vess);
 if (!ExpectEnd()) {
  if (!gets(line)) {
   sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Unexpected End of file found at the end of line. (this %d)\n", buf, vess+1);
   Stop(-1, errmsg);
  }
  if (line[0]) {
   sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Timetable does not follow empty line. (this %d)\n", buf, vess+1);
   Stop(-1, errmsg);
  }
 }
}

#define en1	"Projected encounter of "
#define en2	" with "
#define en3	" on leg between Waypoints "
#define en4     " and "

void Encounter() {
 int len;
 char f, s, t;
 vesentry *ves;
 int vesflg[MAXVES];
 for (len = 0; len<MAXVES; len++) vesflg[len] = 0;
 strcpy(line, buf);

 if (strncmp(line, en1, 23)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 1st field does not match with <%s>. (this %d)\n", buf, en1, vess+1);
  Stop(-1, errmsg);
 }
 len = 23;
 f = line[len]-'A';
 vesflg[f] = 1;

 ves = vessels+f;
 if (strncmp(line+len, ves->name, ves->len)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 1st vessel name does not match with <%s>. (this %d)\n", buf, ves->name, tpass+1);
  Stop(-1, errmsg);
 }
 len += ves->len;
 if (strncmp(line+len, en2, 6)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 2nd field does not match with <%s>. (this %d)\n", buf, en2, tpass+1);
  Stop(-1, errmsg);
 }
 len +=6;
 f = line[len]-'A';
 if (vesflg[f]) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Vessel <%s> is twice in this line. (this %d)\n", buf, ves->name, tpass+1);
  Stop(-1, errmsg);
 }
 vesflg[f] = 1;
 ves = vessels+f;
 if (strncmp(line+len, ves->name, ves->len)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 2nd vessel name does not match with <%s>. (this %d)\n", buf, ves->name, tpass+1);
  Stop(-1, errmsg);
 }
 len += ves->len;
 while (1) {
  if (strncmp(line+len, en2, 6)) break;
  len += 6;
  f = line[len]-'A';
  if (vesflg[f]) {
   sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Vessel <%s> is twice in this line. (this %d)\n", buf, ves->name, tpass+1);
   Stop(-1, errmsg);
  }
  vesflg[f] = 1;
  ves = vessels+f;
  if (strncmp(line+len, ves->name, ves->len)) {
   sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Xth vessel name does not match with <%s>. (this %d)\n", buf, ves->name, tpass+1);
   Stop(-1, errmsg);
  }
  len += ves->len;
 }
 if (strncmp(line+len, en3, 26)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 3rd field does not match with <%s>. (this %d)\n", buf, en3, tpass+1);
  Stop(-1, errmsg);
 }
 len += 26;
 f = line[len];
 len++;
 if (strncmp(line+len, en4, 5)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: 4th field does not match with <%s>. (this %d)\n", buf, en4, tpass+1);
  Stop(-1, errmsg);
 }
 len += 5;
 s = line[len];
 len++;
 if (line[len]) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Some extra characters at the end <%s>. (this %d)\n", buf, line+len, tpass+1);
  Stop(-1, errmsg);
 }	/* neni konec radky */
 if (f < s) { t = f; f = s; s = t; }
 if (!find(&enctree, vesflg, f, s)) {
  sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: This encounter is not known. (this %d)\n", buf, tpass+1);
  Stop(-1, errmsg);
 }
 tenc++;
 printf("Encounter #%d ok.\n", tenc);
 if (!ExpectEnd()) {
  if (!gets(line) && encounters) {
   sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Unexpected End of file found at the end of line. (this %d)\n", buf, tpass+1);
   Stop(-1, errmsg);
  }
  if (line[0]) {
   sprintf(errmsg, "ERROR in line <%s>\nMESSAGE: Line does not end with new-line. (this %d)\n", buf, tpass+1);
   Stop(-1, errmsg);
  }
 }
}

int ExpectEnd() {
 if (passings+encounters+vesscnt == vess+tenc+tpass) {
  printf("WARNING: Expected end of file reached.\n");
  return 1;
 }
 return 0;
}

int main(int argc, char **argv) {
 FILE *f;
 int i, j;
 int vesflg[MAXVES];
 char a, b;
 char *tmp;
 float fl;

 if (argc < 2) {
  printf("USAGE: <file with testdata>\n");
  return -1;
 }
 if (!(f = fopen(argv[1], "r"))) {
  printf("ERROR: Cannot open file with test data.\n");
  return -1;
 }

 if (!f) {
  printf("Error openning sample input.\n");
  exit(-1);
 }
 fscanf(f, " %d", &vesscnt);
 vess = vesscnt;
 for(i=0; i<vess; i++) {
  fscanf(f, " %s", line);
  j = line[0]-'A';
  strcpy(vessels[j].name, line);
  fscanf(f, " %d %e", &vessels[j].dep, &fl);
  vessels[j].f = fl;
  vessels[j].len = strlen(vessels[j].name);
  fgets(buf, 200, f);	/* because of newbuf */
  fgets(buf, 200, f);
  if (buf[0] == '*') {
   vessels[j].index = -1;
   invalids++;
  } else {
   vessels[j].index = cnt;
   tab[cnt] = (char*)strdup(buf);
   tab[cnt][strlen(tab[cnt])-1] = 0;
   fgets(buf, 200, f);
   cnt++;
   tab[cnt] = (char*)strdup(buf);
   tab[cnt][strlen(tab[cnt])-1] = 0;
   cnt++;
  }
 }
 vess=0;
 fscanf(f, " %d", &encounters);
 fgets(buf, 200, f);	/* because of newbuf */
 for (i=0; i< encounters; i++) {
  for (j=0; j< MAXVES; j++) vesflg[j] = 0;
  fgets(buf, 200, f);
  a = buf[0];
  b = buf[2];
  tmp = buf;
  tmp += 4;
  while (tmp) {
   vesflg[tmp[0]-'A'] = 1;
   if (tmp == strchr(tmp, 32)) tmp++;
  }
  add(&enctree, vesflg, a, b);
 }
 fscanf(f, " %d", &passings);
 fgets(buf, 200, f);	/* because of newbuf */
 for (i=0; i< passings; i++) {
  for (j=0; j< MAXVES; j++) vesflg[j] = 0;
  fgets(buf, 200, f);
  a = buf[0];
  tmp = buf;
  tmp += 2;
  while (tmp) {
   vesflg[tmp[0]-'A'] = 1;
   if (tmp == strchr(tmp, 32)) tmp++;
  }
  add(&passtree, vesflg, a, 0);
 }

 fclose(f);

 i = 0;
 while (gets(buf)) {
  switch (buf[0]) {
  case '*': ClosePassing(); break;
  case 'P': Encounter(); break;
  default: Entering(); break;
  }
 }

 if (encounters !=tenc) {
   sprintf(buf, "Error: The read (%d) and expected (%d) numbers of encounters differ.\n", tenc, encounters);
   Stop(-1, errmsg);
 }
 if (passings != tpass) {
   sprintf(buf, "Error: The read (%d) and expected (%d) numbers of passings differ.\n", tpass, passings);
   Stop(-1, errmsg);
 }
 if (vess != vesscnt) {
   sprintf(buf, "Error: The read (%d) and expected (%d) numbers of vessels differ.\n", vess, vesscnt);
   Stop(-1, errmsg);
 }
 if (invalids !=tinv) {
   sprintf(buf, "Error: The read (%d) and expected (%d) numbers of invalid routes differ.\n", tinv, invalids);
   Stop(-1, errmsg);
 }
 Stop(0, "SUCCESS: Test ran without any error.");

 return 0;
}

/*
  chyba unexpected end of file muze nasta kdyz je tam vice polozek nez ma,
  pak ExpectEnd vraci 0 a on ocekava prazdny radek, ktery za polsedni polozkou neni

  chyba 1st field does not match nastane kduz je tam vice polozek, ale hned
  u prvni nadbytecne, protoze za predchozi necetl prazdny radek a tak ho nacetl ted
*/
