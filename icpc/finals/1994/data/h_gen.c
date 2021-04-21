#include <stdio.h>
#include <stdlib.h>

#define SETS 10000

int random(int r) { return rand()%r; }

int main() {

int i,j,t,p;

printf("5\n");
printf("0.0     200.0   1.0     270\n");
printf("200.0   400.0   1.0     0\n");
printf("400.0   800.0   1.0     90\n");
printf("800.0   1000.0  1.0     180\n");
printf("1000.0  1200.0  1.0     270\n");
printf("1\n");
printf("0.0     500.0   1.0     0\n");
printf("1\n");
printf("0.0     500.0   1.0     90\n");
printf("2\n");
printf("0.0     100.0   1.0     0\n");
printf("100.0   500.0   1.0     180\n");
printf("1\n");
printf("0.0     500.0   1.0     280\n");

for(i=0; i<SETS; i++) {

	printf("%d\n",j=random(15)+1);
	printf("0.0     100.0   1.0     0\n");
	for(t=1000; j>1; j--) {

		p = t+random(200);
		printf("%-7.1f %-7.1f %-7.1f %d\n",(float)t/10,(float)p/10,(float)(random(99)+1)/10,random(360));
		t = p+random(1500);
	}
}
printf("0\n");
return 0;
}
