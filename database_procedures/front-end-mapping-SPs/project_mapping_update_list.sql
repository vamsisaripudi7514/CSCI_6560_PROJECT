DELIMITER $$

CREATE PROCEDURE sp_project_mapping_update_list(
    IN p_user_id INT
)
sp_project_mapping_update_list:BEGIN 
    DECLARE v_exists INT DEFAULT FALSE;
    DECLARE v_role_id INT;
    SELECT role_id INTO v_role_id FROM employees WHERE employee_id = p_user_id;
    IF v_role_id != 2 AND v_role_id != 3 THEN
		SIGNAL SQLSTATE '45000'
			SET MESSAGE_TEXT = 'Invalid User Role';
	END IF;
    CALL sp_employee_id_validation(p_user_id, v_exists);
    IF NOT v_exists THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'User not found';
    END IF;
	IF v_role_id = 2 THEN
		SELECT DISTINCT project_id, project_name from projects;
        LEAVE sp_project_mapping_update_list;
	END IF;
    WITH RECURSIVE employee_hierarchy AS (
        SELECT employee_id, manager_id
        FROM employees
        WHERE manager_id = p_user_id
        UNION ALL
        SELECT e.employee_id, e.manager_id
        FROM employees e
        INNER JOIN employee_hierarchy eh ON e.manager_id = eh.employee_id
    )
		SELECT DISTINCT p.project_id, p.project_name
		FROM projects p
		INNER JOIN employee_hierarchy eh ON p.manager_id = eh.manager_id;
END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_project_mapping_update_list;