with report as (
    select a.player_id       as player_id,
           min(a.event_date) as logged_at
      from activity a
    group by a.player_id
)
select round(count(a.player_id) * 1.0 / count(r.player_id), 2) as "fraction"
  from activity a
         right join
       report r
   on r.player_id = a.player_id
  and r.logged_at = a.event_date - 1
