select t.machine_id as machine_id,
       round(avg(t.duration)::numeric, 3) as processing_time
  from (
    select st.machine_id as machine_id,
           st.process_id as process_id,
           en.timestamp - st.timestamp as duration
      from Activity as st,
           Activity as en
    where st.machine_id = en.machine_id
      and st.process_id = en.process_id
      and st.activity_type = 'start'
      and en.activity_type = 'end'
  ) as t
group by t.machine_id
order by t.machine_id
