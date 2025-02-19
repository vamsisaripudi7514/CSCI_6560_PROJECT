DELIMITER $$

CREATE PROCEDURE sp_search_employee(
    IN p_user_id INT,
    IN search_employee_id INT
)
BEGIN
    DECLARE v_exists INT DEFAULT 0;
    DECLARE v_access BOOLEAN DEFAULT FALSE;
    DECLARE v_hierarchy BOOLEAN DEFAULT FALSE;
    
    SELECT COUNT(*) INTO v_exists
      FROM employees
     WHERE employee_id = search_employee_id;
     
    IF v_exists = 0 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Employee not found';
    END IF;
    
    CALL Check_User_Access(p_user_id, 'employees', 'SELECT', v_access);
    IF NOT v_access THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'User does not have permission to search employees';
    END IF;
    
    CALL Check_Employee_Hierarchy(p_user_id, search_employee_id, v_hierarchy);
    IF NOT v_hierarchy THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'User is not authorized to search for this employee';
    END IF;
    
    SELECT *
      FROM employees
     WHERE employee_id = search_employee_id;
    
END$$
