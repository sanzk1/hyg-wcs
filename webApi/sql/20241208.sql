INSERT INTO public.sys_menu
(id, parent_id, title, "type", icon, route, keep_alive, "key", "path", pem, "enable", "show", target, query, "order", created_time)
VALUES(619488838734213, 0, '系统设置', 2, 'SettingOutlined', '/setting', 'setting', '', '/src/views/admin/setting/index.vue', 'sys:setting', 1, 1, 0, '', 999, '2024-12-05 22:06:34.289');


CREATE TABLE public.sys_setting (
	id bigserial NOT NULL,
	key_name varchar(255) NOT NULL,
	value varchar(255) NOT NULL,
	CONSTRAINT sys_setting_pkey PRIMARY KEY (id)
);
