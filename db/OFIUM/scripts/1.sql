insert into receipt_types(name, letter, sign)
select 'Factura A', 'A', 1
	where not exists (select id_receipt_type from receipt_types where name = 'Factura A');
		
insert into receipt_types(name, letter, sign)
select 'Factura B', 'B', 1
	where not exists (select id_receipt_type from receipt_types where name = 'Factura B');

insert into receipt_types(name, letter, sign)
select 'Factura C', 'C', 1
	where not exists (select id_receipt_type from receipt_types where name = 'Factura C');
	
insert into receipt_types(name, letter, sign)
select 'Factura X', 'X', 1
	where not exists (select id_receipt_type from receipt_types where name = 'Factura X');
	
	
	
insert into receipt_types(name, letter, sign)
select 'Nota de Credito A', 'NC A', -1
	where not exists (select id_receipt_type from receipt_types where name = 'Nota de Credito A');
		
insert into receipt_types(name, letter, sign)
select 'Nota de Credito B', 'NC B', -1
	where not exists (select id_receipt_type from receipt_types where name = 'Nota de Credito B');

insert into receipt_types(name, letter, sign)
select 'Nota de Credito C', 'NC C', -1
	where not exists (select id_receipt_type from receipt_types where name = 'Nota de Credito C');
	
insert into receipt_types(name, letter, sign)
select 'Nota de Credito X', 'NC X', -1
	where not exists (select id_receipt_type from receipt_types where name = 'Nota de Credito X');
	

insert into receipt_types(name, letter, sign)
select 'Nota de Debito A', 'ND A', 1
	where not exists (select id_receipt_type from receipt_types where name = 'Nota de Debito A');
		
insert into receipt_types(name, letter, sign)
select 'Nota de Debito B', 'ND B', 1
	where not exists (select id_receipt_type from receipt_types where name = 'Nota de Debito B');

insert into receipt_types(name, letter, sign)
select 'Nota de Debito C', 'ND C', 1
	where not exists (select id_receipt_type from receipt_types where name = 'Nota de Debito C');
	
insert into receipt_types(name, letter, sign)
select 'Nota de Debito X', 'ND X', 1
	where not exists (select id_receipt_type from receipt_types where name = 'Nota de Debito X');
	
	
	
	
	
	