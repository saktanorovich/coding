/*
   Judge
   
   Problem:   Switching channels
   Author:    Petr Gregor
   Date:      summer 1998
*/
 
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define INFILE  "../problems/e.in"
#define OUTFILE "../problems/e.out"

#define NL 6
#define NP 9
#define NT 9

int nt,np;
int po[NP],le[NP];
int to[NT],eo[NT];
int ti[NT],ei[NT];

int CompErr(int t[NT], int e[NL]) {

	int i,j,st,er;

	memset(e,0,NL*sizeof(int));
	for(i=j=st=0; i<np; i++) {
		for(;(st<po[i]) && (j<nt); st+=t[j++]);
		if (j==0) e[le[i]] += st-po[i];
		else if (st<po[i]) e[le[i]] += po[i]-st;
		else e[le[i]] += ((st-po[i]) < (po[i]-st+t[j-1])) ? (st-po[i]) : (po[i]-st+t[j-1]);
	}

	for(i=er=0; i<NL; i++) er += e[i];

	return er;
}

int main(int argc, char *argv[]) {

FILE *fin,*f;
int c,d,i,l,m,er,ed;

if ((f=fopen(OUTFILE,"r")) == NULL) {

	fprintf(stderr,"Nelze otevrit soubor %s.\n",INFILE);
	return -1;
}

if ((fin=fopen(INFILE,"r")) == NULL) {

	fprintf(stderr,"Nelze otevrit soubor %s.\n",INFILE);
	return -1;
}
if (freopen(argv[1], "r", stdin) == NULL) {
	fprintf(stderr,"Nelze otevrit vstup %s.\n",argv[1]);
	return -1;
}

for(fscanf(fin,"%d",&nt); nt!=0; fscanf(fin,"%d",&nt)) {

	for(i=0; i<nt; i++) fscanf(fin,"%d",&d);

	fscanf(fin,"%d",&np);
	for(i=0; i<np; i++) {

		fscanf(fin,"%d",&le[i]);
		fscanf(fin,"%d",&po[i]);
		for(l=i; l>0; l--)
		 if (po[l-1] > po[l]) {
			m = po[l-1]; po[l-1] = po[l]; po[l] = m;
			m = le[l-1]; le[l-1] = le[l]; le[l] = m;
		}
	}

	scanf("Data set %d\n",&c);
	scanf("Order:");
	for(i=0; i<nt; i++) scanf("%d",&to[i]);
	er = CompErr(to,eo);
	scanf("\nError: %d\n",&ed);

	fscanf(f,"Data set %d\n",&d);
	fscanf(f,"Order:");
	for(i=0; i<nt; i++) fscanf(f,"%d",&ti[i]);
	er = CompErr(ti,ei);
	fscanf(f,"\nError: %d\n",&ed);

	if ((c!=d) || (er != ed)) {

		printf("Wrong Answer\n");
		fclose(fin); 
		return 1;
	}

	for(i=0; i<NL; i++) {

		if (ei[i] < eo[i]) break;           /* vzor neni optimalni !!!*/		if (ei[i] > eo[i]) {

			printf("Wrong Answer\n");
			fclose(fin); 
			return 1;
		}
	}
}
printf("Accepted\n");
fclose(fin);  fclose(f);
return 0;
}
