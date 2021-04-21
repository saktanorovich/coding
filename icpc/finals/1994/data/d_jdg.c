/*
      Judge
      
      Problem:   Package Pricing
      Author:    Petr Gregor
      Date:      summer 1998
*/

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define INFILE  "../problems/d.in"
#define OUTFILE "../problems/d.out"
#define N     51
#define ZAKR  1e-4

int main(int argc, char *argv[]) {

FILE *fin,*fok;
int c,d,i,is,to,ti,in,ka,po,m,im,n;
int kat[N],za[4],pac[N][4];
double pri[N];
double eo,ei;
char zn;

if ((fin=fopen(INFILE,"r")) == NULL) {

	fprintf(stderr,"Nelze otevrit soubor %s.\n",INFILE);
	return -1;
}
if ((fok=fopen(OUTFILE,"r")) == NULL) {

	fprintf(stderr,"Nelze otevrit soubor %s.\n",OUTFILE);
	fclose(fin);
	return -1;
}
if (freopen(argv[1], "r", stdin) == NULL) {
	fprintf(stderr,"Nelze otevrit vstup %s.\n",argv[1]);
	return -1;
}

for(fscanf(fin,"%d",&n); n!=0; fscanf(fin,"%d",&n)) {

	fscanf(fok,"Input set #%d:\n",&c);
	scanf(  "Input set #%d:\n",&d);

	if (c!=d) {

		printf("Wrong Answer\n");
		fclose(fin); fclose(fok); 
		return 1;
	}

	memset(pac,0,N*4*sizeof(int));
	for(in=0; in<n; in++) {

		fscanf(fin,"%d %lf",&kat[in],&pri[in]);
		for(fscanf(fin,"%c",&zn); zn != '\n'; fscanf(fin,"%c",&zn))
		 if (zn != ' ') fscanf(fin,"%d",&pac[in][zn-'a']);
	}

	fscanf(fin,"%d\n",&m);
	for(im=1; im<=m; im++) {

		memset(za,0,4*sizeof(int));
		for(fscanf(fin,"%c",&zn); zn != '\n'; fscanf(fin,"%c",&zn))
		 if (zn != ' ') { fscanf(fin,"%d",&is); za[zn-'a'] += is; }

		fscanf(fok,"%d: %lf",&to,&eo);
		scanf(  "%d: %lf",&ti,&ei);

		if ((to!=ti) || (ei>eo)) {

			printf("Wrong Answer\n");
			fclose(fin); fclose(fok); 
			return 1;
		}

		for(fscanf(fok,"%c",&zn); zn!='\n';) {

			fscanf(fok,"%d%c",&ka,&zn);
			if (zn=='(') fscanf(fok,"%d)%c",&po,&zn);
			else po=1;
		}

		for(scanf("%c",&zn); zn!='\n';) {

			scanf("%d%c",&ka,&zn);
			if (zn=='(') scanf("%d)%c",&po,&zn);
			else po=1;

			for(i=0; i<n; i++)
			 if (ka == kat[i]) break;

			if (i==n) {

				printf("Wrong Answer\n");
				fclose(fin); fclose(fok); 
				return 1;
			}
			ei -= pri[i]*(double)po;
			for(is=0; is<4; is++) za[is] -= pac[i][is]*po;
		}

		for(is=0; is<4; is++)
		if (za[is] > 0) {

			printf("Wrong Answer\n");
			fclose(fin); fclose(fok); 
			return 1;
		}

		if (abs(ei) > ZAKR) {

			printf("Wrong Answer\n");
			fclose(fin); fclose(fok); 
			return 1;
		}
	}
}
printf("Accepted\n");
fclose(fin); fclose(fok); 
return 0;
}
