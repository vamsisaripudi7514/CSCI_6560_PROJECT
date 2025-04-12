DELIMITER $$

CREATE PROCEDURE sp_encrypt_data(
    IN raw_value VARCHAR(255),
    IN purpose ENUM('password', 'salary'),
    IN encryption_key VARCHAR(64),
    OUT encrypted_value BLOB
)
BEGIN
    DECLARE temp_value BLOB;

    SELECT AES_ENCRYPT(raw_value, UNHEX(encryption_key)) INTO temp_value;

    SET encrypted_value = temp_value;
END$$

DELIMITER ;


-- DROP PROCEDURE IF EXISTS sp_encrypt_data;