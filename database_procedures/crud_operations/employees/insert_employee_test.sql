
CALL sp_insert_employee(10001, 50026, 'John Doe', 'hello@gmail.com', '123456789', 5, 40001, 50000, '2025-01-01',@message);
SELECT * FROM employees WHERE employee_id = 50026;
select @message;
-- Expected output:
-- inserted

CALL sp_insert_employee(30001, 50025, 'John Doe', 'hello@gmail.com', '123456789', 5, 40001, 50000, '2025-01-01',@message);
SELECT * FROM employees WHERE employee_id = 50025;
select @message;
-- Expected output:
-- access denied

CALL sp_insert_employee(500027, 50025, 'John Doe', 'hello@gmail.com', '123456789', 5, 40001, 50000, '2025-01-01',@message);
SELECT * FROM employees WHERE employee_id = 50025;
select @message;
-- Expected output:
-- access denied