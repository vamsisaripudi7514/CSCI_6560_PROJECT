CALL sp_project_id_validation(101, @project_exists);
SELECT @project_exists;
-- 1

CALL sp_project_id_validation(100, @project_exists);
SELECT @project_exists;
-- 0

CALL sp_project_id_validation(102, @project_exists);
SELECT @project_exists;
-- 1