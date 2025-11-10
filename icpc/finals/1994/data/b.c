/*
   Problem:     Testing the CATCHER
   Description: find number of missiles that can be intercepted 
   Class:       combinatorics
   Subclass:    sequences
   Algorithm:   find max length of falling subsesequence,O(n^2)
   Author:      Michal Kicmer
   Date:        spring 1998
*/

#include <stdlib.h>
#include <stdio.h>

#define N	0xffff
#define NoSuc	-1

int main() {
 int nums[N];
 int lens[N];
 int sucs[N];
 int i=0, j, k;
 int total, result;
 int probcnt=0;

 while (1) {
  scanf("%d", &i);
  if (i < 0) break;
  if (probcnt) printf("\n");

  for (k=0; i>=0; k++) {
   nums[k] = i;
   sucs[k] = NoSuc;
   scanf("%d", &i);
  }
  total = k;
  for (i = total-1; i >= 0; i--) {
   int max = 0;
   int suc = NoSuc;
   for (j = i+1; j<total; j++)
    if (nums[j] < nums[i])
    if (lens[j] > max) {
     max = lens[j];
     suc = j;
    }
   lens[i] = max+1;
   sucs[i] = suc;
  }
  result = 0;
  for (i = total-1; i >= 0; i--) {
    if (result < lens[i])
      result = lens[i];
  }
  probcnt++;
  printf("Test #%d:\n  maximum possible interceptions: %d\n", probcnt, result);
 }
 return 0;
}
