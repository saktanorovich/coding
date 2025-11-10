create or replace function NthHighestSalary(N int)
returns table (Salary int) as $$
begin
  return query (
    with high as (
        select e.id,
               e.salary,
               dense_rank() over (
                   order by e.salary desc
               ) as rank
        from Employee e
    )
    select distinct(h.salary)
      from high as h
    where h.rank = N
  );
end;
$$ language plpgsql;