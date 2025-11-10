select
    employee_id   as employee_id,
    department_id as department_id
  from Employee
where primary_flag = 'Y'
  or  employee_id in (
    select
      employee_id as employee_id
    from Employee
      group by employee_id
      having count(employee_id) = 1
  )
order by 1, 2