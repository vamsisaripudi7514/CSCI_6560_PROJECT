CALL sp_select_employees(20001);
CALL sp_select_employees(40001);
CALL sp_select_employees(50001);
CALL sp_select_employees(30002);
CALL sp_select_employees(10001);
select * from employees where manager_id IS NOT NULL;