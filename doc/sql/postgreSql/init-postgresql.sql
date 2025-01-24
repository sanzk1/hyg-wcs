-- public.job_info definition

-- Drop table

-- DROP TABLE public.job_info;

CREATE TABLE public.job_info (
	id int8 NOT NULL,
	job_name varchar(100) NOT NULL,
	category varchar(100) NOT NULL,
	assembly_name varchar(255) NOT NULL,
	type_name varchar(255) NOT NULL,
	job_key varchar(100) NOT NULL,
	job_group varchar(100) NOT NULL,
	cron_expression varchar(20) NOT NULL,
	misfire_policy int4 NOT NULL,
	concurrent bool NOT NULL,
	status int4 NOT NULL,
	seconds int4 NOT NULL,
	repeat int4 NOT NULL,
	CONSTRAINT job_info_pkey PRIMARY KEY (id)
);


-- public.job_log definition

-- Drop table

-- DROP TABLE public.job_log;

CREATE TABLE public.job_log (
	id int8 NOT NULL,
	job_name varchar(100) NOT NULL,
	category varchar(100) NOT NULL,
	type_name varchar(255) NOT NULL,
	job_message text NOT NULL,
	exception_info text NOT NULL,
	status bool NOT NULL,
	start_time timestamp NOT NULL,
	stop_time timestamp NOT NULL,
	CONSTRAINT job_log_pkey PRIMARY KEY (id)
);


-- public.sys_log definition

-- Drop table

-- DROP TABLE public.sys_log;

CREATE TABLE public.sys_log (
	id int8 NOT NULL,
	operation varchar(255) NOT NULL,
	request_param text NOT NULL,
	response_param text NOT NULL,
	operator_name varchar(255) NOT NULL,
	operate_time timestamp NOT NULL,
	execute_time int8 NOT NULL,
	execute_status bool NOT NULL,
	reason text NOT NULL,
	"path" varchar(255) NOT NULL,
	CONSTRAINT sys_log_pk PRIMARY KEY (id)
);


-- public.sys_menu definition

-- Drop table

-- DROP TABLE public.sys_menu;

CREATE TABLE public.sys_menu (
	id int8 NOT NULL,
	parent_id int8 NOT NULL,
	title varchar(255) NOT NULL,
	"type" int4 NOT NULL,
	icon varchar(255) NOT NULL,
	route varchar(255) NOT NULL,
	keep_alive varchar(255) NOT NULL,
	"key" varchar(255) NOT NULL,
	"path" varchar(255) NOT NULL,
	pem varchar(255) NOT NULL,
	"enable" int4 NOT NULL,
	"show" int4 NOT NULL,
	target int4 NOT NULL,
	query varchar(255) NOT NULL,
	"order" int4 NOT NULL,
	created_time timestamp NOT NULL,
	CONSTRAINT sys_menu_pkey PRIMARY KEY (id)
);


-- public.sys_role definition

-- Drop table

-- DROP TABLE public.sys_role;

CREATE TABLE public.sys_role (
	id int8 NOT NULL,
	role_name varchar(255) NOT NULL,
	role_key varchar(255) NOT NULL,
	disabled int4 NOT NULL,
	created_by varchar(255) NOT NULL,
	created_time timestamp NOT NULL,
	remark varchar(255) NOT NULL,
	CONSTRAINT sys_role_pkey PRIMARY KEY (id)
);


-- public.sys_role_and_menu definition

-- Drop table

-- DROP TABLE public.sys_role_and_menu;

CREATE TABLE public.sys_role_and_menu (
	menu_id int8 NOT NULL,
	role_id int8 NOT NULL
);


-- public.sys_user definition

-- Drop table

-- DROP TABLE public.sys_user;

CREATE TABLE public.sys_user (
	id int8 NOT NULL,
	user_name varchar(255) NOT NULL,
	nick_name varchar(255) NOT NULL,
	"password" varchar(255) NOT NULL,
	phone_number varchar(255) NOT NULL,
	remark varchar(255) NOT NULL,
	disabled int4 NOT NULL,
	created_by varchar(255) NOT NULL,
	created_time timestamp NOT NULL,
	CONSTRAINT sys_user_pkey PRIMARY KEY (id)
);


-- public.sys_user_and_role definition

-- Drop table

-- DROP TABLE public.sys_user_and_role;

CREATE TABLE public.sys_user_and_role (
	user_id int8 NOT NULL,
	role_id int8 NOT NULL
);

-- public.other_sys_info definition

-- Drop table

-- DROP TABLE public.other_sys_info;

CREATE TABLE public.other_sys_info (
	id int8 NOT NULL,
	"name" varchar(255) NOT NULL,
	app_key varchar(255) NOT NULL,
	state int4 NOT NULL,
	created_time timestamp NOT NULL,
	CONSTRAINT other_sys_info_pk PRIMARY KEY (id)
);


-- public.other_sys_log definition

-- Drop table

-- DROP TABLE public.other_sys_log;

