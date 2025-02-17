DELIMITER $$

CREATE PROCEDURE Check_User_Access(
    IN user_id INT, 
    IN table_name VARCHAR(100), 
    IN operation ENUM('SELECT', 'INSERT', 'UPDATE', 'DELETE'), 
    OUT is_allowed BOOLEAN
)
BEGIN
    DECLARE role INT;
    
    SELECT role_id INTO role 
    FROM employees 
    WHERE employee_id = user_id;
    
    SELECT 
        CASE 
            WHEN operation = 'SELECT' THEN can_select
            WHEN operation = 'INSERT' THEN can_insert
            WHEN operation = 'UPDATE' THEN can_update
            WHEN operation = 'DELETE' THEN can_delete
        END 
    INTO is_allowed
    FROM access_permissions 
    WHERE role_id = role 
      AND db_table_name = table_name;
    
END$$

DELIMITER ;
