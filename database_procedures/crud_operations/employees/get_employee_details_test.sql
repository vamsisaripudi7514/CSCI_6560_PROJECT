
-- Successful test case
CALL sp_get_employee_details(10001, 50001, 'B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613');

-- Successful Test Case
CALL sp_get_employee_details(20001, 50001, 'B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613');

-- Successful Test Case
CALL sp_get_employee_details(30001, 50001, 'B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613');

-- Successful Test Case
CALL sp_get_employee_details(30001, 40001, 'B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613');


-- Failure test case (Hierarchy Denial)
CALL sp_get_employee_details(50001, 50002, 'B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613'); 

-- Failure test case (Invalid User)
CALL sp_get_employee_details(59999, 50001, 'B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613');