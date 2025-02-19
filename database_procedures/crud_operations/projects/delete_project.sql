DELIMITER $$
CREATE PROCEDURE sp_delete_project(
    IN p_user_id INT,
    IN p_project_id INT
)
BEGIN
    DECLARE v_is_allowed BOOLEAN DEFAULT FALSE;
    
    CALL Check_User_Access(p_user_id, 'projects', 'DELETE', v_is_allowed);
    
    IF v_is_allowed THEN
        DELETE FROM projects
        WHERE project_id = p_project_id;
        
        CALL sp_audit_log(p_user_id, 'DELETE', 'projects', p_project_id);
        
        SELECT 'Project deleted successfully.' AS Message;
    ELSE
        SELECT 'Access Denied: You do not have permission to delete projects.' AS Message;
    END IF;

END$$
DELIMITER ;
-- DROP PROCEDURE IF EXISTS sp_delete_project;