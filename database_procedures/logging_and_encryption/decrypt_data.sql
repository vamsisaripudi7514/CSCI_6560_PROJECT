DELIMITER $$

CREATE PROCEDURE sp_decrypt_data(
    IN encrypted_value BLOB,
    IN purpose ENUM('password','salary'),
    IN decryption_key VARCHAR(64),
    OUT decrypted_value VARCHAR(255)
)
BEGIN
    DECLARE temp_value VARCHAR(255);
        SELECT CAST(AES_DECRYPT(encrypted_value, UNHEX(decryption_key)) AS CHAR(255)) INTO temp_value;
    SET decrypted_value = temp_value;
END$$

DELIMITER ;

-- DROP PROCEDURE IF EXISTS sp_decrypt_data;
