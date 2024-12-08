INSERT INTO public.sys_menu
(id, parent_id, title, "type", icon, route, keep_alive, "key", "path", pem, "enable", "show", target, query, "order", created_time)
VALUES(619488838734213, 0, '系统设置', 2, 'SettingOutlined', '/setting', 'setting', '', '/src/views/admin/setting/index.vue', 'sys:setting', 1, 1, 0, '', 999, '2024-12-05 22:06:34.289');


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