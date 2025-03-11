CALL sp_update_project_mapping(20001,50002,105);
SELECT * FROM employee_projects WHERE employee_id = 50002;
-- Success
CALL sp_update_project_mapping(20001,50002,101);
SELECT * FROM employee_projects WHERE employee_id = 50002;
-- Success

CALL sp_update_project_mapping(20001,50002,100);
SELECT * FROM employee_projects WHERE employee_id = 50002;
-- Failure

CALL sp_update_project_mapping(30001,50002,101);
SELECT * FROM employee_projects WHERE employee_id = 50002;
-- Success

CALL sp_update_project_mapping(300000,50002,105);
SELECT * FROM employee_projects WHERE employee_id = 50002;
-- Failure INVALID EMPLOYEE ID

CALL sp_update_project_mapping(20001,50001,1000);
SELECT * FROM project_mapping WHERE employee_id = 50001;
-- Failure INVALID PROJECT ID

CALL sp_update_project_mapping(50001,50002,101);
SELECT * FROM project_mapping WHERE employee_id = 50002;
-- Failure HIERARCHICAL ACCESS DENIED