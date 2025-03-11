DELIMITER $$

CREATE PROCEDURE sp_project_id_validation(
    IN p_project_id INT,
    OUT p_project_exists BOOLEAN
)
BEGIN
    DECLARE v_project_count INT;

    SELECT COUNT(*) INTO v_project_count
        FROM projects
        WHERE project_id = p_project_id;
    IF v_project_count = 0 THEN
        SET p_project_exists = FALSE;
    ELSE
        SET p_project_exists = TRUE;
    END IF;

END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_project_id_validation;