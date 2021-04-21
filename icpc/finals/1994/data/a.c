/* 
    Problem:     Borrowers
    Description: simulate simple librarian
    Class:       simulations
    Subclass:    discrete
    Algorithm:   merge sort of sorted returned books into shelve, n^2
    Author:      vitas
    Date:        summer 1998
*/ 
    
#define _GNU_SOURCE
#include <stdio.h>
#include <stdlib.h>
#include <string.h>


/*
 * vstup: cast 1: "nazev knihy" by autor pripadne autori
 *                ...
 *                END
 *        cast 2: BORROW "nazev knihy"
 *                RETURN "nazev knihy"
 *                SELVE
 *                ...
 *                END
 * 
 * vystup: knihy definovane v casti 1 (pote setrideny do policek 
 * dle autora a pak dle nazvu knihy), jsou pujcovany
 * (BORROW) a vraceny. vracene knihy ponechavany  do ``SELVE''
 * pak se setridi dle autora, a nasledujicimi prikazy se roztridi
 * do knihonvnicky
 *          Put "nazev knihy" after "nazev knihy pred"
 *          Put "nazev knihy" first 
 */

/*
 * algoritmus: knihy jsou v strukutre 
 * *t_kniha* (jm- jmeno au- autor, flg- stav vypujceni: 0, 'B', 'R')
 * (0 v knihovne, 'B' pujcena 'R' prave vracna)
 * po nacteni vsech knih, se setridi, jednak dle jmena (pole *by_jm*)
 * a jednak dle autora (*by_au*), pri vypujce se zaznamena do *flg* 
 * 'B' pri vraceni 'R'. vyhledavani v poli *by_jm* momoci fce *bsearch*
 * pri SELVE se projde cele pole *by_au*
 * a vytisknese se kam se vracene knizky maji dat. prcemz priznak
 * 'R' se zmeni na 0 
 * 
 */

struct t_kniha {
  char *jm;
  char *au;
  char flg; /* mozna stavy 0- v knihovne, 'B' pujcna, 'R' vracna */
};
typedef struct t_kniha t_kniha;

t_kniha **by_au; /* setrizene knihy dle autora */
t_kniha **by_jm; /* setrizene dle jmena */

int kniha_siz; /* velikost pole by_au, (zvetsuje se 2x) */
int kniha_poc=0; /* pocet knih */

void init()
{
  kniha_poc= 0;
  kniha_siz= 50;
  by_au= (t_kniha**)malloc(kniha_siz * sizeof(t_kniha*));
}

/*
 * prida knihu do *by_au* (zatim nezatrizuje)
 * *flg* bude 0 , 
 * jmeno a autor, dle *jm*, *au* 
 */
void add_kniha(char *jm, char *au)
{
  t_kniha *nw;

  if( kniha_poc >= kniha_siz)
  { /* zvetsim misto na knihy */
    kniha_siz*= 2; 
    by_au= (t_kniha**)realloc(by_au, kniha_siz * sizeof(t_kniha*) );
  }

  nw= (t_kniha*)malloc(sizeof(t_kniha));


  nw->jm= strdup(jm);
  nw->au= strdup(au);
  nw->flg= 0;

  by_au[kniha_poc]= nw;
  kniha_poc++;
} /* add_kniha */

/*
 * porovnani, ``dle autora'', pokud se autor shoduje pak dle
 * jmena
 * r: <0 a < b
 *     0 a == b
 *    >0 a > b
 *
 */
int cmp_by_au(const void *a, const void *b)
{
  
  t_kniha *aa= *((t_kniha**)a);
  t_kniha *bb= *((t_kniha**)b);

  int ret;
  ret= strcmp(aa->au, bb->au);  
  if( ret != 0 )
  {
    return ret;
  }
  ret= strcmp(aa->jm, bb->jm);
  return ret;
} /* cmp_by_au */

/*
 * porovnani dle knihy, (knihy by nikdy nemely byt stejne)
 * r: <0 a < b
 *     0 a == b 
 *    >0 a > b
 */
int cmp_by_jm(const void *a, const void *b)
{
  int ret;
  t_kniha *aa= *((t_kniha**)a);
  t_kniha *bb= *((t_kniha**)b);
  ret= strcmp( aa->jm, bb->jm );
  return ret;

 /* printf("cmp_by_name:\"%s\" ? \"%s\" = %i\n", aa->jm, bb->jm, ret);
  */
} /* cmp_by_jm */

