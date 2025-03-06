CALL sp_select_access_permissions(20001, 'projects', @can_insert, @can_select, @can_update,@can_delete);  
SELECT @can_insert, @can_select, @can_update, @can_delete ;

-- Expected output:
-- 1, 1, 1, 1

CALL sp_select_access_permissions(30001, 'employees', @can_insert, @can_select, @can_update,@can_delete);
SELECT @can_insert, @can_select, @can_update, @can_delete ;

-- Expected output:
-- 0, 1, 0, 0

CALL sp_select_access_permissions(40001, 'employees', @can_insert, @can_select, @can_update,@can_delete);
SELECT @can_insert, @can_select, @can_update, @can_delete ;

-- Expected output:
-- 0, 1, 0, 0

CALL sp_select_access_permissions(10001, 'employees', @can_insert, @can_select, @can_update,@can_delete);
SELECT @can_insert, @can_select, @can_update, @can_delete ;

-- Expected output:
-- 1, 1, 1, 1

CALL sp_select_access_permissions(30001, 'projects', @can_insert, @can_select, @can_update, @can_delete);
SELECT @can_insert, @can_select, @can_update, @can_delete ;
-- Expected output:
-- 0,1,1,0

CALL sp_select_access_permissions(30000, 'projects', @can_insert, @can_select, @can_update, @can_delete);
SELECT @can_insert, @can_select, @can_update, @can_delete ;
-- Expected output:
-- 0,1,1,0

CALL sp_select_access_permissions(30001, 'project', @can_insert, @can_select, @can_update, @can_delete);
SELECT @can_insert, @can_select, @can_update, @can_delete ;
-- Expected output:
-- 0,1,1,0