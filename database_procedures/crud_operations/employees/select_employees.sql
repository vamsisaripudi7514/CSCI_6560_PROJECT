DELIMITER $$

CREATE PROCEDURE sp_select_employees(
    IN p_user_id INT
)
sp_select_employees:BEGIN
    DECLARE v_access BOOLEAN DEFAULT FALSE;
    DECLARE v_user_role_id INT;
    SELECT role_id INTO v_user_role_id FROM employees WHERE employee_id = p_user_id;
	IF v_user_role_id = 6 THEN
		SELECT *
          FROM employees;
          LEAVE sp_select_employees;
    END IF;
    CALL Check_User_Access(p_user_id, 'employees', 'SELECT', v_access);
    IF NOT v_access THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'User does not have permission to select employees';
    END IF;

    WITH RECURSIVE employee_hierarchy AS (
        SELECT employee_id,
               employee_name,
               manager_id,
               role_id,
               email,
               phone,
               hire_date,
               is_working,
               created_at,
               updated_at
          FROM employees
         WHERE manager_id = p_user_id
        UNION ALL
        SELECT e.employee_id,
               e.employee_name,
               e.manager_id,
               e.role_id,
               e.email,
               e.phone,
               e.hire_date,
               e.is_working,
               e.created_at,
               e.updated_at
          FROM employees e
          INNER JOIN employee_hierarchy eh ON e.manager_id = eh.employee_id
    )
    SELECT *
          FROM employee_hierarchy;

END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_select_employees;
