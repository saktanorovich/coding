select * from (
  select agg.contest_id as contest_id,
         round((100.0 *
           agg.num_users /
           agg.tot_users)::numeric, 2) as percentage
    from (
      select r.contest_id as contest_id,
             r.num_users  as num_users,
             u.tot_users  as tot_users
        from (
          select contest_id     as contest_id,
                 count(user_id) as num_users
            from Register
          group by contest_id
         ) as r,
         (
          select count(*) as tot_users
            from Users
         ) as u
    ) as agg
) as t
order by t.percentage desc,
         t.contest_id asc
