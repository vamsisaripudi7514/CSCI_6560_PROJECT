SET @is_valid = FALSE;
CALL Check_Employee_Hierarchy(20001, 50001, @is_valid);
SELECT @is_valid AS 'Director accessing Developer';  -- Expected: TRUE

SET @is_valid = FALSE;
CALL Check_Employee_Hierarchy(30001, 50001, @is_valid);
SELECT @is_valid AS 'Manager accessing Developer';  -- Expected: TRUE

SET @is_valid = FALSE;
CALL Check_Employee_Hierarchy(40001, 50001, @is_valid);
SELECT @is_valid AS 'Team Leader accessing Developer';  -- Expected: TRUE

SET @is_valid = FALSE;
CALL Check_Employee_Hierarchy(50001, 50001, @is_valid);
SELECT @is_valid AS 'Developer accessing Self';  -- Expected: TRUE

SET @is_valid = FALSE;
CALL Check_Employee_Hierarchy(50001, 30001, @is_valid);
SELECT @is_valid AS 'Developer accessing Manager';  -- Expected: FALSE
