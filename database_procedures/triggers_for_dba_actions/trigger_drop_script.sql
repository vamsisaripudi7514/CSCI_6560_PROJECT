DELIMITER $$

DROP TRIGGER IF EXISTS trg_employees_after_insert$$
DROP TRIGGER IF EXISTS trg_employees_after_update$$
DROP TRIGGER IF EXISTS trg_employees_after_delete$$

DROP TRIGGER IF EXISTS trg_access_permissions_after_insert$$
DROP TRIGGER IF EXISTS trg_access_permissions_after_update$$
DROP TRIGGER IF EXISTS trg_access_permissions_after_delete$$

DROP TRIGGER IF EXISTS trg_projects_after_insert$$
DROP TRIGGER IF EXISTS trg_projects_after_update$$
DROP TRIGGER IF EXISTS trg_projects_after_delete$$

DROP TRIGGER IF EXISTS trg_timesheets_after_insert$$
DROP TRIGGER IF EXISTS trg_timesheets_after_update$$
DROP TRIGGER IF EXISTS trg_timesheets_after_delete$$

DROP TRIGGER IF EXISTS trg_users_after_insert$$
DROP TRIGGER IF EXISTS trg_users_after_update$$
DROP TRIGGER IF EXISTS trg_users_after_delete$$

DROP TRIGGER IF EXISTS trg_employee_projects_after_insert$$
DROP TRIGGER IF EXISTS trg_employee_projects_after_update$$
DROP TRIGGER IF EXISTS trg_employee_projects_after_delete$$

DROP TRIGGER IF EXISTS trg_user_roles_after_insert$$
DROP TRIGGER IF EXISTS trg_user_roles_after_update$$
DROP TRIGGER IF EXISTS trg_user_roles_after_delete$$

DELIMITER ;