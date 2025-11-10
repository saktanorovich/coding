/* 
    Input generator

    Problem:   Borrowers
    Autor:     Vitas
    Date:      summer 1998
*/

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define BOOK_SIZ  200
#define SHELVE_POC 1000
#define BORROW_POC 500


char book[BOOK_SIZ][81];
char au[BOOK_SIZ][81];
/*
 * -1 je 
 * i je vracena v j-tem kroku ( na zacatku 0)
 */ 
int flag[BOOK_SIZ];

#define N_SIZ  16
char *b[N_SIZ]={
  "dlouhem",
  "jenickovi",
  "makove panence",
  "sirokem",

  "marence",
  "motylu emanueli",
  "bystrozrakem",
  "pernikove chaloupce", 

  "velke repe",
  "gagarinovi", 
  "slunecniku",
  "mesicniku",

  "jarmilce",
  "vilemovi",
  "hynkovi",
  "karlovi",

}; /* b */

#define A_SIZ 5
char *a[A_SIZ]= {
  "Nemcova, B.",
  "Sekora, J.",
  "Stalin, J. V.",
  "Capek, K.",
  "Gregor, P.",
}; /* a */


int rr(int r){return rand()%r;}
void init_book()
{
  int i;

  for(i= BOOK_SIZ; i--;)
  {
    sprintf(book[i], "O %s a o %s a o %s a o %s.",
      b[ ( ( i      ) & 3 )    ],
      b[ ( ( i >> 2 ) & 3 ) + 4],
      b[ ( ( i >> 4 ) & 3 ) + 8],
      b[ ( ( i >> 6 ) & 3 ) +12]);

    sprintf(au[i], "%s and %s and %s",
      a[rr(A_SIZ)],
      a[rr(A_SIZ)],
      a[rr(A_SIZ)]);
  }
  for(i= BOOK_SIZ; i--;)
  {
    /* vsechny knihy doma */
    flag[i]= 0;
    printf("\"%s\" by %s\n", book[i], au[i]);
  }
  printf("END\n");
} /* init_book */

void borrow()
{

  int bb;
  int i, j;
  int jj;

  for(i=SHELVE_POC;i--;)
  {
    jj= rr(BORROW_POC);
    for(j=1; j < jj; j++)
    {
      bb= rr(BOOK_SIZ);
      if( flag[bb] == i )
      { /* byla vracena v tomto kole: jiz nepujcuj */
	continue;
      }
      if( flag[bb] == -1 )
      { /* je pujcena */
        printf("RETURN \"%s\"\n", book[bb]);
	flag[bb]= i;
      }
      else 
      {
        printf("BORROW \"%s\"\n", book[bb]);
	flag[bb]= -1;
      }
    }
    printf("SHELVE\n");
  }
  printf("END");
} /* borrow */




int main()
{
  init_book();
  borrow();
  return 0;
} /* main */
    
    
