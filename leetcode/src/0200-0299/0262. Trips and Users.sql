with report as (
    select t.request_at,
        case when t.status !='completed'
            then 1
            else 0 end
          as cancelled
      from Users u,
           Users d,
           Trips t
    where u.users_id = t.client_id
      and d.users_id = t.driver_id
      and u.banned = 'No'
      and d.banned = 'No'
)
select r.request_at as "Day",
       round(sum(r.cancelled) * 1.0 / count(r.cancelled), 2) as "Cancellation Rate"
  from report r
where r.request_at between '2013-10-01' and '2013-10-03'
group by r.request_at
order by r.request_at
