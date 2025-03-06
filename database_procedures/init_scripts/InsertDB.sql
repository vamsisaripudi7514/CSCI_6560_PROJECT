-- Mock Data for CSCI 6560 Role-Based Hierarchical Access System

-- 1. Insert Roles into user_roles
INSERT INTO user_roles (role_id, role_name, role_description) VALUES
  (1, 'Admin', 'Administrator with full permissions'),
  (2, 'Director', 'Director with oversight of managers and team leaders'),
  (3, 'Manager', 'Project Manager with access to team information'),
  (4, 'Team Leader', 'Team Leader responsible for team operations'),
  (5, 'Developer', 'Developer with limited access'),
  (6, 'HR', 'Human Resources with full CRUD on employees, read on projects and timesheets, and read on audit_logs');

-- -----------------------------------------------------------
-- 2. Insert Employees (Using 5-digit IDs and hierarchy)

-- Director (using a 5-digit ID; here we use 20001)
INSERT INTO employees (employee_id, employee_name, email, phone, role_id, manager_id, encrypted_salary, hire_date, is_working) VALUES
 (1,'Admin','admin1@example.com','111-111',1,NULL,AES_ENCRYPT('100000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')),'2020-01-02',TRUE),
  (10001, 'HR User', 'hr@example.com', '555-0101', 6, NULL,AES_ENCRYPT('50000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')),'2022-01-01', TRUE),
  (20001, 'Director One', 'director@example.com', '111-111-1111', 2, NULL, AES_ENCRYPT('100000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-01-01', TRUE),

  (30001, 'Manager One', 'manager1@example.com', '222-111-1111', 3, 20001, AES_ENCRYPT('80000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-02-01', TRUE),
  (30002, 'Manager Two', 'manager2@example.com', '222-222-1111', 3, 20001, AES_ENCRYPT('80000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-02-02', TRUE),
  (30003, 'Manager Three', 'manager3@example.com', '222-333-1111', 3, 20001, AES_ENCRYPT('80000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-02-03', TRUE),
  (30004, 'Manager Four', 'manager4@example.com', '222-444-1111', 3, 20001, AES_ENCRYPT('80000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-02-04', TRUE),

  (40001, 'Team Leader 1', 'tl1@example.com', '333-111-1111', 4, 30001, AES_ENCRYPT('60000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-03-01', TRUE),
  (40002, 'Team Leader 2', 'tl2@example.com', '333-222-1111', 4, 30001, AES_ENCRYPT('60000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-03-02', TRUE),
  (40003, 'Team Leader 3', 'tl3@example.com', '333-333-1111', 4, 30002, AES_ENCRYPT('60000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-03-03', TRUE),
  (40004, 'Team Leader 4', 'tl4@example.com', '333-444-1111', 4, 30002, AES_ENCRYPT('60000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-03-04', TRUE),
  (40005, 'Team Leader 5', 'tl5@example.com', '333-555-1111', 4, 30003, AES_ENCRYPT('60000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-03-05', TRUE),
  (40006, 'Team Leader 6', 'tl6@example.com', '333-666-1111', 4, 30003, AES_ENCRYPT('60000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-03-06', TRUE),
  (40007, 'Team Leader 7', 'tl7@example.com', '333-777-1111', 4, 30004, AES_ENCRYPT('60000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-03-07', TRUE),
  (40008, 'Team Leader 8', 'tl8@example.com', '333-888-1111', 4, 30004, AES_ENCRYPT('60000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-03-08', TRUE),

  (50001, 'Developer 1', 'dev1@example.com', '444-111-1111', 5, 40001, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-01', TRUE),
  (50002, 'Developer 2', 'dev2@example.com', '444-222-1111', 5, 40001, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-02', TRUE),
  (50003, 'Developer 3', 'dev3@example.com', '444-333-1111', 5, 40001, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-03', TRUE),

  (50004, 'Developer 4', 'dev4@example.com', '444-444-1111', 5, 40002, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-04', TRUE),
  (50005, 'Developer 5', 'dev5@example.com', '444-555-1111', 5, 40002, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-05', TRUE),
  (50006, 'Developer 6', 'dev6@example.com', '444-666-1111', 5, 40002, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-06', TRUE),

  (50007, 'Developer 7', 'dev7@example.com', '444-777-1111', 5, 40003, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-07', TRUE),
  (50008, 'Developer 8', 'dev8@example.com', '444-888-1111', 5, 40003, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-08', TRUE),
  (50009, 'Developer 9', 'dev9@example.com', '444-999-1111', 5, 40003, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-09', TRUE),

  (50010, 'Developer 10', 'dev10@example.com', '444-101-1111', 5, 40004, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-10', TRUE),
  (50011, 'Developer 11', 'dev11@example.com', '444-102-1111', 5, 40004, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-11', TRUE),
  (50012, 'Developer 12', 'dev12@example.com', '444-103-1111', 5, 40004, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-12', TRUE),

  (50013, 'Developer 13', 'dev13@example.com', '444-104-1111', 5, 40005, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-13', TRUE),
  (50014, 'Developer 14', 'dev14@example.com', '444-105-1111', 5, 40005, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-14', TRUE),
  (50015, 'Developer 15', 'dev15@example.com', '444-106-1111', 5, 40005, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-15', TRUE),

  (50016, 'Developer 16', 'dev16@example.com', '444-107-1111', 5, 40006, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-16', TRUE),
  (50017, 'Developer 17', 'dev17@example.com', '444-108-1111', 5, 40006, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-17', TRUE),
  (50018, 'Developer 18', 'dev18@example.com', '444-109-1111', 5, 40006, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-18', TRUE),

  (50019, 'Developer 19', 'dev19@example.com', '444-110-1111', 5, 40007, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-19', TRUE),
  (50020, 'Developer 20', 'dev20@example.com', '444-111-2222', 5, 40007, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-20', TRUE),
  (50021, 'Developer 21', 'dev21@example.com', '444-112-1111', 5, 40007, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-21', TRUE),

  (50022, 'Developer 22', 'dev22@example.com', '444-113-1111', 5, 40008, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-22', TRUE),
  (50023, 'Developer 23', 'dev23@example.com', '444-114-1111', 5, 40008, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-23', TRUE),
  (50024, 'Developer 24', 'dev24@example.com', '444-115-1111', 5, 40008, AES_ENCRYPT('40000', UNHEX('B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613')), '2020-04-24', TRUE);
-- -----------------------------------------------------------
-- 3. Insert Projects (8 projects; project_id as a 3-digit number)
INSERT INTO projects (project_id, project_name, project_description, manager_id, start_date, end_date) VALUES
  (101, 'Project Alpha', 'Description for Project Alpha', 30001, '2021-01-01', '2021-06-30'),
  (102, 'Project Beta', 'Description for Project Beta', 30001, '2021-02-01', '2021-07-31'),
  (103, 'Project Gamma', 'Description for Project Gamma', 30002, '2021-03-01', '2021-08-31'),
  (104, 'Project Delta', 'Description for Project Delta', 30002, '2021-04-01', '2021-09-30'),
  (105, 'Project Epsilon', 'Description for Project Epsilon', 30003, '2021-05-01', '2021-10-31'),
  (106, 'Project Zeta', 'Description for Project Zeta', 30003, '2021-06-01', '2021-11-30'),
  (107, 'Project Eta', 'Description for Project Eta', 30004, '2021-07-01', '2021-12-31'),
  (108, 'Project Theta', 'Description for Project Theta', 30004, '2021-08-01', '2022-01-31');

-------------------------------------------------------------
-- 4. Insert Timesheets (6-digit timesheet_id)
INSERT INTO timesheets (timesheet_id, employee_id, project_id, hours_logged, log_date) VALUES
  (100001, 50001, 101, 8.0, '2021-01-02'),
  (100002, 50005, 102, 7.5, '2021-01-03'),
  (100003, 50010, 103, 9.0, '2021-01-04'),
  (100004, 50002, 101, 8.0, '2021-01-02'),
  (100005, 50003, 101, 7.5, '2021-01-02'),
  (100006, 50004, 102, 8.0, '2021-01-03'),
  (100007, 50005, 102, 8.5, '2021-01-03'),
  (100008, 50006, 102, 7.0, '2021-01-03'),
  (100009, 50007, 103, 8.0, '2021-01-04'),
  (100010, 50008, 103, 8.0, '2021-01-04'),
  (100011, 50009, 103, 7.5, '2021-01-04'),
  (100012, 50010, 104, 8.0, '2021-01-05'),
  (100013, 50011, 104, 8.0, '2021-01-05'),
  (100014, 50012, 104, 8.0, '2021-01-05'),
  (100015, 50013, 105, 8.0, '2021-01-06'),
  (100016, 50014, 105, 7.5, '2021-01-06'),
  (100017, 50015, 105, 8.0, '2021-01-06'),
  (100018, 50016, 106, 8.0, '2021-01-07'),
  (100019, 50017, 106, 8.0, '2021-01-07'),
  (100020, 50018, 106, 8.0, '2021-01-07'),
  (100021, 50019, 107, 8.0, '2021-01-08'),
  (100022, 50020, 107, 8.0, '2021-01-08'),
  (100023, 50021, 107, 8.0, '2021-01-08'),
  (100024, 50022, 108, 8.0, '2021-01-09'),
  (100025, 50023, 108, 8.0, '2021-01-09'),
  (100026, 50024, 108, 8.0, '2021-01-09'),
  (100027, 40001, 101, 9.0, '2021-01-02'),
  (100028, 40002, 102, 9.0, '2021-01-03'),
  (100029, 30001, 101, 7.0, '2021-01-02'),
  (100030, 30002, 103, 7.5, '2021-01-04'),
  (100031, 30003, 105, 6.5, '2021-01-06'),
  (100032, 30004, 107, 7.0, '2021-01-08');


-- 5. Insert Sample Audit Logs
INSERT INTO audit_logs (log_id, user_id, db_action, db_table_name, record_id) VALUES
  (1, 30001, 'UPDATE', 'employees', 50001),
  (2, 30002, 'INSERT', 'projects', 104),
  (3, 20001, 'DELETE', 'timesheets', 100003);


-- -----------------------------------------------------------
-- 6. Insert Users (Login credentials for the employees)
INSERT INTO users (employee_id, username, user_password, last_login, failed_attempts, is_locked) VALUES
  (10001, 'hr',AES_ENCRYPT('hr_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')),NOW(), 0, FALSE),
  (20001, 'director', AES_ENCRYPT('director_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (30001, 'manager1', AES_ENCRYPT('manager_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (30002, 'manager2', AES_ENCRYPT('manager_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (30003, 'manager3', AES_ENCRYPT('manager_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (30004, 'manager4', AES_ENCRYPT('manager_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (40001, 'tl1', AES_ENCRYPT('tl_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (40002, 'tl2', AES_ENCRYPT('tl_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (40003, 'tl3', AES_ENCRYPT('tl_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (40004, 'tl4', AES_ENCRYPT('tl_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (40005, 'tl5', AES_ENCRYPT('tl_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (40006, 'tl6', AES_ENCRYPT('tl_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (40007, 'tl7', AES_ENCRYPT('tl_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (40008, 'tl8', AES_ENCRYPT('tl_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50001, 'dev1', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50002, 'dev2', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50003, 'dev3', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50004, 'dev4', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50005, 'dev5', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50006, 'dev6', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50007, 'dev7', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50008, 'dev8', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50009, 'dev9', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50010, 'dev10', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50011, 'dev11', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50012, 'dev12', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50013, 'dev13', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50014, 'dev14', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50015, 'dev15', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50016, 'dev16', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50017, 'dev17', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50018, 'dev18', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50019, 'dev19', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50020, 'dev20', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50021, 'dev21', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50022, 'dev22', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50023, 'dev23', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE),
  (50024, 'dev24', AES_ENCRYPT('dev_pass', UNHEX('AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1')), NOW(), 0, FALSE);
-- -----------------------------------------------------------
-- 7. Insert Access Permissions (Based on the provided Access Control Matrix)

-- For each role and table, the following permissions are set:
-- Tables: employees, projects, timesheets, user_roles, access_permissions, audit_logs

-- Admin (role_id 1): Full CRUD on all tables.
INSERT INTO access_permissions (role_id, db_table_name, can_select, can_insert, can_update, can_delete) VALUES
  (1, 'employees', 1, 1, 1, 1),
  (1, 'projects', 1, 1, 1, 1),
  (1, 'timesheets', 1, 1, 1, 1),
  (1, 'user_roles', 1, 1, 1, 1),
  (1, 'access_permissions', 1, 1, 1, 1),
  (1, 'audit_logs', 1, 1, 1, 1);

-- Director (role_id 2):
-- employees & projects: CRUD; timesheets, user_roles, access_permissions: Read only; audit_logs: CRUD.
INSERT INTO access_permissions (role_id, db_table_name, can_select, can_insert, can_update, can_delete) VALUES
  (2, 'employees', 1, 0, 0, 0),
  (2, 'projects', 1, 1, 1, 1),
  (2, 'timesheets', 1, 0, 0, 0),
  (2, 'user_roles', 0, 0, 0, 0),
  (2, 'access_permissions', 0, 0, 0, 0),
  (2, 'audit_logs', 1, 0, 0, 0);

-- Manager (role_id 3):
-- employees & projects: Read and Update; timesheets: Read only; user_roles & access_permissions: None; audit_logs: Read.
INSERT INTO access_permissions (role_id, db_table_name, can_select, can_insert, can_update, can_delete) VALUES
  (3, 'employees', 1, 0, 0, 0),
  (3, 'projects', 1, 0, 1, 0),
  (3, 'timesheets', 1, 0, 0, 0),
  (3, 'user_roles', 0, 0, 0, 0),
  (3, 'access_permissions', 0, 0, 0, 0),
  (3, 'audit_logs', 0, 0, 0, 0);


-- Team Leader (role_id 4):
-- All tables: Read only (except user_roles and access_permissions, which are None), audit_logs: Read.
INSERT INTO access_permissions (role_id, db_table_name, can_select, can_insert, can_update, can_delete) VALUES
  (4, 'employees', 1, 0, 0, 0),
  (4, 'projects', 1, 0, 0, 0),
  (4, 'timesheets', 1, 0, 0, 0),
  (4, 'user_roles', 0, 0, 0, 0),
  (4, 'access_permissions', 0, 0, 0, 0),
  (4, 'audit_logs', 0, 0, 0, 0);

-- Developer (role_id 5):
-- employees, projects & timesheets: Read only; no access to user_roles, access_permissions or audit_logs.
INSERT INTO access_permissions (role_id, db_table_name, can_select, can_insert, can_update, can_delete) VALUES
  (5, 'employees', 1, 0, 0, 0),
  (5, 'projects', 1, 0, 0, 0),
  (5, 'timesheets', 1, 0, 0, 0),
  (5, 'user_roles', 0, 0, 0, 0),
  (5, 'access_permissions', 0, 0, 0, 0),
  (5, 'audit_logs', 0, 0, 0, 0);
 
 --    According to the matrix: employees (CRUD), projects (R), timesheets (R),
--    user_roles (None), access_permissions (None), audit_logs (R)
INSERT INTO access_permissions (role_id, db_table_name, can_select, can_insert, can_update, can_delete) VALUES
  (6, 'employees', 1, 1, 1, 1),
  (6, 'projects', 1, 0, 0, 0),
  (6, 'timesheets', 1, 0, 0, 0),
  (6, 'user_roles', 0, 0, 0, 0),
  (6, 'access_permissions', 0, 0, 0, 0),
  (6, 'audit_logs', 1, 0, 0, 0);


  
INSERT INTO employee_projects (employee_id, project_id) VALUES
  -- Manager 30001's projects:
  -- Project 101: Team from Team Leader 40001
  (30001, 101),
  (40001, 101),
  (50001, 101),
  (50002, 101),
  (50003, 101),
  
  -- Project 102: Team from Team Leader 40002
  (30001, 102),
  (40002, 102),
  (50004, 102),
  (50005, 102),
  (50006, 102),
  
  -- Manager 30002's projects:
  -- Project 103: Team from Team Leader 40003
  (30002, 103),
  (40003, 103),
  (50007, 103),
  (50008, 103),
  (50009, 103),
  
  -- Project 104: Team from Team Leader 40004
  (30002, 104),
  (40004, 104),
  (50010, 104),
  (50011, 104),
  (50012, 104),
  
  -- Manager 30003's projects:
  -- Project 105: Team from Team Leader 40005
  (30003, 105),
  (40005, 105),
  (50013, 105),
  (50014, 105),
  (50015, 105),
  
  -- Project 106: Team from Team Leader 40006
  (30003, 106),
  (40006, 106),
  (50016, 106),
  (50017, 106),
  (50018, 106),
  
  -- Manager 30004's projects:
  -- Project 107: Team from Team Leader 40007
  (30004, 107),
  (40007, 107),
  (50019, 107),
  (50020, 107),
  (50021, 107),
  
  -- Project 108: Team from Team Leader 40008
  (30004, 108),
  (40008, 108),
  (50022, 108),
  (50023, 108),
  (50024, 108);
  
  

