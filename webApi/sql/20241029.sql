INSERT INTO public.sys_menu
(id, parent_id, title, "type", icon, route, keep_alive, "key", "path", pem, "enable", "show", target, query, "order", created_time)
VALUES(606296584405381, 575130165617029, 'S7', 2, 'EnvironmentOutlined', '/dataPoint/s7', 's7', '', '/src/views/dataPoint/s7/index.vue', 'dataPoint:s7:list', 1, 1, 0, '', 99, '2024-10-29 15:27:09.072');
INSERT INTO public.sys_menu
(id, parent_id, title, "type", icon, route, keep_alive, "key", "path", pem, "enable", "show", target, query, "order", created_time)
VALUES(589967722164613, 0, '低代码开发', 1, 'CodepenOutlined', '', '', '', '', 'dev:lowcode', 1, 1, 0, '', 11, '2024-09-13 12:04:50.439');
INSERT INTO public.sys_menu
(id, parent_id, title, "type", icon, route, keep_alive, "key", "path", pem, "enable", "show", target, query, "order", created_time)
VALUES(589969538470278, 589967722164613, '任务配置', 2, 'HighlightOutlined', '/lowcode/taskSetting', 'taskSetting', '', '/src/views/lowcode/tasksetting/index.vue', 'dev:lowcode:taskSetting', 1, 1, 0, '', 1, '2024-09-13 12:12:13.873');
