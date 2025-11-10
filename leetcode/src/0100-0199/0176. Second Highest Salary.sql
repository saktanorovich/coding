with high as (
    select max(salary) as salary
      from Employee
)
select max(e.salary) as SecondHighestSalary
  from Employee as e
where e.salary < (select salary from high);

