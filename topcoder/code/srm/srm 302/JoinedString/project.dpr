program project;

{$apptype console}

const
  max_n = 17;
  max_m = 1 shl max_n;
  max_length = 110;
  infinity = 10000;
  none = -1;

var
  minlen: array [0 .. max_m, 0 .. max_n] of integer;
  prev: array [0 .. max_m, 0 .. max_n] of integer;
  s: array [0 .. max_n] of string;
  use: array [0 .. max_n] of boolean;
  ps: array [0 .. max_n, 0 .. max_n] of integer;
  n, i, j, k, ind, leni: integer;
  res: string;
  reslen: integer;
  prefix, str: string;
  suffix: array [0 .. max_length] of string;
  setofstrings: integer;

function readstring(): string;
var
  ch: char;
begin
  repeat
    read(ch);
  until (eof or (ch > ' '));
  result := '';
  if (ch > ' ') then
  begin
    result := ch;
    repeat
      read(ch);
      if (ch <= ' ') then
        break;
      result := result + ch;
    until (eof);
  end;
end;

procedure find(theset: integer);
var
  i, j, len: integer;
  curset: integer;
begin
  for i := 0 to n - 1 do
    if (theset and (1 shl i) > 0) then
      if (minlen[theset, i] = infinity) then
      begin
        curset := theset xor (1 shl i);
        if (curset = 0) then
        begin
          minlen[theset, i] := length(s[i]);
          prev[theset, i] := none;
        end
        else
        begin
          find(curset);
          for j := 0 to n - 1 do
            if (curset and (1 shl j) > 0) then
            begin
              len := minlen[curset, j] + length(s[i]) - ps[i, j];
              if (len < minlen[theset, i]) then
              begin
                minlen[theset, i] := len;
                prev[theset, i] := j;
              end;
            end;
        end;
    end;
end;

function getstring(theset, i: integer): string;
begin
  if (prev[theset, i] = none) then
    result := s[i]
  else
    result := getstring(theset xor (1 shl i), prev[theset, i]) + copy(s[i], ps[i, prev[theset, i]] + 1, 1000);
end;

begin
  assign(input, 'input.txt');
  reset(input);
  readln(n);
  for i := 0 to n - 1 do
    s[i] := readstring();
  close(input);

  res := '';
  fillchar(use, sizeof(use), true);
  for i := 0 to n - 1 do
    for j := 0 to n - 1 do
      if ((i <> j) and use[j]) then
        if (pos(s[i], s[j]) > 0) then
        begin
          use[i] := false;
          break;
        end;
  fillchar(ps, sizeof(ps), 0);
  for i := 0 to n - 1 do
    if (use[i]) then
    begin
      suffix[0] := '';
      leni := length(s[i]);
      for j := leni downto 2 do
        suffix[leni - j + 1] := s[i][j] + suffix[leni - j];
      for j := 0 to n - 1 do
        if (use[j] and (j <> i)) then
        begin
          ind := 0;
          prefix := '';
          for k := 1 to length(s[j]) do
          begin
            if (k >= leni) then
              break;
            prefix := prefix + s[j][k];
            if (prefix = suffix[k]) then
              ind := k;
          end;
          ps[j, i] := ind;
       end;
    end;
  setofstrings := 0;
  for i := 0 to n - 1 do
    if (use[i]) then
      setofstrings := setofstrings or (1 shl i);
  for i := 0 to max_m do
    for j := 0 to n - 1 do
      minlen[i, j] := infinity;
  find(setofstrings);
  reslen := infinity;
  for i := 0 to n - 1 do
    if (minlen[setofstrings, i] < reslen) then
      reslen := minlen[setofstrings, i];
  for i := 0 to n - 1 do
    if (minlen[setofstrings, i] = reslen) then
    begin
      str := getstring(setofstrings, i);
      if (res = '') then
        res := str
      else
        if (str < res) then
          res := str;
    end;

  assign(output, 'output.txt');
  rewrite(output);
  write(res);
  close(output);
end.
