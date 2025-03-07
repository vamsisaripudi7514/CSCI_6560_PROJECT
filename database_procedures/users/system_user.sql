-- Creating a new user that only has access to stored procedures and not tables
CREATE USER 'system_user'@'localhost' IDENTIFIED BY 'MTSU2025';
GRANT EXECUTE ON PROCEDURE rbac_system.Check_Employee_Hierarchy TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.Check_User_Access TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_audit_log TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_decrypt_data TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_delete_project TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_employee_id_validation TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_encrypt_data TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_insert_employee TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_insert_project TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_insert_timesheet TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_relation_validation TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_search_employee TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_select_access_permissions TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_select_employees TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_select_project TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_select_timesheet TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_update_project TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_update_user_password TO 'system_user'@'localhost';
GRANT EXECUTE ON PROCEDURE rbac_system.sp_user_login TO 'system_user'@'localhost';

-- Delete the user
-- DROP USER 'system_user'@'localhost';
-- SHOW GRANTS FOR 'system_user'@'localhost';
