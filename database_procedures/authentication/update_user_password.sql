DELIMITER $$

CREATE PROCEDURE sp_update_user_password(
    IN user_name VARCHAR(50),
    IN old_password VARCHAR(255),
    IN new_password VARCHAR(255),
    IN decryption_key VARCHAR(64)
)
sp_update_user_password: BEGIN
    DECLARE new_encrypted_password BLOB;
    DECLARE v_employee_id INT;
    CALL sp_user_login(user_name, old_password, decryption_key, v_employee_id);
    IF v_employee_id = -1 OR v_employee_id = -2 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid Password/Username';
        LEAVE sp_update_user_password;
    END IF;
    CALL sp_encrypt_data(new_password, 'password',decryption_key, new_encrypted_password);
    
    UPDATE users
    SET user_password = new_encrypted_password,
        updated_at = CURRENT_TIMESTAMP
    WHERE employee_id = v_employee_id;
END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_update_user_password;