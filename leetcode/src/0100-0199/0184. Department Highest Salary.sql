select d.Name   as Department,
       e.Name   as Employee,
       e.Salary as Salary
from Employee   e,
     Department d,
     (
       select e.DepartmentId as DepartmentId,
              max(e.Salary)  as Salary
       from Employee e
       group by e.DepartmentId
     ) o
where e.DepartmentId = d.Id
  and o.DepartmentId = d.Id
  and e.Salary = o.Salary

/*
select d.Name   as Department,
       e.Name   as Employee,
       e.Salary as Salary
from Employee   e,
     Department d
where (e.DepartmentId = d.Id)
  and (e.DepartmentId, e.Salary) in (
    select e.DepartmentId as DepartmentId,
           max(e.Salary)  as Salary
    from Employee e
    group by e.DepartmentId
  )
*/