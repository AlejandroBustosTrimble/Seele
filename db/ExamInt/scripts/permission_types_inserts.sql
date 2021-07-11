
insert into permission_types(name)
select 'Enfermedad'
	where not exists (select 1 from permission_types where name = 'Enfermedad');
	
insert into permission_types(name)
select 'Diligencia'
	where not exists (select 1 from permission_types where name = 'Diligencia');