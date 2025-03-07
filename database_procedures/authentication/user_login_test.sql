-- 2. Test: Correct login with valid credentials
SET @login_user_id = 0;
CALL sp_user_login('director', 'director_pass','AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1', @login_user_id);
SELECT @login_user_id AS 'Login User ID (Wrong Password)';  
-- Expected Output: 20001

-- 3. Test: Incorrect login with wrong password

SET @login_user_id = null;
CALL sp_user_login('manager1', 'testpass','AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1', @login_user_id);
SELECT @login_user_id AS 'Login User ID (Correct Credentials)';  
-- Expected Output: -1

-- 4. Test: Non-existent user login attempt
SET @login_user_id = 0;
CALL sp_user_login('nonexistent', 'any','AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1', @login_user_id);
SELECT @login_user_id AS 'Login User ID (Non-existent User)';  
-- Expected Output: -2
