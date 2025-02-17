-- Test 1: Director (10001) accessing Developer (50001)
SET @is_valid = FALSE;
CALL Check_Employee_Hierarchy(20001, 50001, @is_valid);
SELECT @is_valid AS 'Director accessing Developer';  -- Expected: TRUE

-- Test 2: Manager (30001) accessing Developer (50001)
SET @is_valid = FALSE;
CALL Check_Employee_Hierarchy(30001, 50001, @is_valid);
SELECT @is_valid AS 'Manager accessing Developer';  -- Expected: TRUE

-- Test 3: Team Leader (40001) accessing Developer (50001)
SET @is_valid = FALSE;
CALL Check_Employee_Hierarchy(40001, 50001, @is_valid);
SELECT @is_valid AS 'Team Leader accessing Developer';  -- Expected: TRUE

-- Test 4: Developer (50001) accessing own record
SET @is_valid = FALSE;
CALL Check_Employee_Hierarchy(50001, 50001, @is_valid);
SELECT @is_valid AS 'Developer accessing Self';  -- Expected: TRUE

-- Test 5: Developer (50001) trying to access Manager (30001)
SET @is_valid = FALSE;
CALL Check_Employee_Hierarchy(50001, 30001, @is_valid);
SELECT @is_valid AS 'Developer accessing Manager';  -- Expected: FALSE
