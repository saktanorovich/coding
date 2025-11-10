select p.email
  from Person p
group by p.email
having count(p.email) > 1
