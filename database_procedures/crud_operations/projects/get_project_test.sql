CALL sp_get_project(20001, 101, @get_project_flag);
SELECT @get_project_flag AS 'Get Project Flag';

CALL sp_get_project(50001, 101, @get_project_flag);
SELECT @get_project_flag AS 'Get Project Flag';

CALL sp_get_project(20001, 107, @get_project_flag);
SELECT @get_project_flag AS 'Get Project Flag';

CALL sp_get_project(200011, 107, @get_project_flag);
SELECT @get_project_flag AS 'Get Project Flag';

CALL sp_get_project(20001, 1027, @get_project_flag);
SELECT @get_project_flag AS 'Get Project Flag';