select event_day as day,
       emp_id    as emp_id,
       sum(time) as total_time
  from (
    select event_day,
           emp_id,
           out_time - in_time as time
    from Employees
  )
group by day, emp_id
order by day, emp_id