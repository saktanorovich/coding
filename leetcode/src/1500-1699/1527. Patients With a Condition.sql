select p.patient_id   as patient_id,
       p.patient_name as patient_name,
       p.conditions   as conditions
  from Patients as p
where p.conditions like 'DIAB1%'
   or p.conditions like '% DIAB1%'
