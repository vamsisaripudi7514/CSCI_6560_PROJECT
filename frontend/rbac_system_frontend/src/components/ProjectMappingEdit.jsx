import React from "react";
import { useState } from "react";
import Header from "./Header";
import { useNavigate } from "react-router-dom";
function ProjectMappingEdit() {
    const navigate = useNavigate();
    const [project, setProject] = useState(101);
    const [projectName, setProjectName] = useState("Project 1");
    const [projectId, setProjectId] = useState(101);
    const [projects, setProjects] = useState([
        { id: 101, pName: "Project 1" },
        { id: 102, pName: "Project 2" },
        { id: 103, pName: "Project 3" },
        { id: 104, pName: "Project 4" },
        { id: 105, pName: "Project 5" }
    ]);
    function handleSubmit(e) {
        e.preventDefault();
    }
    return (
        <div>
            <Header />
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
                                        <select className="form-control select2 select2-hidden-accessible" style={{ width: "100%" }} data-select2-id="1" tabindex="-1" aria-hidden="true">
                                            <option selected="selected" data-select2-id="3">{project}</option>
                                            {
                                                projects.map((project, index) => (
                                                    <option key={index} data-select2-id="3">{project.id}</option>
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
                    <button type="submit" className="btn btn-primary" onClick={()=>{navigate('/employee-view')}}>Submit</button>
                        </div>
                        
                    </div>
                </form>
            </div>
        </div >
    );
}

export default ProjectMappingEdit;