	drop table if exists receipt_types;

    create table receipt_types (
       id_receipt_type bigserial not null,
       name character varying,
	   letter character varying,
       sign numeric,
       create_datetime timestamp default now() ,
       update_datetime timestamp default null,
       active boolean default true ,
       primary key (id_receipt_type)
    );
