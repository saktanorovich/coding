select user_id as user_id,
       upper(substring(name, 1, 1)) ||
       lower(substring(name, 2, length(name) - 1)) as name
  from Users
order by user_id
