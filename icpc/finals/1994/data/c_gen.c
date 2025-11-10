/* 
   Input generator
   
   Problem:  Crosword Answers
   Author:   Michal Kicmer
   Date:     summer 1998
*/

#include <stdio.h>
#include <stdlib.h>

/*
 * tabulka
 */
int p[100];

char randch()
{
  return rand()%('Z' - 'A') + 'A'; 
} /* randch */

void printx(int r, int c)
{
 int rr,cc;
 printf("%d %d\n", r, c);
 for(rr=r; rr--;)
 {
   for(cc=c; cc--;)
   {
     printf("%c", p[cc + rr *c]);
   }
   printf("\n");
 }
} /* printx */

void sachy(int r, int c)
{
  int cc,rr;
  for(rr= r;rr--;)
  {
    for(cc=c; cc--;)
    {
      p[cc + rr*c]= (rr+cc)&1?randch():'*';
    }
  }
  printx(r,c);
} /* sachy */

void nahodne(int r, int c)
{
  int i;
  for(i= r*c;i--;)
  {
    p[i]= rand()%5?randch():'*';
  }
  printx(r,c);
} /* nahodne */

void plne(int r, int  c)
{
  int i;
  for(i= r*c; i--;)
  {
    p[i]='*';
  }
  printx(r,c);
}

void prazdne(int r, int c)
{
  int i;
  for(i= r*c; i--;)
  {
    p[i]=randch();
  }
  printx(r,c);
}


#define VSE(r,c) {sachy(r,c); nahodne(r,c); plne(r,c); prazdne(r,c);}
int main()
{
  int r,c;
/*
  for(i=1; i<=10;i++)
  {
    VSE(i,i);
    VSE(11-i,i);
    VSE(7,i);
    VSE(1,i);
  }
*/
  for(r=1;r<=10;r++)
  for(c=1;c<=10;c++)
  {
    VSE(r,c);
  }

  printf("0\n");
  return 0;
} /* main */

