CALL sp_get_projects(20001, @get_projects_flag);
SELECT @get_projects_flag AS 'Get Projects Flag';

CALL sp_get_projects(50001, @get_projects_flag);
SELECT @get_projects_flag AS 'Get Projects Flag';

CALL sp_get_projects(30001, @get_projects_flag);
SELECT @get_projects_flag AS 'Get Projects Flag';

CALL sp_get_projects(59999, @get_projects_flag);
SELECT @get_projects_flag AS 'Get Projects Flag';
