/*
		  Problem:      Package Pricing
		  Description:  satisfy 4D request with minimal cost
		  Class:        optimalization
		  Subclass:     discrete, 4D objects
		  Algorithm:    backtracking with restrains, exp
		  Author:       Petr Gregor
		  Date:         May 15, 1998

*/
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define N   51
#define INF 1e10

int n;
int kat[N],Skat[N],za[4],fp[N],ap[N];
int pac[N][4],S[4][N],M[4][N];
double fo,ac;
double pri[N],avg[4][N];


void FIND(int o, int g) {

	int z,t,i;

	if (ac >= fo) return;

	for(i=0; i<4; i++)
	 if (za[i] > 0) break;

	if (i == 4) {

		for(i=0; i<n; i++) fp[i] = ap[i];
		fo = ac; return;
	}

	for(i=0; i<4; i++)
	 if ((za[i]*avg[i][M[i][o]]) >= (fo-ac)) return;

	for(t=0,i=1; i<4; i++) t = (za[t] < za[i]) ? i : t;

	for(z=0; z<n; z++)
	 if (S[t][z] >= o) {

		for(i=0; i<4; i++)
		 if (za[i]*pac[S[t][z]][i]>0) break;

		if (i==4) continue;

		ac += pri[S[t][z]];
		ap[S[t][z]]++;
		for(i=0; i<4; i++) za[i] -= pac[S[t][z]][i];

		FIND(S[t][z],g+1);

		ac -= pri[S[t][z]];
		ap[S[t][z]]--;
		for(i=0; i<4; i++) za[i] += pac[S[t][z]][i];
	}
	return;
}

int main() {

int m,im,in,is,i,l;
char zn;

for(scanf("%d",&n),l=1; n!=0; scanf("%d",&n),l++) {

printf("Input set #%d:\n",l);

memset(pac,0,N*4*sizeof(int));
for(in=0; in<n; in++) {

	scanf("%d %lf",&kat[in],&pri[in]);
	for(scanf("%c",&zn); zn != '\n'; scanf("%c",&zn))
	 if (zn != ' ') scanf("%d",&pac[in][zn-'a']);

	for(i=0; i<4; i++) {

		avg[i][in] = (pac[in][i] > 0) ? (pri[in]/(float)pac[in][i]) : INF;
		for(S[i][is=in] = in; (is>0) && (avg[i][S[i][is-1]] > avg[i][S[i][is]]); is--) {

			m = S[i][is];
			S[i][is] = S[i][is-1];
			S[i][is-1] = m;
		}
	}
	for(Skat[is=in] = in; (is>0) && (kat[Skat[is-1]] > kat[Skat[is]]); is--) {

		m = Skat[is];
		Skat[is] = Skat[is-1];
		Skat[is-1] = m;
	}
}
for(i=0; i<4; i++)
 for(M[i][n-1]=in=n-1; in>0; in--)
  M[i][in-1] = (avg[i][in-1] <= avg[i][M[i][in]]) ? (in-1) : M[i][in];

scanf("%d\n",&m);
for(im=1; im<=m; im++) {

	memset(za,0,4*sizeof(int));
	for(scanf("%c",&zn); zn != '\n'; scanf("%c",&zn))
	 if (zn != ' ') { scanf("%d",&is); za[zn-'a'] += is; }

	fo = INF; memset(fp,0,N*sizeof(int));
	ac = 0; memset(ap,0,N*sizeof(int));

	FIND(0,0);

	printf("%d: %7.2f",im,fo);
	for(in=0; in<n; in++)
	 if (fp[Skat[in]] > 0) {

		printf(" %d",kat[Skat[in]]);
		if (fp[Skat[in]] > 1) printf("(%d)",fp[Skat[in]]);
	 }
	printf("\n");
}

}

return 0;
}
