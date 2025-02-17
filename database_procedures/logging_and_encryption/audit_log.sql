DELIMITER $$
CREATE PROCEDURE sp_audit_log(
    IN user_id INT,
    IN db_action VARCHAR(255),
    IN db_table_name VARCHAR(100),
    IN record_id INT
)
BEGIN
    INSERT INTO audit_logs (user_id, db_action, db_table_name, record_id)
    VALUES (user_id, db_action, db_table_name, record_id);

END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_audit_log;