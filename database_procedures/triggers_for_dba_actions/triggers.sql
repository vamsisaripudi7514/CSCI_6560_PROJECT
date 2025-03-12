DELIMITER $$

/* =========================
   Employees Table Triggers
   ========================= */
CREATE TRIGGER trg_employees_after_insert
AFTER INSERT ON employees
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'INSERT', 'employees', NEW.employee_id);
    END IF;
END$$

CREATE TRIGGER trg_employees_after_update
AFTER UPDATE ON employees
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'UPDATE', 'employees', NEW.employee_id);
    END IF;
END$$

CREATE TRIGGER trg_employees_after_delete
AFTER DELETE ON employees
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'DELETE', 'employees', OLD.employee_id);
    END IF;
END$$

/* ===============================
   Access_Permissions Table Triggers
   =============================== */
-- DELIMITER $$
CREATE TRIGGER trg_access_permissions_after_insert
AFTER INSERT ON access_permissions
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'INSERT', 'access_permissions', NEW.permission_id);
    END IF;
END$$
-- DELIMITER ;
-- 

CREATE TRIGGER trg_access_permissions_after_update
AFTER UPDATE ON access_permissions
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'UPDATE', 'access_permissions', NEW.permission_id);
    END IF;
END$$

CREATE TRIGGER trg_access_permissions_after_delete
AFTER DELETE ON access_permissions
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'DELETE', 'access_permissions', OLD.permission_id);
    END IF;
END$$

/* =========================
   Projects Table Triggers
   ========================= */
CREATE TRIGGER trg_projects_after_insert
AFTER INSERT ON projects
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'INSERT', 'projects', NEW.project_id);
    END IF;
END$$

CREATE TRIGGER trg_projects_after_update
AFTER UPDATE ON projects
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'UPDATE', 'projects', NEW.project_id);
    END IF;
END$$

CREATE TRIGGER trg_projects_after_delete
AFTER DELETE ON projects
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'DELETE', 'projects', OLD.project_id);
    END IF;
END$$

/* ============================
   Timesheets Table Triggers
   ============================ */
CREATE TRIGGER trg_timesheets_after_insert
AFTER INSERT ON timesheets
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'INSERT', 'timesheets', NEW.timesheet_id);
    END IF;
END$$

CREATE TRIGGER trg_timesheets_after_update
AFTER UPDATE ON timesheets
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'UPDATE', 'timesheets', NEW.timesheet_id);
    END IF;
END$$

CREATE TRIGGER trg_timesheets_after_delete
AFTER DELETE ON timesheets
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'DELETE', 'timesheets', OLD.timesheet_id);
    END IF;
END$$

/* ======================
   Users Table Triggers
   ====================== */
CREATE TRIGGER trg_users_after_insert
AFTER INSERT ON users
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'INSERT', 'users', NEW.employee_id);
    END IF;
END$$

CREATE TRIGGER trg_users_after_update
AFTER UPDATE ON users
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'UPDATE', 'users', NEW.employee_id);
    END IF;
END$$

CREATE TRIGGER trg_users_after_delete
AFTER DELETE ON users
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'DELETE', 'users', OLD.employee_id);
    END IF;
END$$

/* ==============================
   Employee_Projects Table Triggers
   ============================== */
CREATE TRIGGER trg_employee_projects_after_insert
AFTER INSERT ON employee_projects
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        -- For composite keys, logging one of the keys; adjust if necessary.
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'INSERT', 'employee_projects', NEW.employee_id);
    END IF;
END$$

CREATE TRIGGER trg_employee_projects_after_update
AFTER UPDATE ON employee_projects
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'UPDATE', 'employee_projects', NEW.employee_id);
    END IF;
END$$

CREATE TRIGGER trg_employee_projects_after_delete
AFTER DELETE ON employee_projects
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'DELETE', 'employee_projects', OLD.employee_id);
    END IF;
END$$

/* ========================
   User_Roles Table Triggers
   ======================== */
CREATE TRIGGER trg_user_roles_after_insert
AFTER INSERT ON user_roles
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'INSERT', 'user_roles', NEW.role_id);
    END IF;
END$$

CREATE TRIGGER trg_user_roles_after_update
AFTER UPDATE ON user_roles
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'UPDATE', 'user_roles', NEW.role_id);
    END IF;
END$$

CREATE TRIGGER trg_user_roles_after_delete
AFTER DELETE ON user_roles
FOR EACH ROW
BEGIN
    IF CURRENT_USER() LIKE 'root@%' THEN
        INSERT INTO audit_logs(user_id, db_action, db_table_name, record_id)
        VALUES(1, 'DELETE', 'user_roles', OLD.role_id);
    END IF;
END$$

DELIMITER ;



