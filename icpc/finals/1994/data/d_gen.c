/*
    Input generator
	
    Problem:  Package Pricing
    Author:   Petr Gregor
    Date:     summer 1998
*/

#include <stdio.h>
#include <stdlib.h>

#include <time.h>
#define SETS 0

int random(int r) { return rand()%r; }

void inputset(int n) {

	int i,j,o,k,m,c,p[4],l[4];
	float r;

	printf("%d\n",n);
	p[0]=p[1]=p[2]=p[3]=0;
	for(i=c=0; i<n; i++) {

		l[0]=l[1]=l[2]=l[3]=0;
		l[k=random(4)] += 1+random(10);
		for(j=random(5); j>0; j--) l[random(4)] += 1+random(7);

		r=(float)(l[0]+l[1]+l[2]+l[3])*(random(5)+10)+(float)random(1000)/100;

		c+=random(100)+1;

		printf("%d %.2f",c,r);
		for(j=0; j<4; j++)
		 if (l[j]>0) { p[j]=1; printf(" %c %d",'a'+j,l[j]); }
		printf("\n");
	}

	m = random(n)+5;
	printf("%d\n",m);
	for(; m>0; m--) {

		printf("%c %d",'a'+k,random(20)+1);
		for(j=0,i=random(4*2); j<i; j++)
		 if (p[o=random(4)] == 1) { printf(" %c %d",'a'+o,random(20)+1); k=o; }
		printf("\n");
	}
}

int main() {

int n,i;

srand(time(NULL));
for(n=1; n<=20; n++)  inputset(n);
for(i=0; i<SETS; i++) inputset(random(15)+2);

printf("0\n");
return 0;
}
