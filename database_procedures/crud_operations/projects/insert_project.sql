DELIMITER $$
CREATE PROCEDURE sp_insert_project(
    IN p_user_id INT,
    IN p_project_id INT,
    IN p_project_name VARCHAR(200),
    IN p_project_description TEXT,
    IN p_manager_id INT,
    IN p_start_date DATE,
    IN p_end_date DATE
)
sp_insert_project:BEGIN
    DECLARE v_is_allowed BOOLEAN DEFAULT FALSE;
    DECLARE v_new_project_id INT;
    DECLARE v_project_exists INT;
    CALL Check_User_Access(p_user_id, 'projects', 'INSERT', v_is_allowed);
    
    IF v_is_allowed THEN
        SELECT COUNT(*) INTO v_project_exists FROM projects WHERE project_id = p_project_id;
        IF v_project_exists > 0 THEN
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Project already exists.';
            LEAVE sp_insert_project;
        END IF;
        INSERT INTO projects (
            project_id,
            project_name,
            project_description,
            manager_id,
            start_date,
            end_date
        )
        VALUES (
            p_project_id,
            p_project_name,
            p_project_description,
            p_manager_id,
            p_start_date,
            p_end_date
        );
        CALL sp_audit_log(p_user_id, 'INSERT', 'projects', p_project_id);
        
        SELECT 'Project inserted successfully.' AS Message;
    ELSE
        SELECT 'Access Denied: You do not have permission to insert projects.' AS Message;
    END IF;
END$$
DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_insert_project;
