/*  
   Problem:       Typesetting
   Description:   typeset paragraphs into lines with simple rules
   Class:         text
   Subclass:      formating
   Algorithm:     direct, O(1)
   Autor:         Vitas
   Date:          summer 1998
*/
   
#include <stdio.h>
#include <string.h>
#include <ctype.h>

/*
 * sazeni textu
 * 
 */


/* 
 * pocet radek k precteni, na zacatku odstavce odpovida *L* ze zadani
 */
int lines;
/*
 * sirka radky, *W* ze zadani
 */
int linew;

/*
 * velikost fontu, odpovida cislu *sNN
 */
int fontsiz;
/*
 * font, *f1=> font==0;
 */
int font;

/*
 * velikost mezery, za poslednim vysazenym slovem v jeho velikosti
 * (od te doby se mohl zmenit font)
 */
int spacew;
/*
 * kde na radku konci posledni slovo 
 */
int dotedw;
/*
 * pocet jiz vyplivnutych radek
 */
int lineout;
/*
 * tabulka fontu
 */
int f[6][300];
/*
 * prvni slovo v radce
 */
char prvni[30];
/*
 * pokud je posledni slovo ruzne bude ze posledni slovo
 */
char posledni_buf[30];

/*
 * pointer na posledni slovo, pri prvnim slove se nastavi na *prvni*
 * pri kazdem dalsim na *posledn_buf
 */
char *posledni;




/*
 * vytiskni reseni, pokud je to jen jedno slovo
 * a: buf - to jedno slovo
 *    delta - o kolik bylo preteceno
 */
void print_single(char *buf, int delta)
{
  lineout++;
  printf("Line %i: %s (%i whitespace)\n", lineout, buf, delta);
} /* print_single */

/*
 * vytiskni reseni, pokud se slovo normalne vlezlo na radek
 */
void print_double()
{
  lineout++;
  printf("Line %i: %s ... %s (%i whitespace)\n",
    lineout, 
    prvni, 
    posledni, 
    linew - dotedw);
} /* print_double */

/*
 * sirka znaku, v tabulce jsou 10ti bodove velikosti
 * ostatni se dopocitavaji linearne, zaokrouhluje se od 0.5 nahoru
 */
#define CHAR_SIZ(c) ( (f[font][(unsigned char)(c)]*fontsiz + 9) / 10 )
/*
 * spocitat delku slova
 * a: buf - slovo jehoz delka se ma spocitat
 */
int delka_slova(char *buf)
{
  int ret=0;
  for(;*buf;buf++)
  {
    ret+= CHAR_SIZ(*buf);
  }
  return ret;
} /* delka_slova */

/*
 * zmen font  v bufu bude prikaz ke zmene fontu dle zadani
 * *f2 -> promena *font* bude mit hodnotu 1
 * *s88 -> promena *fontsiz* 88
 */
void xfont(char *buf)
{
  if( buf[1] == 'f' )
  { /* zmena nazvu fontu  *f1 => font==0 */
    font= buf[2] - '1';
    return;
  } 
  /* buf[1] == s => zmena velikosti fontu */
  fontsiz=  (buf[2] - '0');

  if( buf[3] )
  { /* pokud je cislo dovouciferne */
    fontsiz*= 10;
    fontsiz+= buf[3] - '0';
  }
} /* xfont */
   

/*
 * vysazi slovo
 * musi byt nastaveno:
 * *dotedw* - kam az saha posledni text
 * *spacew* - velikost poslendni mezery
 * *lineout* - aktulani spracovavana radka (vzhledem k vystupu)
 * pokud je *dotedw* != 0
 * *prvni* prvni slovo
 * *posledni* posledni slovo
 */

void sazej(char *buf)
{
  int ww;
  int new_dotedw;
  
  ww= delka_slova(buf);

  if( dotedw == 0  )
  { /* prvni slovo */
	 spacew= CHAR_SIZ(' ');
	 if( ww > linew )
	 {
		print_single(buf, linew - ww);
		/* dotedw= 0; */
		return;
	 }
	 posledni= strcpy(prvni, buf);
	 dotedw= ww;
	 return;
  }

  /* v bufu jiz je nejake slovo */
  new_dotedw= dotedw + spacew + ww;
  spacew= CHAR_SIZ(' ');
  if(  new_dotedw <= linew )
  { /* v poradku vysazej slovo */
	 dotedw= new_dotedw;
	 posledni= strcpy(posledni_buf, buf);
    return;
  }

  /* vysazej slovo na samostatny novy radek */
  print_double();
  if( ww > linew )
  {
    print_single(buf, linew - ww);
    dotedw= 0; /* dalsi slovo bude prvni na radku */
    return;
  }
    
  dotedw= ww;
  posledni= strcpy(prvni, buf);

} /* sazej */

/*
 * konecny automat na prejizdeni mezer, nacitani slov
 * a pocitani koncu radku
 */
void  solve_par()
{
  int zn;
  /*
   * slovo nebude vetsi nez 8 znaku (9 i s '\0')
   */
  char buf[9];
  /*
   * aktualni pozice v *buf* pri nacitani slova
   */
  char *s= buf;
 
  zn= getchar();

  for(;;)
  {
    while(isspace(zn))
    { /* skip white */
      if( zn == '\n' && --lines == 0 )
      { /* konec */
        if( dotedw > 0 )
	{ /* v radku neco bylo: vytiskni co */
	  print_double();
	}
        return;
      }
      zn= getchar();
    }

    while(!isspace(zn))
    { /* nacti slovo do buf */
      *s++=zn;
      zn= getchar();
    }
    *s= '\0';

    if( *buf == '*' )
    { /* pokud je prvni znak '*' => prikaz */
      xfont(buf);
    }
    else
    { /* jinak je to obych slovo */
      sazej(buf);
    }

    /* vycisti *buf* */
    s= buf;

  }/*for*/
} /* solve */

int main()
{
  int N;
  char zn;

  /* nacteni fontu */
  scanf("%d", &N);

  /* nacist znakovou sadu */
  for(;N--;)
  { 
    /* eat \n */
    zn= getchar();

    /* get letter */
    zn= getchar();
    scanf("%d %d %d %d %d %d", 
      &f[0][(unsigned char)zn],  
      &f[1][(unsigned char)zn],  
      &f[2][(unsigned char)zn],  
      &f[3][(unsigned char)zn],  
      &f[4][(unsigned char)zn], 
      &f[5][(unsigned char)zn]);
  }

  /* formatuj odstavec */
  for(N=1;;N++)
  {
    /* defaultni nastaveni na zacatku kazdeho odstavce */
    font= 0; /* = *f1 */
    fontsiz= 10;
    lineout= 0;
    dotedw= 0;
    scanf("%d %d\n", &lines, &linew);
    if( lines == 0 )
    { /* konec zadani */
      return 0;
    }
    printf("Paragraph %i\n", N);
    solve_par();
  }
      
} /* main */
