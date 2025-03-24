DELIMITER $$

CREATE PROCEDURE sp_update_employee(
    IN p_user_id INT,
    IN p_employee_id INT,
    IN p_employee_name VARCHAR(100),
    IN p_employee_email VARCHAR(150),
    IN p_employee_phone VARCHAR(15),
    IN p_employee_role_id INT,
    IN p_employee_manager_id INT,
    IN p_employee_salary VARCHAR(255),
    IN p_is_working BOOLEAN,
    IN p_encryption_key VARCHAR(64)
)
sp_update_employee:BEGIN
    DECLARE v_user_role_id INT;
    DECLARE v_is_allowed BOOLEAN DEFAULT FALSE;
    DECLARE v_is_role_access BOOLEAN DEFAULT FALSE;
    DECLARE v_is_valid_employee_id BOOLEAN DEFAULT FALSE;
    DECLARE v_encrypted_salary BLOB;
    SELECT role_id INTO v_user_role_id FROM employees WHERE employee_id = p_user_id;
    CALL sp_employee_id_validation(p_employee_id, v_is_valid_employee_id);
    IF v_is_valid_employee_id = FALSE AND p_employee_id != 1 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid employee_id';
        LEAVE sp_update_employee;
    END IF;
    CALL Check_User_Access(p_user_id, 'employees', 'UPDATE', v_is_role_access);
    IF NOT v_is_role_access AND v_user_role_id!=6 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Access Denied: You do not have permission to update employees';
        LEAVE sp_update_employee;
    END IF;
    CALL Check_Employee_Hierarchy(p_user_id, p_employee_id, v_is_allowed);
    IF (NOT v_is_allowed) AND v_user_role_id!=6 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Access Denied: You are not authorized to update this employee';
        LEAVE sp_update_employee;
    END IF;
    CALL sp_encrypt_data(p_employee_salary, 'salary', p_encryption_key, v_encrypted_salary);
    UPDATE employees e
    SET e.employee_name = p_employee_name,
        e.email = p_employee_email,
        e.phone = p_employee_phone,
        e.role_id = p_employee_role_id,
        e.manager_id = p_employee_manager_id,
        e.encrypted_salary = v_encrypted_salary,
        e.is_working = p_is_working,
        e.updated_at = NOW()
    WHERE e.employee_id = p_employee_id;
    CALL sp_audit_log(p_user_id, 'UPDATE', 'employees', p_employee_id);
	SELECT "Employee Upadated Successfully";

END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_update_employee;