/*
 * po docteni prvni casti s popisem knih
 * se tyto uzporadaji dle autora ci dle jmena
 */
void end_kniha()
{
  by_jm= (t_kniha**)malloc(kniha_poc * sizeof( t_kniha *));
  memcpy(by_jm, by_au, kniha_poc * sizeof(t_kniha*) );  

  qsort(by_au, kniha_poc, sizeof(t_kniha*), cmp_by_au);
  qsort(by_jm, kniha_poc, sizeof(t_kniha*), cmp_by_jm);

} /* end_kniha */

/*
 * pokud se kniha vypujci (resp. vrati) nastavi se flag 'B' (resp 'R')  
 */
void setflg_kniha(char *s, char flg)
{
  t_kniha kniha;
  t_kniha **kniha_found;
  t_kniha *pkniha= &kniha;

  kniha.jm= s;
  kniha_found= 
    (t_kniha**)
    bsearch(&pkniha, by_jm, kniha_poc, sizeof(t_kniha*), cmp_by_jm);
  if( kniha_found == NULL)
  { /* nemelo by nastat */
    printf("err: kniha not found\n");
    exit(-1);
  }

  (*kniha_found)->flg= flg; /* vracena */;
} /* ret_kniha */
 
/*
 * tisk reseni:
 * prochazim knihy (*by_au*) kdyz narazim na  knihu ktera je v knihovne
 * nastavim last, 
 * kdyz narazim na knihu ktera je vracena, vytisku ji i s ifem ktera je
 * pred ni
 * kdyz narazim na vypujcenou, nedelam nic
 * na zacatku je last -1, proto poznam jestli pred 'R' knihou je
 * nejaka nebo ne.
 */
void print_solution()
{
  int last= -1;
  int i;

  for(i= 0; i < kniha_poc; i++)
  {
    switch(by_au[i]->flg)
    {
    case 0: /* na svem miste */
      last= i;
      break;
    case 'B': /* vypujcena */
      break;
    case 'R': /* vracena */
      by_au[i]->flg= 0; /* je v knihovne */
      if (last < 0)
      { /* 
         * kdyz je last < 0 znamena to, 
         * ze pred ni v knihovne neni zadna kniha
         */
        printf("Put \"%s\" first\n", by_au[i]->jm);
      }
      else 
      {
        printf("Put \"%s\" after \"%s\"\n", by_au[i]->jm, by_au[last]->jm); 
      }
      last=i;
    } /* switch */
  } /* for */
  printf("END\n");
} /* print solution */

char buf[80*3];

/*
 * nacte radek a zaradi knihy do knihovny
 * r: !0 pokracuj ve cteni
 *     0 konec sekce
 */
int read_line1()
{
  /* i je pozice druhe uvozovky */ 
  char *s;

  gets(buf);

  if( buf[0]== 'E' )
  { /* prislo END */
    return 0;
  }

  s= index(buf+1, '\"');

  *s= '\0';

  add_kniha(buf+1, s+5); 
  /* presk. " --^    ^--prskoc ".by.  */
  return 1;

} /* read_line1 */
 
/*
 * nacte radek a dle prikazu: 
 * vypujci knihy z knihovny 'B'
 * 'R' vrati knihy do knihovny 
 * 'S' vypis reseni
 * r: !0 pokracuj v nacitani sekce2
 *     0 konec sekce2
 */
int read_line2()
{
  gets(buf);

  if( buf[0] == 'E' )
  { /* konec zadavani 'E' => END */
    return 0;
  }
  if( buf[0] == 'S' )
  { /* prvni pismenko 'S' => SHELVE :) */
    print_solution();
    return 1;
  }
  
  *rindex(buf, '\"')= '\0'; /* ukosni posledni " */
  setflg_kniha(buf+8, buf[0]); /* oznac knihu jako 'B' nebo 'R' */
  /*               ^--- preskoc prikaz a uvozovky  */
  return 1;
} /* read_line2 */

int main()
{
  init();

  /* nacteni prvni sekce */
  while(read_line1());

  /* usporadani knih */
  end_kniha();

  /* vyhodnoceni druhe sekce */
  while(read_line2());
  return 0;
} /* main */
