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