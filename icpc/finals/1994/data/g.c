/*
   Problem:     VTAS - Vessel Trafic Advisory System
   Description: report route characteristics and event in vessel trafic   
   Class:       simulation 
   Subclass:    discrete
   Algorithm:   O(n^3*log n)
   Author:      vitas (vitas@mail.kolej.mff.cuni.cz)
   Date:        spring 1998
*/

#define _GNU_SOURCE                                                        
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <math.h>

struct t_lod;

typedef struct t_cesta
{
  int port;
  int nport;
  struct t_lod *lod;
  struct t_cesta *nexthop;
  struct t_cesta *prew; /* in time in the same leg */
  struct t_cesta *next;
  float ptime, ntime;
} t_cesta;

typedef struct t_lod
{
  char *name;
  int id;
  t_cesta *fsthop;
  float stime, etime;
  float speed;
  struct t_lod *next;
} t_lod;

t_lod *fstlod=NULL;
t_lod *lstlod=NULL;

t_cesta  *ports[ 256 ];
float     delka[ 256 ][ 256 ];


int day= 0;
int lasttime=0;
int lod_id=0;

/*
 * prevest cas 19:21 (1921) na pocet minut od zacatku prvniho dne
 */
int t2t(int t)
{
  int ret;
  ret= day * 60 * 24 + (t / 100) * 60 + (t%100);
  if( lasttime > ret )
  { /* preskocil se jeden den */
    ret+= 24 * 60;
    day++;
  }
  lasttime= ret;
  return ret;
} /* t2t */

t_cesta *new_cesta(t_lod* lod, int port, int nport, float ptime, float ntime)
{
  t_cesta *ret;
  ret= (t_cesta*) malloc(sizeof(t_cesta));
  ret->port= port;
  ret->lod= lod;
  ret->nport= nport;
  ret->ptime= ptime;
  ret->ntime= ntime;
  ret->next= NULL;
  ret->prew= NULL;
  ret->nexthop= NULL;
  return ret;
} /* new_cesta */

t_lod *new_lod(char *name, float stime, float etime, float speed)
{
  t_lod *ret;

  ret= (t_lod*) malloc(sizeof(t_lod));
  ret->id= lod_id++;
  ret->name= strdup(name);
  ret->stime= stime;
  ret->etime= etime;
  ret->speed= speed;
  ret->fsthop= NULL;
  ret->next= NULL;
  return ret;
} /* new_lod */

t_cesta *zarad_do_portu(t_lod *lod, int port, int nport, float ptime, float ntime)
{
  t_cesta start;
  t_cesta *last;
  t_cesta *cesta;

  last= &start;
  start.next= ports[ port ];

  for(;last->next; last=last->next)
  {
    if(last->next->ptime < ptime)
    {
      break;
    }
  }
  
  /* zarad za `last' novou lod */
  cesta= new_cesta(lod, port, nport,  ptime, ntime); 

  cesta->next= last->next;
  last->next= cesta;
  cesta->prew= (last == &start)?NULL:last;
  if( last->next ) last->next->prew= cesta;

  ports[ port ]= start.next;

  return cesta;

} /* zarad_do_portu */

int is_valid_route(char *s)
{
  for(;s[1];s++)
  {
    if( 0 ==  delka[ (unsigned char)s[0] ][ (unsigned char)s[1] ] )
    {
      return 0;
    }
  }
  return 1;
} /* is_valid_route */

float time4way(int from, int to, float speed)
{
  return (delka[from][to]*60) / speed;
} /* time4way */

void zarad_lod(char *name, float stime, float speed, char *way)
{
  t_lod *ret;
  t_cesta cesta;
  t_cesta *last;
  float ptime, ntime;

  if( !is_valid_route(way) )
  {
     ret= new_lod(name, stime, 0, speed);
     ret->etime= stime;
  }
  else
  {
    last= &cesta; 
    ptime= stime;

    ret= new_lod(name, stime, ptime, speed);

    for(; *way; way++)
    {
      ntime= ptime + time4way((unsigned char)way[0], (unsigned char)way[1],
			      speed);
      last->nexthop= zarad_do_portu(ret, way[0], way[1], ptime, ntime);
      ptime= ntime;
      last=last->nexthop;
    }
    last->nexthop= NULL;

    ret->fsthop= cesta.nexthop;
    ret->etime= ptime;
      
  }

  if( lstlod == NULL )
  {
    fstlod= ret;
    lstlod= ret;
    return;
  }

  lstlod->next= ret;
  lstlod= ret;
 
} /* zarad_lod */

int eolode=0;

void lode_do(int dokdy)
{
  char way[40];
  char name[40];
  float speed;
  int stime;

  if( eolode )
  {
    return;
  }

  stime= lasttime;  
  for(;stime <= dokdy;)
  {
    scanf("%s", name);
    if( name[0] == '*' )
    {
      eolode= 1;
      return;
    }
    scanf("%d %f %s", &stime, &speed, way);
    stime= t2t(stime); 
    zarad_lod(name, stime, speed, way);
  }
  
} /* lode_do */


