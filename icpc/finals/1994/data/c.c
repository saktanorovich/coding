/*
   Problem:      Crosword Answers
   Description:  list across and down words in crossword
   Class:        text
   Subclass:     formating
   Algorithm:    direct,O(1)
   Author:       Michal Kicmer
   Date:         summer 1998
*/
 
#include <stdio.h>
#include <stdlib.h>
/*
 * reseni: v prvni fazi se rovnou pri nacitani
 * vytisknou Acros, pritom se do *p* ukada transponovana
 * krizovka a zaroven do *d* uklada  jake cislo
 * zacina a do *dp* kde v poli *p* zacina Down.
 * v druhe fazy se pouze projde *d* a vytiskne se
 * p+dp[i] - slovo 
 * d[i] - jeho ocislovani
 */
 

/*
 * misto na ukladani tabulky, velikost je (r+1)*c
 * misto cernych mist jsou '\0'
 */
char p[200];
/*
 * zde se ukladaji postupne cisla down
 */
int d[100];
/*
 * zacatky slov 
 */
int dp[100];
/*
 * pocet slov down
 */
int d_siz;
/*
 * r - poc radku
 * c - sloupcu
 * rc - r*c
 * R - r+1
 */
int r, c, rc;
int R;
/*
 *#define SWAPRC(x)  ( ((x) / c) + ((x) % c) * r ) 
 */

/*
 * nacteni, a rovnou vytisteni across
 * zaroven ulozeni tabulky do *p*
 */
void solve_across()
{
  int n=1; /* cislo poradove */
  char ch; /* aktualni znak */
  int last_free;/* priznak zda byl ctverecek */
  int i; /* index v poli *p* */
  int cc, cr;

  d_siz=0;
/*
 * makro na zjisteni, zdali je ctverecek nad souradnici cc,rr cerny
 * za okrajem == je cerny
 * r: 0 - je cerny
 *    !0 - je tam pismenko
 */ 
#define UPFREE(cc,cr) ( ((cr)>0) && p[(cc) * R + (cr) - 1] )

  for(cr=0; cr < r; cr++)
  { 
    last_free= 0;
    for(cc=0; cc < c; cc++)
    {
      ch= getchar();
      if(ch == '*')
      { /* cerny ctverecek: */
        if( last_free )
        { /* je to prvni cerny ctverecek v rade: udelej eol */
          printf("\n");
        }
        last_free= 0;
        p[cc  *  R + cr]=  0; 
        continue;
      }

      /* volny ctverecek */
      p[i= (cc * R + cr)]= ch;

      if( !UPFREE(cc, cr) )
      { /* nahore: plno */
        if( !last_free )
        { /* vlevo: plno nahore: plno */
          printf("%3i.", n);
        }
        d[d_siz  ]= n;
	dp[d_siz++]= i;
        n++;
      }
      else if( !last_free)
      { /* levo: plno nahore: volno  */
        printf("%3i.", n);
        n++;
      }
 
      putchar(ch);
      last_free= 1; /* priznak prazdnsti posledniho ctverecku */

    }/* for(cc) */
    getchar(); /* eat \n */
    if( last_free )
    { /* posledni ctverecek prazdny => ukonci slovo */
      printf("\n");
    }
  } /* for(cr) */
} /* solve_acr */ 

void solve_down()
{
  int i;

  /* vycistit konce tabulku */
  for(i=c;i--;p[i*R+r]='\0');

  for(i=0; i< d_siz;i++)
  {
    printf("%3i.%s\n", d[i], p + dp[i] );
  }
} /* solve_down */
   
int main()
{
  int N;
  for(N=1;;N++)
  {
    scanf("%d", &r); /* radek */
    if( r == 0 )
    {
      return 0;

    }

    scanf("%d\n", &c); /* sloupec */
    rc= r*c;
    R= r+1;

    printf("puzzle #%i:\nAcross\n",N); 
    solve_across();

    printf("Down\n");
    solve_down();
    printf("\n");

  }
}  /*main */
