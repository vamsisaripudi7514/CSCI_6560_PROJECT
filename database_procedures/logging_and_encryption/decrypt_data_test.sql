-- Test Case 1: Decrypt a Password
SET @encrypted_password = NULL;
CALL sp_encrypt_data('myPassword123', 'password', @encrypted_password);
SELECT @encrypted_password;

SET @decrypted_password = '';
CALL sp_decrypt_data(@encrypted_password, 'password', @decrypted_password);

SELECT @decrypted_password AS 'Decrypted Password';  -- Expected Output: 'myPassword123'

-- Test Case 2: Decrypt a Salary
SET @encrypted_salary = NULL;
CALL sp_encrypt_data('55000', 'salary', @encrypted_salary);

SET @decrypted_salary = '';
CALL sp_decrypt_data(@encrypted_salary, 'salary', @decrypted_salary);

SELECT @decrypted_salary AS 'Decrypted Salary';  -- Expected Output: '55000'
