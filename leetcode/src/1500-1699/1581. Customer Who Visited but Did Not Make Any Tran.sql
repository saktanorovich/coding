select v.customer_id     as customer_id,
       count(v.visit_id) as count_no_trans
  from Visits as v
where v.visit_id not in (
  select visit_id from Transactions
)
group by v.customer_id
order by v.customer_id
