/*
  Input generator

  Problem:  Testing the CATCHER
  Author:   Michal Kicmer
  Date:     spring 1998
*/

#include <stdio.h>
#include <stdlib.h>

#define MAX	0x7fff
#define N 	3

int sizes[N] = { 1, 0x100, 0xfff };

int main() {
 int i, j;
 for (j=0; j<N; j++) {
  for (i=0; i<sizes[j]; i++) printf("%d\n", rand() & MAX);
  printf("-1\n");
 }
 printf("-1\n");
 return 0; 
}
