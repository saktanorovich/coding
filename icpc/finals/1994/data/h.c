/*
		  Problem:      Monitoring Wheelchair Patients
		  Description:  Compute trace characteristics
		  Class:        2D geometry
		  Subclass:     intersection, distance
		  Algorithm:    straitforward, O(n)
		  Author:       Petr Gregor
		  Date:         May 15, 1998
*/


#include <stdio.h>
#include <math.h>

#define RX 400
#define RY 200

int main() {

#define M_PI 3.14159265358979323846 

double dx,dy,d,p,x,y,s,t1,t2,a,lx,ly,lt,td,md;
int i,j,left;

for(i=1, scanf("%d\n",&j); j!=0; scanf("%d\n",&j), i++) {

	x=200.0;
	y=td=md=0.0; left=0;
	for(; j>0; j--) {

		scanf("%lf %lf %lf %lf\n",&t1,&t2,&s,&a);
		a *= M_PI/180;
		td+= d = s*(t2-t1);
		dx = x+d*sin(a);
		dy = y+d*cos(a);
		if (!left) {
		 if (((dx>=0) && (dx<=RX)) && ((dy>=0) && (dy<=RY)))
		  md = (md < (p=sqrt((dx-200)*(dx-200)+dy*dy))) ? p : md;
		 else {
			left = 1; lx = dx; ly = dy;
			if (lx<0)  { ly = y    -x *(ly-y)/(lx-x); lx = 0;  }
			if (lx>RX) { ly = y+(RX-x)*(ly-y)/(lx-x); lx = RX; }
			if (ly<0)  { lx = x    -y *(lx-x)/(ly-y); ly = 0;  }
			if (ly>RY) { lx = x+(RY-y)*(lx-x)/(ly-y); ly = RY; }
			lt = t1 + sqrt((lx-x)*(lx-x)+(ly-y)*(ly-y))*(t2-t1)/d;
			}
		}
		x = dx;
		y = dy;
	}

	printf("Case Number %d\n",i);
	if (left) printf("Left restricted area at point (%.1f,%.1f) and time %.1f sec.\n",lx,ly,lt);
	else printf("No departure from restricted area\nMaximum distance patient traveled from door was %.1f feet\n",md);
	printf("Total distance traveled was %.1f feet\n",td);
	printf("***************************************\n");
}
return 0;
}
