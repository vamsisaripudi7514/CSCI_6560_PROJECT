DELIMITER $$
CREATE PROCEDURE sp_update_project_mapping(
    p_user_id INT,
    p_employee_id INT,
    p_project_id INT,
    p_old_project_id INT
)
sp_update_project_mapping:BEGIN

    DECLARE v_is_allowed BOOLEAN DEFAULT FALSE;
    DECLARE v_is_role_based BOOLEAN DEFAULT FALSE;
    DECLARE v_employee_exists BOOLEAN DEFAULT FALSE;
    DECLARE v_project_exists BOOLEAN DEFAULT FALSE;
    CALL sp_employee_id_validation(p_employee_id, v_employee_exists);
    IF NOT v_employee_exists THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid employee_id';
    END IF;
    CALL sp_employee_id_validation(p_user_id, v_employee_exists);
    IF NOT v_employee_exists THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid user_id';
    END IF;
    CALL sp_project_id_validation(p_project_id, v_project_exists);
    IF NOT v_project_exists THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid project_id';
    END IF;
    CALL Check_User_Access(p_user_id, 'employee_projects', 'UPDATE', v_is_allowed);
    IF NOT v_is_allowed THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Access Denied';
    END IF;
    CALL Check_Employee_Hierarchy(p_user_id, p_employee_id, v_is_role_based);
    IF NOT v_is_role_based THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Role Based Access Denied';
    END IF;
    IF p_old_project_id IS NULL THEN
        INSERT INTO employee_projects(employee_id, project_id)
        VALUES (p_employee_id, p_project_id);
        CALL sp_audit_log(p_user_id, 'INSERT', 'employee_projects', p_employee_id);
        SELECT 'Project Mapping Created Successfully' AS Message;
        LEAVE sp_update_project_mapping;
    END IF;
    UPDATE employee_projects
    SET project_id = p_project_id
    WHERE employee_id  = p_employee_id AND project_id = p_old_project_id;
    CALL sp_audit_log(p_user_id, 'UPDATE', 'employee_projects', p_employee_id);
    SELECT 'Project Mapping Updated Successfully' AS Message;
END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_update_project_mapping;