CALL sp_employee_id_validation(10001, @is_valid);
SELECT @is_valid;
-- Expected output:
-- 1

CALL sp_employee_id_validation(20001, @is_valid);
SELECT @is_valid;
-- Expected output:
-- 1

CALL sp_employee_id_validation(30001, @is_valid);
SELECT @is_valid;
-- Expected output:
-- 1

CALL sp_employee_id_validation(400010, @is_valid);
SELECT @is_valid;
-- Expected output:
-- 0

CALL sp_employee_id_validation(50025, @is_valid);
SELECT @is_valid;
-- Expected output:
-- 0