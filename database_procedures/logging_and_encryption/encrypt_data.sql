DELIMITER $$
CREATE PROCEDURE sp_encrypt_data(
    IN raw_value VARCHAR(255),
    IN purpose ENUM('password','salary'),
    OUT encrypted_value BLOB
)
BEGIN
    IF purpose = 'password' THEN
        SET encrypted_value = AES_ENCRYPT(raw_value, UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1'));
    ELSEIF purpose = 'salary' THEN
        SET encrypted_value = AES_ENCRYPT(raw_value, UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613'));
    END IF;    
END$$

DELIMITER ;
