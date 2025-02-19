DELIMITER $$
CREATE PROCEDURE sp_select_project(
    IN p_user_id INT,
    IN target_id INT
)
BEGIN
    DECLARE v_is_allowed BOOLEAN DEFAULT FALSE;
    DECLARE v_is_valid BOOLEAN DEFAULT FALSE;
    DECLARE v_project_id INT;
    DECLARE done INT DEFAULT 0;
    DECLARE cur1 CURSOR FOR SELECT project_id FROM temp_projects;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
    
    CALL Check_User_Access(p_user_id, 'projects', 'SELECT', v_is_allowed);
    CALL Check_Employee_Hierarchy(p_user_id, target_id, v_is_valid);
    
    CREATE TEMPORARY TABLE IF NOT EXISTS temp_projects (
        project_id INT,
        project_name VARCHAR(200),
        project_description TEXT,
        manager_id INT,
        start_date DATE,
        end_date DATE
    );
    
    IF v_is_allowed AND v_is_valid THEN
        DELETE FROM temp_projects;
        
        INSERT INTO temp_projects
            SELECT 
                project_id,
                project_name,
                project_description,
                manager_id,
                start_date,
                end_date
            FROM projects
            WHERE project_id IN (
                SELECT project_id FROM employee_projects WHERE employee_id = target_id
            );
        
        OPEN cur1;
        read_loop: LOOP
            FETCH cur1 INTO v_project_id;
            IF done THEN
                LEAVE read_loop;
            END IF;
            CALL sp_audit_log(p_user_id, 'SELECT', 'projects', v_project_id);
        END LOOP;
        CLOSE cur1;
        
        SELECT * FROM temp_projects;
    ELSE
        SELECT 'You do not have permission to access this table.' AS Message;
    END IF;
    
    DROP TEMPORARY TABLE IF EXISTS temp_projects;
    
END$$
DELIMITER ;



-- DROP PROCEDURE IF EXISTS sp_select_project;