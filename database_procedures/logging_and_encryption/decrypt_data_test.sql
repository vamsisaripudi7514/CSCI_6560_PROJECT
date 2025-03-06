-- Test Case 1: Decrypt a Password
SET @encrypted_password = NULL;
CALL sp_encrypt_data('myPassword123', 'password','AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1' , @encrypted_password);
SELECT @encrypted_password;

SET @decrypted_password = '';
CALL sp_decrypt_data(@encrypted_password, 'password','AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1', @decrypted_password);

SELECT @decrypted_password AS 'Decrypted Password';  -- Expected Output: 'myPassword123'

-- Test Case 2: Decrypt a Salary
SET @encrypted_salary = NULL;
CALL sp_encrypt_data('55000', 'salary', 'B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613', @encrypted_salary);

SET @decrypted_salary = '';
CALL sp_decrypt_data(@encrypted_salary,'salary', 'B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613',  @decrypted_salary);

SELECT @decrypted_salary AS 'Decrypted Salary';  -- Expected Output: '55000'
