DELIMITER $$
CREATE PROCEDURE sp_insert_employee(
    p_user_id INT,
    p_employee_id INT,
    p_employee_name VARCHAR(100),
    p_employee_email VARCHAR(100),
    p_employee_phone VARCHAR(15),
    p_employee_role_id INT,
    p_employee_manager_id INT,
    p_employee_salary VARCHAR(255),
    p_employee_hire_date DATE,
    p_encryption_key VARCHAR(64),
    OUT p_message VARCHAR(100)  
)
sp_insert_employee: BEGIN
    DECLARE v_is_allowed BOOLEAN DEFAULT FALSE;
    DECLARE v_is_valid_employee_id BOOLEAN DEFAULT FALSE;
    DECLARE v_encrypted_salary BLOB;
    CALL sp_employee_id_validation(p_user_id, v_is_valid_employee_id);
    IF v_is_valid_employee_id = FALSE THEN
        SET p_message = 'Invalid employee_id';
        LEAVE sp_insert_employee;
    END IF;
    CALL check_user_access(p_user_id, 'employees', 'INSERT', v_is_allowed);
    IF v_is_allowed THEN
        CALL sp_encrypt_data(p_employee_salary, 'salary',p_encryption_key, v_encrypted_salary);
        INSERT INTO employees (employee_id, employee_name, email, phone, role_id, manager_id, encrypted_salary, hire_date)
        VALUES (p_employee_id, p_employee_name, p_employee_email, p_employee_phone, p_employee_role_id, p_employee_manager_id, v_encrypted_salary, p_employee_hire_date);
        CALL sp_audit_log(p_user_id, 'INSERT', 'employees', LAST_INSERT_ID());
        SET p_message = 'Employee inserted successfully.';
    ELSE
        SET p_message = 'Access Denied: You do not have permission to insert employees.';
    END IF;
END$$
DELIMITER ;
-- DROP PROCEDURE IF EXISTS sp_insert_employee;