-- This methods is for internally accessing the employee details when the user selects to the view the details of the employee.
DELIMITER $$
CREATE PROCEDURE sp_get_employee_details(
    IN p_user_id INT,
    IN p_employee_id INT,
    IN p_decryption_key VARCHAR(64)
)
sp_get_employee_details: BEGIN
    DECLARE v_access BOOLEAN DEFAULT FALSE;
    DECLARE v_hierarchy BOOLEAN DEFAULT FALSE;
    DECLARE v_user_role_id INT;
    DECLARE v_project_id INT;
    DECLARE v_employee_name VARCHAR(50);
    DECLARE v_email VARCHAR(50);
    DECLARE v_phone VARCHAR(50);
    DECLARE v_role_id INT;
    DECLARE v_designated_role VARCHAR(50);
    DECLARE v_salary BLOB;
    DECLARE v_decrypted_salary VARCHAR(50);
    DECLARE v_hire_date DATE;
    DECLARE v_manager_id INT;
    DECLARE v_project_name VARCHAR(50);
    DECLARE v_project_description TEXT;
    DECLARE v_start_date DATE;
    DECLARE v_end_date DATE;
    DECLARE v_is_valid_user BOOLEAN DEFAULT FALSE;
    CALL sp_employee_id_validation(p_user_id, v_is_valid_user);
    IF NOT v_is_valid_user THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid employee_id';
        LEAVE sp_get_employee_details;
    END IF;
    CALL sp_employee_id_validation(p_employee_id, v_is_valid_user);
    IF NOT v_is_valid_user THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid employee_id';
        LEAVE sp_get_employee_details;
    END IF;
    CALL Check_User_Access(p_user_id, 'employees', 'SELECT', v_access);
    SELECT role_id INTO v_user_role_id FROM employees WHERE employee_id = p_user_id;
    IF (NOT v_access)  AND v_user_role_id != 6 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Access Denied';
        LEAVE sp_get_employee_details;
    END IF;
    CALL Check_Employee_Hierarchy(p_user_id, p_employee_id, v_hierarchy);
    IF (NOT v_hierarchy) AND v_user_role_id!=6 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Hierarchy Violation';
        LEAVE sp_get_employee_details;
    END IF;
    SELECT project_id INTO v_project_id FROM employee_projects WHERE employee_id = p_employee_id;
    -- Requested Result;
    SELECT e.employee_name as name, e.email, e.phone,
             e.encrypted_salary as salary, e.hire_date, e.role_id INTO
    v_employee_name, v_email, v_phone, v_salary, v_hire_date, v_role_id
    FROM employees e WHERE e.employee_id = p_employee_id;
    SELECT r.role_name INTO v_designated_role FROM user_roles r WHERE r.role_id = v_role_id;
    SELECT p.project_name,p.manager_id, p.project_description, p.start_date, p.end_date
    INTO v_project_name, v_manager_id, v_project_description, v_start_date, v_end_date
    FROM projects p WHERE p.project_id = v_project_id;

    IF v_user_role_id = 6 OR v_user_role_id = 2 OR v_user_role_id = 1 THEN
        CALL sp_decrypt_data(v_salary, 'salary',p_decryption_key, v_decrypted_salary);
        SELECT v_employee_name as name,
              v_email as email,
              v_phone as phone,
              v_designated_role as role,
              v_decrypted_salary as salary,
              v_hire_date as hire_date,
              v_manager_id as manager_id,
              v_project_name as project_name,
              v_project_description as project_description,
              v_start_date as start_date,
              v_end_date as end_date;
        CALL sp_audit_log(p_user_id, 'SELECT', 'employees', p_employee_id);
        LEAVE sp_get_employee_details;
    END IF;

    SELECT v_employee_name as name,
              v_email as email,
              v_phone as phone,
              v_designated_role as role,
              v_salary as salary,
              v_hire_date as hire_date,
              v_manager_id as manager_id,
              v_project_name as project_name,
              v_project_description as project_description,
              v_start_date as start_date,
              v_end_date as end_date;
    CALL sp_audit_log(p_user_id, 'SELECT', 'employees', p_employee_id);
END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_get_employee_details;