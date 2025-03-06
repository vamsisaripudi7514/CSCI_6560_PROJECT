
-- Call the procedure with valid parameters for an authorized user
CALL sp_insert_timesheet(1, 101, 8.00, '2025-01-15');

-- Verify that a timesheet record has been inserted
SELECT * FROM timesheets WHERE employee_id = 1 AND project_id = 101;

-- Check the audit log to see if the insertion was logged
SELECT * FROM audit_logs WHERE user_id = 1 AND db_table_name = 'timesheets';


-- ----------

-- Call the procedure with a user who lacks the INSERT permission
CALL sp_insert_timesheet(20001, 101, 7.50, '2025-01-15');

-- Verify that no new timesheet record has been inserted for employee_id = 20001
SELECT * FROM timesheets WHERE employee_id = 20001 AND project_id = 101;

-- Check audit_logs to confirm that no audit entry was created for this failed insertion
SELECT * FROM audit_logs WHERE user_id = 20001 AND db_table_name = 'timesheets';


-- ----------

-- Testing with 0 hours logged
CALL sp_insert_timesheet(1, 101, 0.00, '2025-01-15');
SELECT * FROM timesheets WHERE employee_id = 1 AND project_id = 101 AND hours_logged = 0.00;

-- Testing with a past date (if valid in your business logic)
CALL sp_insert_timesheet(50001, 101, 4.00, '2020-01-15');
SELECT * FROM timesheets WHERE employee_id = 50001 AND project_id = 101 AND log_date = '2020-01-15';


-- ----------

