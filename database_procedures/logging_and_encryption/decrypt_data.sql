DELIMITER $$

CREATE PROCEDURE sp_decrypt_data(
    IN encrypted_value BLOB,
    IN purpose ENUM('password','salary'),
    OUT decrypted_value VARCHAR(255)
)
BEGIN
    IF purpose = 'password' THEN
        SET decrypted_value = CAST(
            AES_DECRYPT(
                encrypted_value, 
                UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')
            ) AS CHAR(255)
        );
    ELSEIF purpose = 'salary' THEN
        SET decrypted_value = CAST(
            AES_DECRYPT(
                encrypted_value, 
                UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')
            ) AS CHAR(255)
        );
    END IF;
END$$

DELIMITER ;
