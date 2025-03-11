DELIMITER $$

CREATE PROCEDURE sp_button_visibility(
    IN p_employee_id INT
)
BEGIN
    DECLARE v_role_id INT;
    SELECT role_id INTO v_role_id FROM employees WHERE employee_id = p_employee_id;
    -- CREATE A TABLE THAT HAS THE access_permissions FOR THE ROLE OF THE EMPLOYEE AND RETURN IT
    SELECT db_table_name, can_select, can_insert, can_update, can_delete 
    FROM access_permissions 
    WHERE role_id = v_role_id;

END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_button_visibility;