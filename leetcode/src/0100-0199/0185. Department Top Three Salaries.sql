with high as (
    select  e.id     as employeeId,
            e.name   as employeeName,
            e.salary as salary,
            d.id     as departmentId,
            d.name   as departmentName,
            dense_rank() over (
                partition by d.id
                order by e.salary desc
            ) as rank
    from Employee   as e,
         Department as d
    where e.departmentId = d.id
)
select h.departmentName as "Department",
       h.employeeName   as "Employee",
       h.salary         as "Salary"
  from high as h
where h.rank <= 3
