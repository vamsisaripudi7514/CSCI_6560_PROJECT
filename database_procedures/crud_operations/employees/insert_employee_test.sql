
CALL sp_insert_employee(10001, 50027, 'John Doe', 'hello1@gmail.com', '123456789', 5, 40001, 50000, '2025-01-01','B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613',@message);
SELECT * FROM employees WHERE employee_id = 50026;
select @message;
select * from employees where employee_id = 50027;
delete from employees where employee_id = 50027;
-- Expected output:
-- inserted

CALL sp_insert_employee(30001, 50025, 'John Doe', 'hello@gmail.com', '123456789', 5, 40001, 50000, '2025-01-01','B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613',@message);
SELECT * FROM employees WHERE employee_id = 50025;
select @message;
-- Expected output:
-- access denied

CALL sp_insert_employee(50027, 50025, 'John Doe', 'hello@gmail.com', '123456789', 5, 40001, 50000, '2025-01-01','B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613',@message);
SELECT * FROM employees WHERE employee_id = 50025;
select @message;
-- Expected output:
-- access denied