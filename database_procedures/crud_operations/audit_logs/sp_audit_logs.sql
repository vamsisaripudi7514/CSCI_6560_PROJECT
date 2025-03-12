DELIMITER $$
CREATE PROCEDURE sp_select_audit_logs(
    IN p_user_id INT
)
BEGIN 

    DECLARE v_access BOOLEAN DEFAULT FALSE;
    
    CALL Check_User_Access(p_user_id, 'audit_logs', 'SELECT', v_access);
    
    IF v_access THEN
        SELECT * 
          FROM audit_logs;
    ELSE
        SELECT 'Access Denied: You do not have permission to view audit logs.' AS Message;
    END IF;

END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_select_audit_logs;