CREATE TABLE public.other_sys_log (
	id int8 NOT NULL,
	sys_name varchar(255) NOT NULL,
	sys_target_name varchar(255) NOT NULL,
	ip varchar(255) NOT NULL,
	operation varchar(255) NOT NULL,
	request_param varchar(255) NOT NULL,
	response_param varchar(255) NOT NULL,
	operator_name varchar(255) NOT NULL,
	operate_time timestamp NOT NULL,
	execute_time int8 NOT NULL,
	execute_status bool NOT NULL,
	reason varchar(255) NOT NULL,
	"path" varchar(255) NOT NULL,
	CONSTRAINT other_sys_log_pkey PRIMARY KEY (id)
);

-- public.s7_data_point definition

-- Drop table

-- DROP TABLE public.s7_data_point;

CREATE TABLE public.s7_data_point (
	id bigserial NOT NULL,
	"name" varchar(255) NOT NULL,
	category varchar(255) NOT NULL,
	ip varchar(255) NOT NULL,
	cpu_type varchar(255) NOT NULL,
	port int4 NOT NULL,
	rack int4 NOT NULL,
	slot int4 NOT NULL,
	db int4 NOT NULL,
	start_address int4 NOT NULL,
	length int4 NOT NULL,
	data_type varchar(255) NOT NULL,
	"bit" int4 NOT NULL,
	value int4 NOT NULL,
	remark varchar(255) NOT NULL,
	hardware_type int4 NOT NULL,
	operate int4 NOT NULL,
	CONSTRAINT s7_data_point_pkey PRIMARY KEY (id)
);
-- public.protocol_log definition

-- Drop table

-- DROP TABLE public.protocol_log;

CREATE TABLE public.protocol_log (
	id bigserial NOT NULL,
	"name" varchar(255) NOT NULL,
	category varchar(255) NOT NULL,
	status bool NOT NULL,
	"time" int8 NOT NULL,
	reson varchar(255) NOT NULL,
	value varchar(255) NOT NULL,
	created_time timestamp NOT NULL,
	end_time timestamp NOT NULL,
	oper varchar(255) NOT NULL,
	CONSTRAINT protocol_log_pkey PRIMARY KEY (id)
);

-- public.modbus_data_point definition

-- Drop table

-- DROP TABLE public.modbus_data_point;

CREATE TABLE public.modbus_data_point (
	id bigserial NOT NULL,
	"name" varchar(255) NOT NULL,
	category varchar(255) NOT NULL,
	ip varchar(255) NOT NULL,
	port int4 NOT NULL,
	station_no int4 NOT NULL,
	start_address int4 NOT NULL,
	data_type varchar(255) NOT NULL,
	format varchar(255) NOT NULL,
	length int4 NOT NULL,
	read_only bool NOT NULL,
	value int4 NOT NULL,
	remark varchar(255) NOT NULL,
	hardware_type int4 NOT NULL,
	CONSTRAINT modbus_data_point_pkey PRIMARY KEY (id)
);
CREATE TABLE public.sys_setting (
	id bigserial NOT NULL,
	key_name varchar(255) NOT NULL,
	value varchar(255) NOT NULL,
	CONSTRAINT sys_setting_pkey PRIMARY KEY (id)
);

CREATE TABLE public.sys_file_info (
                                      id bigserial NOT NULL,
                                      file_name varchar(255) NOT NULL,
                                      url varchar(255) NOT NULL,
                                      "path" varchar(255) NOT NULL,
                                      suffix varchar(255) NOT NULL,
                                      file_type int4 NOT NULL,
                                      is_delete bool NOT NULL,
                                      created_time timestamp NOT NULL,
                                      "size" int8 NOT NULL,
                                      CONSTRAINT sys_file_info_pkey PRIMARY KEY (id)
);
CREATE TABLE public.opc_ua_data_point (
                                          id bigserial NOT NULL,
                                          "name" varchar(255) NOT NULL,
                                          category varchar(255) NOT NULL,
                                          endpoint varchar(255) NOT NULL,
                                          namespace_index int4 NOT NULL,
                                          identifier varchar(255) NOT NULL,
                                          access_type varchar(255) NOT NULL,
                                          data_type varchar(255) NOT NULL,
                                          remark varchar(255) NOT NULL,
                                          hardware_type int4 NOT NULL,
                                          operate int4 NOT NULL,
                                          CONSTRAINT opc_ua_data_point_pkey PRIMARY KEY (id)
);

CREATE TABLE public.interface_request_config (
                                                 config_id bigserial NOT NULL,
                                                 interface_name varchar(255) NOT NULL,
                                                 request_method int4 NOT NULL,
                                                 request_url varchar(255) NOT NULL,
                                                 request_body varchar(255) NOT NULL,
                                                 response_type int4 NOT NULL,
                                                 auth_type_key varchar(255) NOT NULL,
                                                 auth_credentials varchar(255) NOT NULL,
                                                 is_delete bool NOT NULL,
                                                 create_time timestamp NOT NULL,
                                                 update_time timestamp NOT NULL,
                                                 CONSTRAINT interface_request_config_pkey PRIMARY KEY (config_id)
);