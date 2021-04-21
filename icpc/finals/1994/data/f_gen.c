/* 
   Input generator
   
   Problem:      Typesetting
   Autor:        Vitas
   Date:         summer 1998
*/

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define NPARS 3000

#define FSIZ   100
#define WORDS  10
#define LINES  30
#define LINEW  32000
#define SPACE 5
#define WORDFONTSIZ 5
char *fchar="ABCabc~ ";
char space[50];
int fsiz;

void font_def()
{
  char *zn= fchar;
  fsiz= strlen(fchar);
  printf("%d\n", fsiz);
  for(;*zn;zn++)
  {
    printf("%c %d %d %d %d %d %d\n", 
     *zn,
     rand()%FSIZ+1,
     rand()%FSIZ+1,
     rand()%FSIZ+1,
     rand()%FSIZ+1,
     rand()%FSIZ+1,
     rand()%FSIZ+1);
  }
} /* font_def */
  
void rand_word()
{
  int i;
  /* 1:1:5 == *fN:*sN:SLOVO */
  switch(rand()%WORDFONTSIZ)
  {
  case 0: 
    printf("*s%i", rand()%99+1);
    break;
  case 1:
    printf("*f%i", rand()%6+1);
    break;
  default:
    for(i=rand()%8+1;i--;)
    {
      printf("%c", fchar[rand()%fsiz]);
    }
    break;
  }
} /* rand_word */

int main()
{
  int pars;
  int L,W;
  int i;
  for(i=SPACE; i--;)space[i]=' ';
  space[SPACE]='\0';

  srand(0);

  font_def();
  for(pars= NPARS;pars--;)
  {
    L= rand()%LINES + 1;
    W= rand()%LINEW + 1;

    printf("%d %d\n", L, W);
    for(; L--;)
    {
      printf("%s", space + rand()%SPACE);
      for(i= rand()%WORDS+1;i--;)
      { 
        rand_word();
        if(i)
        { /* za prostredni slovo mezery*/
          printf("%s", space + rand()%(SPACE-1));
        }
         
      }
      /* na konec jeste nahodne mezer */
      printf("%s\n", space + rand()%SPACE);
    }
  }
  printf("0 0\n");
  return 0;
} /* main */
