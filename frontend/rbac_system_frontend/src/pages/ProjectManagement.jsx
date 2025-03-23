import React from "react";
import Header from "../components/Header";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";
function ProjectManagement() {
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
    const [projectID, setProjectID] = useState("");
    const [projects, setProjects] = useState([
        { id: 1, pName: "Project 1" }
    ]);
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
            <div className="col-md-5 offset-md-2" style={{ margin: '50px auto' }}>
                <form >
                    <div className="input-group">
                        <input type="search" className="form-control form-control-lg" placeholder="Enter Project ID"
                            onChange={(e) => { setProjectID(e.target.value) }} />
                        <div className="input-group-append">
                            <button type="submit" className="btn btn-lg btn-default">
                                <i className="fa fa-search"></i>
                            </button>
                        </div>
                    </div>
                    <div className="float-right"  style={{marginBottom: "10px"}}>
                            <button className="btn btn-primary"  onClick={()=>{navigate('/project-add')}}>Add Project</button>
            </div>   
                </form>
            </div>
            <div className="card-body" style={{width: "60%", margin: "0 auto"}}>
                <table className="table table-bordered">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {projects.map((project, index) => (
                            <tr key={project.id}>
                                <td>{project.id}</td>
                                <td>{project.pName}</td>
                                <td>
                                    <button className="btn btn-sm btn-primary" onClick={()=>{navigate('/project-view')}}>View</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    )
}

export default ProjectManagement;