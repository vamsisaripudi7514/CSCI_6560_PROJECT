DELIMITER $$
CREATE PROCEDURE sp_select_timesheet(
    IN p_user_id INT,
    IN target_user_id INT
)
BEGIN 
    DECLARE v_access BOOLEAN DEFAULT FALSE;
    DECLARE v_hierarchy BOOLEAN DEFAULT FALSE;
    DECLARE v_user_role_id INT;
    CALL Check_User_Access(p_user_id, 'timesheets', 'select', v_access);
    IF NOT v_access THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Access Denied';
    END IF;
    CALL Check_Employee_Hierarchy(p_user_id, target_user_id, v_hierarchy);
    SELECT role_id INTO v_user_role_id FROM employees WHERE employee_id = p_user_id;
    IF (NOT v_hierarchy) AND v_user_role_id!=6 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Hierarchy Violation';
    END IF;
    SELECT * FROM timesheets WHERE employee_id = target_user_id;
    CALL sp_audit_log(p_user_id, 'SELECT', 'timesheets', target_user_id);

END$$
DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_select_timesheet;