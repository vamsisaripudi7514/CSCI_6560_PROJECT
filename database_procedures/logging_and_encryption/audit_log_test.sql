CALL sp_audit_log(30001, 'INSERT', 'employees', 50025);
CALL sp_audit_log(30001, 'UPDATE', 'employees', 50025);
select * from audit_logs;
DELETE FROM audit_logs WHERE user_id = 30001;