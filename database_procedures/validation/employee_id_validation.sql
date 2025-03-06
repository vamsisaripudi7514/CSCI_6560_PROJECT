DELIMITER $$
CREATE PROCEDURE sp_employee_id_validation(
    IN p_employee_id INT,
    OUT p_is_valid BOOLEAN
)
BEGIN
    DECLARE v_employee_id INT;
    
    SELECT employee_id INTO v_employee_id
    FROM employees
    WHERE employee_id = p_employee_id;
    
    IF v_employee_id IS NOT NULL THEN
        SET p_is_valid = TRUE;
    ELSE
        SET p_is_valid = FALSE;
    END IF;
END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_employee_id_validation;