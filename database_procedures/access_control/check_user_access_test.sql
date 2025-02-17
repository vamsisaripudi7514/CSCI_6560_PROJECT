-- Test Cases for Director (employee_id = 10001)
-- Test SELECT permission
SET @is_allowed = FALSE;
CALL Check_User_Access(20001, 'employees', 'SELECT', @is_allowed);
SELECT @is_allowed AS 'Director SELECT Permission';  -- Expected: TRUE

-- Test INSERT permission
SET @is_allowed = FALSE;
CALL Check_User_Access(20001, 'employees', 'INSERT', @is_allowed);
SELECT @is_allowed AS 'Director INSERT Permission';  -- Expected: TRUE

-- Test UPDATE permission
SET @is_allowed = FALSE;
CALL Check_User_Access(20001, 'employees', 'UPDATE', @is_allowed);
SELECT @is_allowed AS 'Director UPDATE Permission';  -- Expected: TRUE

-- Test DELETE permission
SET @is_allowed = FALSE;
CALL Check_User_Access(20001, 'employees', 'DELETE', @is_allowed);
SELECT @is_allowed AS 'Director DELETE Permission';  -- Expected: TRUE

-- 5. Test Cases for Developer (employee_id = 50001)
-- Test SELECT permission
SET @is_allowed = FALSE;
CALL Check_User_Access(50001, 'employees', 'SELECT', @is_allowed);
SELECT @is_allowed AS 'Developer SELECT Permission';  -- Expected: TRUE

-- Test INSERT permission
SET @is_allowed = FALSE;
CALL Check_User_Access(50001, 'employees', 'INSERT', @is_allowed);
SELECT @is_allowed AS 'Developer INSERT Permission';  -- Expected: FALSE

-- Test UPDATE permission
SET @is_allowed = FALSE;
CALL Check_User_Access(50001, 'employees', 'UPDATE', @is_allowed);
SELECT @is_allowed AS 'Developer UPDATE Permission';  -- Expected: FALSE

-- Test DELETE permission
SET @is_allowed = FALSE;
CALL Check_User_Access(50001, 'employees', 'DELETE', @is_allowed);
SELECT @is_allowed AS 'Developer DELETE Permission';  -- Expected: FALSE