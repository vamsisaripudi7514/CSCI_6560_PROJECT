DELIMITER $$
CREATE PROCEDURE sp_get_user_audit_logs(
    IN p_user_id INT,
    IN p_search_user_id INT
)
BEGIN

    DECLARE v_access BOOLEAN DEFAULT FALSE;
    DECLARE v_hierarchy BOOLEAN DEFAULT FALSE;
    
    CALL Check_User_Access(p_user_id, 'audit_logs', 'SELECT', v_access);
    
    IF NOT v_access THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'User does not have permission to view audit logs';
    END IF;
    
    SELECT *
      FROM audit_logs
     WHERE user_id = p_search_user_id;

END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_get_user_audit_logs;