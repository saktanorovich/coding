select employee_id from (
    select employee_id from Employees
      where employee_id not in (
        select employee_id from Salaries
      )
  union all
    select employee_id from Salaries
      where employee_id not in (
        select employee_id from Employees
      )
)
order by 1
