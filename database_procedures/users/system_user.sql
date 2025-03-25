-- Creating a new user that only has access to stored procedures and not tables
CREATE USER 'system_user'@'localhost' IDENTIFIED BY 'MTSU2025';

GRANT EXECUTE ON PROCEDURE rbac_system.sp_user_login TO 'system_user'@'localhost'; -- c
GRANT EXECUTE ON PROCEDURE rbac_system.sp_update_user_password TO 'system_user'@'localhost'; -- c
GRANT EXECUTE ON PROCEDURE rbac_system.sp_button_visibility TO 'system_user'@'localhost'; 
GRANT EXECUTE ON PROCEDURE rbac_system.sp_select_employees TO 'system_user'@'localhost'; -- xc
GRANT EXECUTE ON PROCEDURE rbac_system.sp_get_employee_details TO 'system_user'@'localhost'; -- c
GRANT EXECUTE ON PROCEDURE rbac_system.sp_update_employee TO 'system_user'@'localhost'; -- c
GRANT EXECUTE ON PROCEDURE rbac_system.sp_insert_employee To 'system_user'@'localhost'; -- c
GRANT EXECUTE ON PROCEDURE rbac_system.sp_select_timesheet TO 'system_user'@'localhost'; -- c
GRANT EXECUTE ON PROCEDURE rbac_system.sp_insert_project TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_select_project TO 'system_user'@'localhost'; -- c
GRANT EXECUTE ON PROCEDURE rbac_system.sp_get_projects TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_get_project TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_update_project TO 'system_user'@'localhost'; -- c
GRANT EXECUTE ON PROCEDURE rbac_system.sp_project_mapping_update_list TO 'system_user'@'localhost'; -- c
GRANT EXECUTE ON PROCEDURE rbac_system.sp_update_project_mapping TO 'system_user'@'localhost'; -- c
GRANT EXECUTE ON PROCEDURE rbac_system.sp_select_audit_logs TO 'system_user'@'localhost'; -- c
GRANT EXECUTE ON PROCEDURE rbac_system.sp_get_user_audit_logs TO 'system_user'@'localhost'; -- c



-- Delete the user
-- DROP USER IF EXISTS 'system_user'@'localhost';
-- SHOW GRANTS FOR 'system_user'@'localhost';

-- CREATE PROCEDURE sp_button_visibility(
--     IN p_employee_id INT
-- )