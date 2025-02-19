DELIMITER $$
CREATE PROCEDURE sp_update_project(
    IN p_user_id INT,
    IN p_project_id INT,
    IN p_project_name VARCHAR(200),
    IN p_project_description TEXT,
    IN p_manager_id INT,
    IN p_start_date DATE,
    IN p_end_date DATE
)
BEGIN
    DECLARE v_is_allowed BOOLEAN DEFAULT FALSE;
    
    CALL Check_User_Access(p_user_id, 'projects', 'UPDATE', v_is_allowed);
    
    IF v_is_allowed THEN
        UPDATE projects
        SET project_name = p_project_name,
            project_description = p_project_description,
            manager_id = p_manager_id,
            start_date = p_start_date,
            end_date = p_end_date,
            updated_at = CURRENT_TIMESTAMP
        WHERE project_id = p_project_id;
        
        CALL sp_audit_log(p_user_id, 'UPDATE', 'projects', p_project_id);
        
        SELECT 'Project updated successfully.' AS Message;
    ELSE
        SELECT 'Access Denied: You do not have permission to update projects.' AS Message;
    END IF;
    
END$$
DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_update_project;