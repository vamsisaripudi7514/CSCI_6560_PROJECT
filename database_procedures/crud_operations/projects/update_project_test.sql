-- Attempt to update the project as an unauthorized user
-- Update project details as an authorized user
CALL sp_update_project(
    30001,               -- p_user_id (authorized manager)
    101,                 -- p_project_id
    'Updated Project',   -- p_project_name
    'Updated description for project1 101', -- p_project_description
    30001,               -- p_manager_id (assumed unchanged)
    '2023-05-01',        -- p_start_date
    '2023-12-30'         -- p_end_date
);

-- Verify the update:
SELECT * FROM projects WHERE project_id = 101;


CALL sp_update_project(
    50001,               -- p_user_id (unauthorized user)
    101,                 -- p_project_id
    'Malicious Update',  -- p_project_name
    'Trying to update without permission', -- p_project_description
    30001,               -- p_manager_id (kept same for this test)
    '2023-05-01', 
    '2023-12-31'
);
