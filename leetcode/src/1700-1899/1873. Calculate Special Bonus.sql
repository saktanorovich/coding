select * from (
    select employee_id as employee_id, salary * 1 as bonus
      from Employees
    where mod(employee_id, 2) = 1
      and name not like 'M%'
  union all
    select employee_id as employee_id, salary * 0 as bonus
      from Employees
    where mod(employee_id, 2) = 0
       or name like 'M%'
)
order by employee_id
