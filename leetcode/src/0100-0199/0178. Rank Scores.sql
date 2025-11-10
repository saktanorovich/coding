with high as (
    select  s.id     as id,
            s.score  as score,
            dense_rank() over (
                order by s.score desc
            ) as rank
    from Scores as s
)
select h.score,
       h.rank
  from high as h
order by h.rank
