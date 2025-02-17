-- 2. Test: Correct login with valid credentials
SET @login_user_id = null;
CALL sp_user_login('manager1', 'testpass', @login_user_id);
SELECT @login_user_id AS 'Login User ID (Correct Credentials)';  
-- Expected Output: 10010

-- 3. Test: Incorrect login with wrong password
SET @login_user_id = 0;
CALL sp_user_login('director', 'director_pass', @login_user_id);
SELECT @login_user_id AS 'Login User ID (Wrong Password)';  
-- Expected Output: -1

-- 4. Test: Non-existent user login attempt
SET @login_user_id = 0;
CALL sp_user_login('nonexistent', 'any', @login_user_id);
SELECT @login_user_id AS 'Login User ID (Non-existent User)';  
-- Expected Output: -2
