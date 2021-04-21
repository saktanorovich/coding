/*
		  Problem:      Switching Channels
		  Description:  reorder programmes to minimize alingment error
		  Class:        optimalization
		  Subclass:     discrete
		  Algorithm:    backtracking with restrains, exp
		  Author:       Petr Gregor
		  Date:         May 15, 1998
*/

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define NL 6
#define NP 9
#define NT 9

#define MOC 32000

int po[NP],le[NP],t[NT],nt,np;

int s[NT],el[NL],pel[NL];

int l,p1[NP],p2[NP],l2[NP],np1,np2;

int Better(int dt[NT]) {

	int i,j,st;

	memset(pel,0,NL*sizeof(int));
	for(i=j=st=0; i<np2; i++) {
		for(;st<p2[i]; st+=t[dt[j++]]);
		pel[l2[i]] += ((st-p2[i]) < (p2[i]-st+t[dt[j-1]])) ? (st-p2[i]) : (p2[i]-st+t[dt[j-1]]);
	}
	for(i=l+1; i<NP; i++)
	 if (pel[i] != el[i]) return (pel[i] < el[i]);
	return 0;
}

void MinusError(int *e, int *npo, int st, int ca) {

	for(; (*npo > 0) && (st < p1[*npo-1]); (*npo)--)
	 *e -= ((st+ca-p1[*npo-1]) < (p1[*npo-1]-st)) ? (st+ca-p1[*npo-1]) : (p1[*npo-1]-st);
	return;
}

void PlusError(int *e, int *npo, int st, int ca) {

	for(; (*npo < np1) && (st >= p1[*npo]); (*npo)++)
	 *e += ((st-p1[*npo]) < (p1[*npo]-st+ca)) ? (st-p1[*npo]) : (p1[*npo]-st+ca);
	return;
}

void Find(void) {

	int i,j,npo,st,e,f[NP],to[NP];

	for(i=l; i<NL; i++) el[i] = MOC;
	for(i=0; i<nt; i++) { f[i] = 1; s[i] = i; }

	to[st=e=i=npo=0] = -1;
	while(i != -1) {

		if (e > el[l]) {
			if (to[i] >= 0) {
				st -= t[to[i]]; f[to[i]]=1;
				MinusError(&e,&npo,st,t[to[i]]);
			}
			i--; continue;
		}

		if (i == nt) {
			if ((e<el[l]) || ((e==el[l]) && (Better(to)))) {
				Better(to);
				for(j=0; j<nt; j++) s[j] = to[j];
				for(j=0,pel[l]=e; j<NL; j++) el[j] = pel[j];
			}
			i--; continue;
		}

		for(j=to[i]+1; j < nt && !f[j]; j++);

		if (j == nt) {
			st -= t[to[i]]; f[to[i]]=1;
			MinusError(&e,&npo,st,t[to[i]]);
			i--; continue;
		}
		if (to[i] >= 0) {
			st -= t[to[i]]; f[to[i]]=1;
			MinusError(&e,&npo,st,t[to[i]]);
		}
		to[i]=j; st += t[to[i]]; f[to[i]]=0;
		PlusError(&e,&npo,st,t[to[i]]);
		to[++i] = -1;
	}

	return;
}

int main() {

int i,c,m,st,err;

for(c=1,scanf("%d",&nt); nt!=0; scanf("%d",&nt),c++) {

	for(st=i=err=0; i<nt; i++) {
		scanf("%d",&t[i]); st+=t[i];
		for(l=i; l>0; l--)
		 if (t[l-1] > t[l]) { m = t[l-1]; t[l-1] = t[l]; t[l] = m; }
	}

	scanf("%d",&np);
	for(i=0; i<np; i++) {

		scanf("%d",&le[i]);
		scanf("%d",&po[i]);
		if (po[i] == 0) { np--; i--; continue; }
		if (po[i] >= st) { err += po[i]-st; np--; i--; continue; }
		for(l=i; l>0; l--)
		 if (po[l-1] > po[l]) {
			m = po[l-1]; po[l-1] = po[l]; po[l] = m;
			m = le[l-1]; le[l-1] = le[l]; le[l] = m;
		}
	}
	for(i=0,l=NP; i<np; i++) l = (le[i] < l) ? le[i] : l;
	for(i=np1=np2=0; i<np; i++)
	 if (le[i] == l) p1[np1++] = po[i];
	 else { l2[np2] = le[i]; p2[np2++] = po[i]; }

	Find();

	printf("Data set %d\nOrder:",c);
	for(i=0; i<nt; i++) printf(" %d",t[s[i]]);
	for(i=l; i<NL; i++) err += el[i];

	printf("\nError: %d\n",err);
}

return 0;
}
