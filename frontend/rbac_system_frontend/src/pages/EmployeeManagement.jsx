import React from "react";
import { Container } from 'react-bootstrap';
import Header from '../components/Header';
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";
import { Link } from "react-router-dom";

function EmployeeManagement() {
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
        // console.log("Employee ID:", employee_id);
        // console.log("Token:", token);
    const [employeeId, setEmployeeId] = useState('');
    const [employees, setEmployees] = useState([
        { id: 1, name: 'John Doe', manager: 'Jane Doe' },
        { id: 2, name: 'Jane Doe', manager: 'John Doe' },
        { id: 3, name: 'Alice', manager: 'John Doe' }
    ]);
    useEffect(() => {
        const employee_id = sessionStorage.getItem("employee_id");
        const token = sessionStorage.getItem("token");

        const getEmployees = async()=>{
            try{
                const response = await fetch(`http://localhost:7011/api/Employee/GetAllEmployees?userId=${employee_id}`);
                const data = await response.json();
                console.log("Data:", data);
                setEmployees(data);
            }
            catch(error){
                console.error("Error fetching data:", error);
            }
        }
        getEmployees();
    }, []);
    const navigate = useNavigate();
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
                    <div className="row">
                        <div className="col-md-6">

                        </div>
                        <div className="col-md-6">

                        </div>

                    </div>
                    <div className="input-group">
                        <input type="search" className="form-control form-control-lg" placeholder="Enter Employee ID"
                            onChange={(e) => { setEmployeeId(e.target.value) }} />
                        <div className="input-group-append">
                            <button type="submit" className="btn btn-lg btn-default">
                                <i className="fa fa-search"></i>
                            </button>
                        </div>

                    </div>       
                    <div className="float-right"  style={{marginBottom: "10px"}}>
                        <Link to="/employee-add" className="btn btn-primary"
                        state={{
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
                        >Add Employee</Link>
                            {/* <button className="btn btn-primary"  
                            onClick={()=>{navigate('/employee-add',
                                {
                                    state:{
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
                                }
                            )}}>Add Employee</button> */}
            </div>           
                    
                </form>
            </div>
            
            <div className="card-body" style={{width: "80%", margin: "0 auto"}}>
                <table className="table table-bordered">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Manager</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {Array.isArray(employees) && employees.map((employee, index) => (
                            <tr key={employee.employee_id}>
                                <td>{employee.employee_id}</td>
                                <td>{employee.employee_name}</td>
                                <td>{employee.manager_id}</td>
                                <td>
                                    <button className="btn btn-sm btn-primary"
                                     onClick={()=>{
                                        
                                        sessionStorage.setItem("target_employee_id", employee.employee_id);
                                        console.log("Target Employee ID:", employee.employee_id);
                                        navigate('/employee-view',
                                        {
                                            state:{
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
                                          }
                                     )}}>
                                        
                                        View</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    )
}

export default EmployeeManagement;