DELIMITER $$
CREATE PROCEDURE sp_select_access_permissions(
    IN p_employee_id INT,
    IN p_table_name VARCHAR(100),
    OUT p_can_insert BOOLEAN,
    OUT p_can_select BOOLEAN,
    OUT p_can_update BOOLEAN,
    OUT p_can_delete BOOLEAN
)
BEGIN
    DECLARE v_role_id INT;
    DECLARE is_valid_employee_id BOOLEAN;
    DECLARE is_valid_table_name BOOLEAN;

    CALL sp_employee_id_validation(p_employee_id, is_valid_employee_id);
    CALL sp_relation_validation(p_table_name, is_valid_table_name);
    IF is_valid_employee_id = FALSE THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid employee_id';
    END IF;

    IF is_valid_table_name = FALSE THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid table_name';
    END IF;

    SELECT role_id INTO v_role_id
    FROM employees
    WHERE employee_id = p_employee_id;

    SELECT can_insert, can_select, can_update, can_delete
    INTO p_can_insert, p_can_select, p_can_update, p_can_delete
    FROM access_permissions
    WHERE role_id = v_role_id
	AND db_table_name = p_table_name;
END$$
DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_select_access_permissions;