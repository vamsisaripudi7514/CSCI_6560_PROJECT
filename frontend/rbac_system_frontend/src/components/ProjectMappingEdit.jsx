import React, { useEffect } from "react";
import { useState } from "react";
import Header from "./Header";
import { useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";
function ProjectMappingEdit() {
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
            project_name
        } = location.state || {};
    const navigate = useNavigate();
    useEffect(()=>{
        const getProjects = async()=>{
            try{
                const response = await fetch(`http://localhost:7011/api/Project/projectMappingUpdateList`,{
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ employeeID: employee_id })
                });
                const data = await response.json();
                console.log("Data:", data);
                setProjects(data);
            }
            catch(error){
                console.error("Error fetching data:", error);
            }
        }
        getProjects();
    },[]);
    const [selectedProjectId, setSelectedProjectId] = useState(0);
        const [projectName, setProjectName] = useState(project_name);
    const [projectId, setProjectId] = useState(project_id);
    const [projects, setProjects] = useState([
        // { id: 101, pName: "Project 1" },
        // { id: 102, pName: "Project 2" },
        // { id: 103, pName: "Project 3" },
        // { id: 104, pName: "Project 4" },
        // { id: 105, pName: "Project 5" }
    ]);
    const handleChange = (event) => {
        const selectedValue = parseInt(event.target.value, 10);
        setSelectedProjectId(selectedValue);
        console.log("Selected Project ID:", selectedValue);
    };

    async function handleSubmit(e) {
        e.preventDefault();
        console.log("Project Data:", { projects });
        console.log("Employee ID1", employee_id);
        const targetEmployeeId = sessionStorage.getItem("target_employee_id");
        console.log("Target Employee ID111:", targetEmployeeId);

        try{
            console.log("Employee ID1", employee_id);
            console.log("employee Id type:", typeof employee_id);
            console.log("Target Employee ID:", targetEmployeeId);
            const new_project_id = parseInt(selectedProjectId, 10);
            console.log("New Project ID:", new_project_id);
            console.log("Project ID type:", typeof new_project_id);
            const response1 = await fetch("http://localhost:7011/api/Employee/updateProjectMapping",{
                method: "PUT",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ sourceEmployeeId: employee_id, targetEmployeeId: targetEmployeeId ,projectId: new_project_id, oldProjectId: project_id })
            });
            const data = await response1.json();
            console.log("Data111:", data);
            navigate("/employee-view", {
                state: {
                    employee_name: employee_name,
                    employee_id: employee_id,
                    token: token,
                    employee_header_button: employee_header_button,
                    employee_add_button: employee_add_button,
                    employee_update_button: employee_update_button,
                    project_header_button: project_header_button,
                    project_add_button: project_add_button,
                    project_update_button: project_update_button,
                    audit_header_button: audit_header_button
                }
            });
        }
        catch(error){
            console.error("Error:", error);
        }

    }
    return (
        <div>
            <Header
                employee_name={employee_name}
                employee_id = {employee_id}
                token = {token}
                employee_header_button={ employee_header_button}
                employee_add_button={employee_add_button}
                employee_update_button={employee_update_button}
                project_header_button={project_header_button}
                project_add_button={project_add_button}
                project_update_button={project_update_button}                
                audit_header_button={audit_header_button}
            />
            <div className="card card-primary" style={{ alignItems: "center" }}>
                <form style={{ borderRadius: "5px", margin: "50px auto", border: "1px solid #007bff", width: "30%" }} onSubmit={handleSubmit}>
                    <div className="card-header" style={{ backgroundColor: "#007bff", color: "white", alignContent: "center", alignItems: "center" }}>
                        <h2 className="card-title">EDIT PROJECT MAPPING</h2>
                    </div>

                    <div className="card-body" style={{alignContent:"center",alignItems:"center"}} >
                        <div className="d-flex justify-content-center align-items-center">
                        <div className="row">
                            <div className="col-sm-6" >
                                <div className="form-group">
                                    <label>Project ID</label><br />
                                    <p style={{marginLeft:"20px"}}>{projectId}</p>
                                </div>

                            </div>
                            <div className="col-sm-6">
                                <div className="form-group">
                                    <label>Project Name</label><br />
                                    <p style={{marginLeft:"10px"}}>{projectName}</p>
                                </div>
                            </div>
                            
                            <div className="row">
                                <div className="d-flex justify-content-center align-items-center" style={{ width: "100%" }}>
                                    <div className="form-group">

                                        <label>Update Project</label>
                                        <select className="form-control select2 select2-hidden-accessible" style={{ width: "100%" }} data-select2-id="1" tabindex="-1"  onChange={handleChange}>
                                            <option selected="selected" data-select2-id="3" >{"Select Project"}</option>
                                            {
                                                projects.map((project, index) => (
                                                    <option key={index} data-select2-id="3" value={project.project_id}>{project.project_id}</option>
                                                ))
                                            }
                                        </select></div>
                                </div>
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
        </div >
    );
}

export default ProjectMappingEdit;