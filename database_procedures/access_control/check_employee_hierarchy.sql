DELIMITER $$

CREATE PROCEDURE Check_Employee_Hierarchy(
    IN user_id INT,
    IN target_employee_id INT,
    OUT is_valid BOOLEAN
)
BEGIN
    DECLARE current_manager INT;

    SET is_valid = FALSE;

    IF user_id = target_employee_id THEN
        SET is_valid = TRUE;
    ELSE
        SELECT manager_id INTO current_manager
        FROM employees
        WHERE employee_id = target_employee_id;
        
        label_loop: WHILE current_manager IS NOT NULL DO
            IF current_manager = user_id THEN
                SET is_valid = TRUE;
                LEAVE label_loop;
            END IF;
            
            SELECT manager_id INTO current_manager
            FROM employees
            WHERE employee_id = current_manager;
        END WHILE label_loop;
    END IF;
    
END$$

DELIMITER ;
