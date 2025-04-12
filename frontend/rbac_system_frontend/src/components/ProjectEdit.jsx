import React from "react";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Header from "./Header";
import { useLocation } from "react-router-dom";
function ProjectEdit() {
    const location = useLocation();
    const {
        employee_name,
        employee_id,
        token,
        employee_header_button,
        employee_add_button,
        employee_update_button,
        project_header_button,
        project_add_button,
        project_update_button,
        audit_header_button,
        project_id,
        project_name,
        manager_id,
        start_date,
        end_date,
        project_description
    } = location.state || {};
    const navigate = useNavigate();
    const [projectName, setProjectName] = useState(project_name);
    const [projectDescription, setProjectDescription] = useState(project_description);
    const [projectManagerId, setProjectManagerId] = useState(manager_id);
    const [projectStartDate, setProjectStartDate] = useState(() =>
        start_date ? new Date(start_date).toISOString().split("T")[0] : ""
    );
    const [projectEndDate, setProjectEndDate] = useState(() =>
        end_date ? new Date(end_date).toISOString().split("T")[0] : ""
    );
    async function handleSubmit(event) {
        event.preventDefault();
        // console.log("Project Data:", { projectName, projectDescription, projectManagerId, projectStartDate, projectEndDate });
        try {
            const response = await fetch('http://localhost:7011/api/Project/updateProject', {
                method: "PUT",
                headers: { 'Content-Type': 'application/json' ,'Authorization':token},
                body: JSON.stringify({
                    sourceEmployeeId: employee_id,
                    projectId: project_id,
                    projectName: projectName,
                    projectDescription: projectDescription,
                    managerId: projectManagerId,
                    startDate: projectStartDate,
                    endDate: projectEndDate
                })
            });
            const data = await response.json();
            console.log("Data:", data);
            if (!response.ok) {
                console.error("Error in response:", data.message);
                return;
            }
            navigate('/project-view', {
                state: {
                    employee_name,
                    employee_id,
                    token,
                    employee_header_button,
                    employee_add_button,
                    employee_update_button,
                    project_header_button,
                    project_add_button,
                    project_update_button,
                    audit_header_button,
                    project_id
                }
            });
        }
        catch (error) {
            console.error("Error fetching data:", error);
        }

    }

    return (
        <div>
            <Header
                employee_name={employee_name}
                employee_id={employee_id}
                token={token}
                employee_header_button={employee_header_button}
                employee_add_button={employee_add_button}
                employee_update_button={employee_update_button}
                project_header_button={project_header_button}
                project_add_button={project_add_button}
                project_update_button={project_update_button}
                audit_header_button={audit_header_button}
            />
            <div className="card card-primary" style={{ alignItems: "center" }}>
                <form style={{ borderRadius: "5px", margin: "50px auto", border: "1px solid #007bff", width: "50%" }} onSubmit={handleSubmit}>
                    <div className="card-header" style={{ backgroundColor: "#007bff", color: "white" }}>
                        <h2 className="card-title">EDIT PROJECT DATA</h2>
                    </div>

                    <div className="card-body" >
                        <div className="col-sm-6">

                        </div>
                        <div className="row">
                            <div className="col-sm-6">
                                <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Project Name</label>
                                    <input type="text" className="form-control" id="exampleInputEmail1" placeholder="Enter Project Name" value={projectName} onChange={(e) => { setProjectName(e.target.value) }} />
                                </div>

                            </div>
                            <div className="col-sm-6">
                                <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Project Description</label>
                                    <input type="textarea" className="form-control" id="exampleInputEmail1" placeholder="Enter Description" value={projectDescription} onChange={(e) => { setProjectDescription(e.target.value) }} />
                                </div>

                            </div>
                        </div>
                        <div className="row">
                            <div className="col-sm-6">
                                <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Manager ID</label>
                                    <input type="number" className="form-control" id="employee-name" placeholder="Enter Phone" value={projectManagerId} onChange={(e) => { setProjectManagerId(e.target.value) }} />
                                </div>

                            </div>
                            <div className="col-sm-6">
                                <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Start Date</label>
                                    <input type="date" className="form-control" id="employee-name" value={projectStartDate} onChange={(e) => { setProjectStartDate(e.target.value) }} />
                                </div>

                            </div>
                        </div>
                        <div className="row">
                            <div className="col-sm-6">
                                <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">End Date</label>
                                    <input type="date" className="form-control" id="employee-name" value={projectEndDate} onChange={(e) => { setProjectEndDate(e.target.value) }} />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div className="card-footer" >
                        <div className="d-flex justify-content-center align-items-center">
                            <button type="submit" className="btn btn-primary" >Submit</button>
                        </div>

                    </div>
                </form>
            </div>
        </div>
    );
}

export default ProjectEdit;