CALL sp_insert_project(
    20001,                          -- p_user_id (authorized user)
    113,
    'Project Alpha',                -- p_project_name
    'Description for Project Alpha',-- p_project_description
    30001,                          -- p_manager_id (ensure this manager exists)
    '2023-08-01',                   -- p_start_date
    '2023-12-31'                    -- p_end_date
);


CALL sp_insert_project(
    50001,                          -- p_user_id (unauthorized user)
    110,
    'Project Beta',                 -- p_project_name
    'Description for Project Beta', -- p_project_description
    30001,                          -- p_manager_id
    '2023-09-01',                   -- p_start_date
    '2023-12-31'                    -- p_end_date
);
select * from projects where project_id = 109;
select * from projects;
delete from projects where project_id = 113;

