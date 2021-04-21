/* 
    Input generator
    
    Problem:   Switching channels
    Author:    Petr Gregor
    Date:      summer 1998
*/

#include <stdio.h>
#include <stdlib.h>

#define SET 1000

int random(int r) { return rand()%r; }

int main() {

int i,j,k,n,p,a;
int x[8];

for(p=1; p<=8; p++) {

	a = p;
	printf("%d",p);
	for(i=0; i<p; i++) printf(" %d",x[i] = random(20)*4+5);
	printf("\n%d",a);
	for(j=n=0; j<a; j++) {
		for(k=random(p); x[k]==0; k++)
		 if (k == p-1) k=-1;
		n += x[k]; x[k]=0;
		printf(" %d %d",random(5)+1,n);
	}
	printf("\n");
}

for(j=0; j<SET; j++) {

	p = random(8)+1;
	a = random(9);
	printf("%d",p);
	for(i=0; i<p; i++) printf(" %d",random(20)*4+5);
	printf("\n%d",a);
	for(i=n=0; i<a; i++) x[i]=(n+=random(15)*3+5);
	for(i=0; i<a; i++) {
		for(k=random(a); x[k]==0; k++)
		 if (k == a-1) k=-1;
		printf(" %d %d",random(5)+1,x[k]);
		x[k]=0;
	}
	printf("\n");

}
printf("0\n");
return 0;
}
