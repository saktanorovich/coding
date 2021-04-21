select u.user_id as user_id,
       u.name    as name,
       u.mail    as mail
  from Users as u
where u.mail ~ '^[A-Za-z][A-Za-z,0-9,\_,\.,\-]*@leetcode\.com$'
