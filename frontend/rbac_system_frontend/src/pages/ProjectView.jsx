import React from "react";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Header from "../components/Header";
import { useLocation } from "react-router-dom";
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
            audit_header_button
        } = location.state || {};
    const navigate = useNavigate();
    const [project, setProject] = useState({
        projectId: 101,
        projectName: "Project 1",
        projectDescription: "Sample Project",
        projectManagerId: 30001,
        projectStartDate: "21-09-2021",
        projectEndDate: "31-12-2021",
    });
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
                                1 ?
                                    (<button type="button" className="btn btn-block btn-primary" onClick={() => { navigate('/project-edit') }}>Edit</button>)
                                    : (<></>)
                            }
                        </small>
                    </h4>
                </div><br />
                <div className="row invoice-info">
                    <div className="col-sm-4 invoice-col">
                        <strong>Project Name</strong>
                        <address>
                            {project.projectName}<br />

                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>Manager ID</strong>
                        <address>
                            {project.projectManagerId}<br />

                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>Start Date</strong>
                        <address>
                            {project.projectStartDate}<br />
                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>End Date</strong>
                        <address>
                            {project.projectEndDate}<br />
                        </address>
                    </div>
                </div>
                <div className="row invoice-info">
                    <div className="col-sm-4 invoice-col">
                        <strong>Project Description</strong>
                        <address>
                            {project.projectDescription}<br />

                        </address>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default ProjectView;