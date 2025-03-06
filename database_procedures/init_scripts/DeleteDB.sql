
use rbac_system;
DROP TABLE IF EXISTS 
  audit_logs,
  users,
  timesheets,
  employee_projects,
  projects,
  access_permissions,
  employees,
  user_roles;
  
  DROP DATABASE IF EXISTS rbac_system;