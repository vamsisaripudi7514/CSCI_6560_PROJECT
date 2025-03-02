-- Test Case 1: Encrypt a password
SET @enc_val = NULL;
CALL sp_encrypt_data('myPassword123', 'password', 'AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1', @enc_val);
SELECT @enc_val AS 'Encrypted Password';
-- Verify by decrypting the value using the corresponding key
SELECT CAST(AES_DECRYPT(@enc_val, UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')) AS CHAR(255)) AS 'Decrypted Password';

-- Test Case 2: Encrypt a salary
SET @enc_val = NULL;
CALL sp_encrypt_data('55000', 'salary', 'B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613', @enc_val);
SELECT @enc_val AS 'Encrypted Salary';
-- Verify by decrypting the value using the corresponding key
SELECT CAST(AES_DECRYPT(@enc_val, UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')) AS CHAR(255)) AS 'Decrypted Salary';