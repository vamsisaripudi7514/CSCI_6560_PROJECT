
CALL sp_update_user_password(20001, 'newpassword');

-- 3. Verify that the password was updated:
-- Retrieve the new encrypted password and decrypt it for verification.
SET @new_encrypted = (SELECT user_password FROM users WHERE employee_id = 20001 LIMIT 1);
SET @decrypted_password = '';
CALL sp_decrypt_data(@new_encrypted, 'password', @decrypted_password);

SELECT @decrypted_password AS 'Decrypted New Password';
-- Expected Output: 'newpassword'
