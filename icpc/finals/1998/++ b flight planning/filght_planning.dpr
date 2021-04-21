program flight_planning;

uses
      math;

const
      maxk      = 100;
      up        = 40;
      down      = 20;
      vcruise   = 400;
      aopt      = 30;
      gphopt    = 2000;
      gphextra  = 10;
      climbcost = 50;

type
      leg = record
            len: integer;
            winddown, windup: integer;
            altitudeopt: integer;
      end;

var
      fuel: array [1..maxk, down..up] of real;
      prev: array [1..maxk, down..up] of integer;
      legs: array [1..maxk] of leg;
      n, k, i, j: integer;
      fuelopt: real;

function getwindspeed(leg: leg; altitude: integer): real;
begin
      getwindspeed :=(leg.windup - leg.winddown) * (altitude - down) / (up - down) + leg.winddown;
end;

function getfuel(leg: leg; altitude: integer): real;
begin
      getfuel :=(abs(altitude - aopt) * gphextra + gphopt) * leg.len / (vcruise + getwindspeed(leg, altitude));
end;

function getclimbfuel(h1, h2: integer): integer;
begin
      getclimbfuel :=0;
      if h1 < h2 then
            getclimbfuel :=abs(h2 - h1) * climbcost;
end;

procedure process();
var
      i, j, h: integer;
begin
      for h :=down to up do
            fuel[1, h] := h * climbcost + getfuel(legs[1], h);
      for i :=2 to k do
            for j :=down to up do
            begin
                  fuel[i, j] :=1e10;
                  for h :=down to up do
                  begin
                        if fuel[i - 1, h] + getclimbfuel(h, j) < fuel[i, j] then
                        begin
                              fuel[i, j] :=fuel[i - 1, h] + getclimbfuel(h, j);
                              prev[i, j] :=h;
                        end;
                  end;
                  fuel[i, j] := fuel[i, j] + getfuel(legs[i], j);
            end;
      fuelopt :=1e10;
      for i :=down to up do
            if fuel[k, i] < fuelopt then
            begin
                  fuelopt :=fuel[k, i];
                  legs[k].altitudeopt :=i;
            end;
      for i :=k - 1 downto 1 do
            legs[i].altitudeopt :=prev[i + 1, legs[i + 1].altitudeopt];
end;

begin
      read(n);
      for i :=1 to n do
      begin
            read(k);
            for j :=1 to k do
                  read(legs[j].len, legs[j].winddown, legs[j].windup);
            process();
            write('Flight ', i, ': ');
            for j :=1 to k do
                  write(legs[j].altitudeopt, ' ');
            writeln(ceil(fuelopt));
      end;
end.

