USE rbac_system;
DELIMITER $$

CREATE PROCEDURE sp_user_login(
    IN p_username VARCHAR(50),
    IN p_user_password VARCHAR(255),
    IN p_decryption_key VARCHAR(64),
    OUT p_user_id INT
)
BEGIN
    DECLARE user_exists BOOLEAN;
    DECLARE v_encrypted_password BLOB;
    DECLARE v_decrypted_password VARCHAR(255);

    SELECT EXISTS(SELECT 1 FROM users WHERE username = p_username) INTO user_exists;
    
    IF user_exists THEN
        SELECT user_password 
          INTO v_encrypted_password 
          FROM users 
         WHERE username = p_username 
         LIMIT 1;
         
        CALL sp_decrypt_data(v_encrypted_password, 'password',p_decryption_key, v_decrypted_password);
        
        IF v_decrypted_password = p_user_password THEN
            SELECT employee_id 
              INTO p_user_id 
              FROM users 
             WHERE username = p_username 
             LIMIT 1;
        ELSE
            SET p_user_id = -1;  
        END IF;
    ELSE
        SET p_user_id = -2;  
    END IF;
    
END$$

DELIMITER ;


-- DROP PROCEDURE IF EXISTS sp_user_login;