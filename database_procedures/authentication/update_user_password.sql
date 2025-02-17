DELIMITER $$

CREATE PROCEDURE sp_update_user_password(
    IN user_id INT,
    IN new_password VARCHAR(255)
)
BEGIN
    DECLARE new_encrypted_password BLOB;
    
    CALL sp_encrypt_data(new_password, 'password', new_encrypted_password);
    
    UPDATE users
    SET user_password = new_encrypted_password,
        updated_at = CURRENT_TIMESTAMP
    WHERE employee_id = user_id;
END$$

DELIMITER ;
