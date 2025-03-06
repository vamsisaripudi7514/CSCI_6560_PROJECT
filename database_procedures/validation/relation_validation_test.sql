CALL sp_relation_validation('employees', @is_valid);
SELECT @is_valid;
-- Expected output:
-- 1

CALL sp_relation_validation('projects', @is_valid);
SELECT @is_valid;
-- Expected output:
-- 1

CALL sp_relation_validation('access_permissions', @is_valid);
SELECT @is_valid;
-- Expected output:
-- 1

CALL sp_relation_validation('audit_logss', @is_valid);
SELECT @is_valid;
-- Expected output:
-- 0

CALL sp_relation_validation('user_roles', @is_valid);
SELECT @is_valid;   
-- Expected output:
-- 1

CALL sp_relation_validation('access_roles', @is_valid);
SELECT @is_valid;   
-- Expected output:
-- 0