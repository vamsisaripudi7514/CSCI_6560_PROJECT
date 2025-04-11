DELIMITER $$

CREATE PROCEDURE sp_get_projects(
    IN p_user_id INT,
    OUT p_flag INT
)
sp_get_projects:BEGIN

    DECLARE v_is_valid_employee_id BOOLEAN DEFAULT FALSE;
    DECLARE v_is_allowed BOOLEAN DEFAULT FALSE;
    DECLARE v_role_id INT;

    CALL sp_employee_id_validation(p_user_id, v_is_valid_employee_id);
    IF NOT v_is_valid_employee_id THEN
        SELECT 'Invalid employee ID.';
        SET p_flag = -1;
        LEAVE sp_get_projects;
    END IF;

    SELECT role_id INTO v_role_id
      FROM employees
     WHERE employee_id = p_user_id;

     IF v_role_id = 2 THEN
        SELECT * from projects;
        SET p_flag = 1;
        CALL sp_audit_log(p_user_id, 'SELECT', 'projects', 0);
    LEAVE sp_get_projects;
    END IF;

    CALL Check_User_Access(p_user_id, 'projects', 'SELECT', v_is_allowed);
    IF NOT v_is_allowed  THEN
        SELECT 'You do not have permission to access this table.';
        SET p_flag = -1;
        LEAVE sp_get_projects;
    END IF;
    SET p_flag = 1;
    WITH RECURSIVE subordinates AS (
        SELECT employee_id 
        FROM employees 
        WHERE employee_id = p_user_id
        UNION ALL
        SELECT e.employee_id
        FROM employees e
        JOIN subordinates s ON e.manager_id = s.employee_id
    )
    -- Return distinct projects assigned to any employee in the hierarchy.
    SELECT DISTINCT p.*
    FROM projects p
    JOIN employee_projects ep ON p.project_id = ep.project_id
    JOIN subordinates s ON ep.employee_id = s.employee_id

    UNION

-- Projects where the manager_id is p_user_id (direct command)
SELECT DISTINCT p.*
FROM projects p
WHERE p.manager_id = p_user_id;

    
    
END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_get_projects;
-- use rbac_system;