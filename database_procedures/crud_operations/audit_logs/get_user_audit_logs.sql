DELIMITER $$
CREATE PROCEDURE sp_get_user_audit_logs(
    IN p_user_id INT,
    IN p_search_user_id INT
)
BEGIN

    DECLARE v_access BOOLEAN DEFAULT FALSE;
    DECLARE v_hierarchy BOOLEAN DEFAULT FALSE;
    DECLARE v_is_valid_employee BOOLEAN DEFAULT FALSE;
    CALL Check_User_Access(p_user_id, 'audit_logs', 'SELECT', v_access);

    CALL sp_employee_id_validation( p_search_user_id, v_is_valid_employee);
    
    IF NOT v_is_valid_employee THEN
        SELECT "Invalid Employee ID";
    END IF;

    CALL sp_employee_id_validation( p_user_id, v_is_valid_employee);
    
    IF NOT v_is_valid_employee THEN
        SELECT "Invalid User ID";
    END IF;
    IF NOT v_access THEN
       Select "Access Denied";
    END IF;
    
    SELECT *
      FROM audit_logs
     WHERE user_id = p_search_user_id;

END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_get_user_audit_logs;