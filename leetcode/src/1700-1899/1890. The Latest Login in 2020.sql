select user_id as user_id,
       max(time_stamp) as last_stamp
  from Logins
where date_part('year', time_stamp) = 2020
  group by user_id
  order by user_id
