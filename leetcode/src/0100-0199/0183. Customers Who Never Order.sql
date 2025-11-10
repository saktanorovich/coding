with buyers as (
    select o.customerId
      from Orders o
)
select c.name as "Customers"
  from Customers c
where c.id not in (
    select customerId from buyers
)
