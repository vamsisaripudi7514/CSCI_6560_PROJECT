DELIMITER $$
CREATE PROCEDURE sp_insert_timesheet(
    IN p_user_id INT,
    IN p_project_id INT,
    IN p_hours_logged DECIMAL(5,2),
    IN p_log_date DATE
)
BEGIN
    DECLARE v_is_allowed BOOLEAN DEFAULT FALSE;
    
    CALL Check_User_Access(p_user_id, 'timesheets', 'INSERT', v_is_allowed);
    
    IF v_is_allowed THEN
        INSERT INTO timesheets (employee_id, project_id, hours_logged, log_date)
        VALUES (p_user_id, p_project_id, p_hours_logged, p_log_date);
        CALL sp_audit_log(p_user_id, 'INSERT', 'timesheets', LAST_INSERT_ID());
        
        SELECT 'Timesheet inserted successfully.' AS Message;
    ELSE
        SELECT 'Access Denied: You do not have permission to insert timesheets.' AS Message;
    END IF;
    
END$$
DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_insert_timesheet;