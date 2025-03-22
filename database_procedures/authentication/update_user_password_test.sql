
-- Sucess

CALL sp_update_user_password('dev1', 'dev_pass', 'new_dev_pass1', 'AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1',@output);
SET @decypted_value ='';
SET @new_encrypted_password = (SELECT user_password FROM users WHERE employee_id = 50002);
CALL sp_decrypt_data(
    @new_encrypted_password,
    'password',
    'AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1',
   @decrypted_value 
);
SELECT @decrypted_value AS 'Decrypted New Password';
select @output;

-- Failure dut to wrong old password
CALL sp_update_user_password('dev1', 'wrong_dev_pass', 'newPassword456', 'AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1',@output);
select @output;

-- Failure due to non-existent user
CALL sp_update_user_password('dev99', 'anyPassword', 'newPassword456', 'AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1',@output);
select @output;

CALL sp_update_user_password('dev2', 'dev_pass', 'new_pass', 'AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1',@output1);
select @output1;










-- CALL sp_update_user_password(20001, 'newpassword');

-- -- 3. Verify that the password was updated:
-- -- Retrieve the new encrypted password and decrypt it for verification.
-- SET @new_encrypted = (SELECT user_password FROM users WHERE employee_id = 20001 LIMIT 1);
-- SET @decrypted_password = '';
-- CALL sp_decrypt_data(@new_encrypted, 'password', @decrypted_password);

-- SELECT @decrypted_password AS 'Decrypted New Password';
-- -- Expected Output: 'newpassword'
