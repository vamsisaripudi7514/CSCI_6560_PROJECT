DELIMITER $$
CREATE PROCEDURE sp_relation_validation(
    IN p_table_name VARCHAR(100),
    OUT p_is_valid BOOLEAN
)
BEGIN
    DECLARE v_table_name VARCHAR(100);
    
    SELECT DISTINCT db_table_name INTO v_table_name
    FROM access_permissions
    WHERE db_table_name = p_table_name;
    
    IF v_table_name IS NOT NULL THEN
        SET p_is_valid = TRUE;
    ELSE
        SET p_is_valid = FALSE;
    END IF;
END$$
DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_relation_validation;