import React, { use } from "react";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import Header from "../components/Header";
import { useLocation } from "react-router-dom";
import { useEffect } from "react";
function ProjectView() {
    const location = useLocation();
        const {
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
    const [project, setProject] = useState({

    });
    useEffect(()=>{
        const getProject = async()=>{
            try{
                const response = await fetch(`http://localhost:7011/api/Employee/GetProject`,{
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ employee_id: employee_id, project_id: project_id })
                });
                const data = await response.json();
                console.log("Data:", data);
                setProject(data);
            }
            catch(error){
                console.error("Error fetching data:", error);
            }
        }
        getProject();
    },[]);
    function processString(string) {
        let ans ="";
        if(string == null){
            return "N/A";
        }
        for (let i = 0; i < string.length; i++) {
            if (string[i] === 'T') {
                break;
            }
            ans += string[i];
        }
        return ans;
    }   
    return (
        <div>
           <Header
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
            <div className="invoice p-5 mb-2" style={{ border: '1px solid black', width: '90%', margin: '10px auto' }}>
                <div className="col-12">
                    <h4>
                        <ion-icon name="briefcase-outline"></ion-icon> Project Info
                        <small className="float-right">
                            {
                                project_update_button &&
                                <Link to="/project-edit" className="btn btn-primary"
                                state={{
                                    employee_id: employee_id,
                                    token: token,
                                    employee_header_button: employee_header_button,
                                    employee_add_button: employee_add_button,
                                    employee_update_button: employee_update_button,
                                    project_header_button: project_header_button,
                                    project_add_button: project_add_button,
                                    project_update_button: project_update_button,
                                    audit_header_button: audit_header_button,
                                    project_id: project.project_id,
                                    project_name : project.project_name,
                                    manager_id: project.manager_id,
                                    start_date: project.start_date,
                                    end_date: project.end_date,
                                    project_description: project.project_description
                                }}
                                >Edit</Link>
                                    // <button type="button" className="btn btn-block btn-primary" onClick={() => { navigate('/project-edit') }}>Edit</button>)
                                    
                            }
                        </small>
                    </h4>
                </div><br />
                <div className="row invoice-info">
                    <div className="col-sm-4 invoice-col">
                        <strong>Project Name</strong>
                        <address>
                            {project.project_name}<br />

                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>Manager ID</strong>
                        <address>
                            {project.manager_id}<br />

                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>Start Date</strong>
                        <address>
                            {processString(project.start_date)}<br />
                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>End Date</strong>
                        <address>
                            {processString(project.end_date)}<br />
                        </address>
                    </div>
                </div>
                <div className="row invoice-info">
                    <div className="col-sm-4 invoice-col">
                        <strong>Project Description</strong>
                        <address>
                            {project.project_description}<br />

                        </address>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default ProjectView;