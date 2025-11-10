delete from Person p
where p.id not in (
    select min(p.id)
      from Person p
    group by p.email
)
