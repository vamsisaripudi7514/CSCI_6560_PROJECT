import React from "react";
import Header from "../components/Header";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";
import { Link } from "react-router-dom";
import { useEffect } from "react";
function ProjectManagement() {
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
            audit_header_button
        } = location.state || {};
    const navigate = useNavigate();
    const [projectID, setProjectID] = useState("");
    const [projects, setProjects] = useState([
    ]);
    const [searchTerm, setSearchTerm] = useState('');
    useEffect(()=>{
        const getProjects = async()=>{
            try{
                const response = await fetch(`http://localhost:7011/api/Employee/GetProjects`,{
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' ,'Authorization':token},
                    body: JSON.stringify({ employee_id: employee_id })
                });
                const data = await response.json();
                console.log("Data:", data);
                setProjects(Array.isArray(data) ? data : []);
            }
            catch(error){
                console.error("Error fetching data:", error);
            }
        }
        getProjects();
    },[]);
    const filteredProjects = projects.filter((project) => {
        if (!searchTerm) return true;
        const idStr = project.project_id ? project.project_id.toString() : "";
        const nameStr = project.project_name
          ? project.project_name.toLowerCase()
          : "";
        const term = searchTerm.toLowerCase();
        return idStr.includes(term) || nameStr.includes(term);
      });
    return (
        <div>
            <Header
                employee_name = {employee_name}
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
                        <input type="search" className="form-control form-control-lg" placeholder="Enter Project ID/Name"
                            onChange={(e) => { setSearchTerm(e.target.value) }} />
                        {/* <div className="input-group-append">
                            <button type="submit" className="btn btn-lg btn-default">
                                <i className="fa fa-search"></i>
                            </button>
                        </div> */}
                    </div>
                    <div className="float-right"  style={{marginBottom: "10px"}}>
                        <Link to="/project-add" className="btn btn-primary"
                        state={{
                            employee_name,
                            employee_id,
                            token,
                            employee_header_button,
                            employee_add_button,
                            employee_update_button,
                            project_header_button,
                            project_add_button,
                            project_update_button,
                            audit_header_button
                        }}
                        >Add Project
                        </Link>
                            {/* <button className="btn btn-primary"  onClick={()=>{navigate('/project-add')}}>Add Project</button> */}
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
                        {filteredProjects.map((project, index) => (
                            <tr key={project.project_id}>
                                <td>{project.project_id}</td>
                                <td>{project.project_name}</td>
                                <td>
                                    <Link to="/project-view" className="btn btn-sm btn-primary"
                                        state={{
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
                                            project_id: project.project_id,
                                            project_name: project.project_name
                                        }}>
                                    View</Link>
                                    {/* <button className="btn btn-sm btn-primary" onClick={()=>{navigate('/project-view')}}>View</button> */}
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