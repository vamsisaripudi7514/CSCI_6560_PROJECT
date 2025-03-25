DELIMITER $$

CREATE PROCEDURE sp_get_project(
    IN p_user_id INT,
    IN p_project_id INT,
    OUT p_flag INT
)
sp_get_project:BEGIN 

    DECLARE v_is_valid_employee_id BOOLEAN DEFAULT FALSE;
    DECLARE v_is_valid_project_id BOOLEAN DEFAULT FALSE;
    DECLARE v_is_allowed BOOLEAN DEFAULT FALSE;

    CALL sp_employee_id_validation(p_user_id, v_is_valid_employee_id);
    IF NOT v_is_valid_employee_id THEN
        SELECT 'Invalid employee ID.';
        SET p_flag = -1;
        LEAVE sp_get_project;
    END IF;

    CALL sp_project_id_validation(p_project_id, v_is_valid_project_id);
    IF NOT v_is_valid_project_id THEN
        SELECT 'Invalid project ID.';
        SET p_flag = -2;
        LEAVE sp_get_project;
    END IF;

    CALL Check_User_Access(p_user_id, 'projects', 'SELECT', v_is_allowed);
    IF NOT v_is_allowed  THEN
        SELECT 'You do not have permission to access this table.';
        SET p_flag = -1;
        LEAVE sp_get_project;
    END IF;

    SET p_flag = 1;

    SELECT 
        project_id,
        project_name,
        project_description,
        manager_id,
        start_date,
        end_date
    FROM projects
    WHERE project_id = p_project_id;

END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_get_project;