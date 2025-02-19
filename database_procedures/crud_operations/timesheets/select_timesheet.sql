DELIMITER $$
CREATE PROCEDURE sp_select_timesheet(
    IN p_user_id INT,
    IN target_user_id INT
)
BEGIN 
    DECLARE v_access BOOLEAN DEFAULT FALSE;
    DECLARE v_hierarchy BOOLEAN DEFAULT FALSE;
    CALL Check_User_Access(p_user_id, 'timesheets', 'select', v_access);
    IF NOT v_access THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Access Denied';
    END IF;
    CALL Check_Employee_Hierarchy(p_user_id, target_user_id, v_hierarchy);
    IF NOT v_hierarchy THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Hierarchy Violation';
    END IF;
    SELECT * FROM timesheets WHERE employee_id = target_user_id;

END$$
DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_select_timesheet;