void del_cesta(t_cesta *cesta)
{
  if( ports[ cesta->port ] == cesta )
  { /* smaz rovnou prvni cestu */
    ports[ cesta->port ]= cesta->next;
    cesta->next->prew= NULL;
    free(cesta);
  }
  /* zarad do spojaku */
  cesta->prew->next= cesta->next;
  if( cesta->next ) cesta->next->prew= cesta->prew;
  free(cesta);
} /* del_cesta */


void zrus_lod_do(int dokdy)
{
  t_lod start;
  t_lod *last;
  t_lod *tmp_lod;
  t_cesta *nexthop;
  t_cesta *cesta;

  start.next= fstlod;
  last= &start;

  for(;last->next && last->next->stime < dokdy ; last= last->next)
  {
    if( last->next->etime < dokdy )
    { /* lod je nepouzitelna: zrusit */
      tmp_lod= last->next->next;
      cesta= last->next->fsthop;

      /* snazat vsechny cesty teto lode */
      for(;cesta;cesta=nexthop)
      {
        nexthop= cesta->nexthop;
        del_cesta(cesta);
      }
      free(last->next);
      last->next= tmp_lod;
      if( last->next == NULL )
      {
        break;
      }
    } /* if */
  } /* for */

  fstlod=start.next;

} /*zrus_lode_do */  


void print_close(t_cesta *cesta)
{
  float ptime;
  t_cesta *i;
 
  ptime= cesta->ptime;

  for(i=ports[cesta->port]; i;i=i->next)
  {
    if( i != cesta && i->ptime < ptime + 3 &&  i->ptime > ptime - 3 ) 
    { /* potkali se */
      printf("++ Close passing with %s at Waypoint %c\n",
        i->lod->name,
        cesta->port);
    }
  }
} /* print_close */

int je_drive(t_cesta *c1, t_cesta *c2)
{
  if( c2 == NULL ) return 1;

  if( c1->ptime == c2->ptime )
  {
    return c1->lod->id < c2->lod->id;
  }

  return c1->ptime < c2->ptime;
} /* je_drive  */
 
void print_encounter(t_cesta *cesta)
{
  t_cesta *c1, *c2;
  int port, nport;
  int id;

  if( 0 == cesta->nexthop)
  {
    return;
  }

  id= cesta->lod->id;
  port= cesta->port;
  nport= cesta->nport;

  c1= ports[cesta->port];
  c2= ports[cesta->nexthop->port];

#define FSTR "++ Project encounterg with %s on leg between Waypoints %c and %c\n"

  for(;c1||c2;)
  {
    for(;c1&& je_drive(c1, c2);c1=c1->next )
    {
      if( c1->lod->id == id ) continue;
      if(       c1->nport == nport &&
         c1->ptime <= cesta->ntime &&
         c1->ntime >= cesta->ptime )
      {
        printf(FSTR, c1->lod->name, port, nport);
      }
    }
    for(;c2&& je_drive(c2, c1);c2=c2->next )
    {
      if( c2->lod->id == id ) continue;
      if(       c2->nport == port &&
         c2->ptime <= cesta->ntime &&
         c2->ntime >= cesta->ptime )
      {
        printf(FSTR, c2->lod->name, port, nport);
      }
    }
  } /* c1||c2 */
} /* print_encounter */

  
#define MINUT(x) ( ((int)round(x))%60 )
#define HODIN(x)  ( (((int)round(x))/60) %24 )
    
void print_lod(t_lod *lod)
{
  t_cesta *cesta;

  printf("%s entering system at %02i%02i "
         "with a planed speed of %.1f knots\n",
	 lod->name,
         HODIN(lod->stime),
         MINUT(lod->stime),
         lod->speed
         );
  if( lod->fsthop == NULL )
  {
    printf("!! Invalid Route Plan for Vessel: %s\n\n", lod->name);
    return;
  }

  printf("-- Waypoints:");
  for(cesta= lod->fsthop; cesta; cesta=cesta->nexthop)
    printf("    %c", cesta->port);

  printf("\n-- Arrival:  ");
  for(cesta= lod->fsthop; cesta; cesta=cesta->nexthop)
    printf(" %02i%02i", HODIN(cesta->ptime), MINUT(cesta->ptime));

  printf("\n");
  for(cesta= lod->fsthop; cesta; cesta=cesta->nexthop)
  {
    print_close(cesta);
    print_encounter(cesta);
  }

  printf("\n");

	
} /* print_lod */


void proces_all()
{
  t_lod *lod;
  lode_do( 24 * 60 );
  
  for(lod= fstlod; lod; lod=lod->next)
  {
    lode_do(lod->etime + 3);
    print_lod(lod);
  }
  if( !eolode )
  {
    printf("puuusr\n");
  }
		      
} /* process_all */


void nacti_vstup()
{
  int N;
  int i,j;
  float d;
  char w[30];
  scanf("%i %s", &N, w);

  for(i=256;i--;)for(j=256;j--;)delka[i][j]=0;

  for(i=0;i<N;i++)
  {
    for(j=0;j<N;j++)
    {
      scanf("%f", &d);
      delka[ (unsigned char)w[i] ][ (unsigned char)w[j] ] = d; 
    }
  }
} /* nacti_vstup */

int main()
{
  nacti_vstup();
  proces_all();
  return 0;
}
