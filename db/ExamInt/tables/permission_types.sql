	drop table if exists permission_types;

    create table permission_types (
       id_permission_type bigserial not null,
       name character varying not null,
       create_datetime timestamp default now() ,
       update_datetime timestamp default null,
       active boolean default true ,
       primary key (id_permission_type)
    );
