	drop table if exists permissions;

    create table permissions (
       id_permission bigserial not null,
       name character varying not null,
	   surname character varying not null,
	   request_datetime timestamp not null,
       create_datetime timestamp default now() ,
       update_datetime timestamp default null,
       active boolean default true ,
       primary key (id_permission)
    );
