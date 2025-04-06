import React from "react";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Header from "./Header";
import { useLocation } from "react-router-dom";
import Swal from "sweetalert2";
import { Link } from "react-router-dom";

function EmployeeAdd(){
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
    const [employeeId, setEmployeeId] = useState(0);
    const [employeeName, setEmployeeName] = useState('');
    const [employeeEmail, setEmployeeEmail] = useState('');
    const [employeePhone, setEmployeePhone] = useState('');
    const [employeeRoleId, setEmployeeRoleId] = useState(0);
    const [employeeManagerId, setEmployeeManagerId] = useState(0);
    const [employeeSalary, setEmployeeSalary] = useState('');
    const [employeehireDate, setEmployeeHireDate] = useState('');
    async function handleSubmit(event){
        event.preventDefault();
        console.log("Employee Data:", {employeeId, employeeName, employeeEmail, employeePhone, employeeRoleId, employeeManagerId, employeeSalary, employeehireDate});
        try{
            sessionStorage.setItem("target_employee_id", employeeId);
            const response = await fetch("http://localhost:7011/api/Employee/InsertEmployee",{
                method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(
                    {
                        sourceEmployeeId : employee_id,
                        targetEmployeeId : employeeId,
                        employeeName : employeeName,
                        employeeEmail : employeeEmail,
                        employeePhone : employeePhone,
                        employeeRoleId : employeeRoleId,
                        employeeManagerId : employeeManagerId,
                        employeeSalary : employeeSalary,
                        hireDate : employeehireDate
                    }
                )
            });
            const data = await response.json();
            console.log("Data:", data);
            if(!response.ok){
                console.error("Error in adding employee:", data.message);
                Swal.fire({
                    icon: 'error',
                    title: data.message,
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000,
                    timerProgressBar: true,
                });
                return;
            }
            Swal.fire({
                icon: 'success',
                title: data.message,
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
            });
            navigate('/employee-view',{
                state:{
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
            console.error("Error in adding employee:", error);
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
                <form style={{ borderRadius: "5px", margin: "50px auto", border: "1px solid #007bff", width: "50%" }} onSubmit={ handleSubmit }>
                    <div className="card-header" style={{ backgroundColor: "#007bff", color: "white" }}>
                        <h2 className="card-title">ADD EMPLOYEE </h2>
                    </div>

                    <div className="card-body" >
                    <div className="row">
                            <div className="col-sm-6">
                                <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">ID</label>
                                    <input type="number" className="form-control" id="exampleInputEmail1" placeholder="Enter Employee ID" value={employeeId} onChange={(e) => { setEmployeeId(e.target.value) }} />
                                </div>

                            </div>
                            <div className="col-sm-6">
                            <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Name</label>
                                    <input type="text" className="form-control" id="exampleInputEmail1" placeholder="Enter Name" value={employeeName} onChange={(e) => { setEmployeeName(e.target.value) }} />
                                </div>
                                

                            </div>
                        </div>
                        <div className="row">
                            <div className="col-sm-6">
                            <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Email address</label>
                                    <input type="email" className="form-control" id="exampleInputEmail1" placeholder="Enter email" value={employeeEmail} onChange={(e) => { setEmployeeEmail(e.target.value) }} />
                                </div>

                            </div>
                            <div className="col-sm-6">
                            <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Phone</label>
                                    <input type="text" className="form-control" id="employee-name" placeholder="Enter Phone" value={employeePhone} onChange={(e) => { setEmployeePhone(e.target.value) }} />
                                </div>

                            </div>
                        </div>
                        <div className="row">
                            <div className="col-sm-6">
                            <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Role ID</label>
                                    <input type="number" className="form-control" id="employee-name" placeholder="Enter Role ID" value={employeeRoleId} onChange={(e) => { setEmployeeRoleId(e.target.value) }} />
                                </div>

                            </div>
                            <div className="col-sm-6">
                            <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Manager ID</label>
                                    <input type="number" className="form-control" id="employee-name" placeholder="Enter Manager ID" value={employeeManagerId} onChange={(e) => { setEmployeeManagerId(e.target.value) }} />
                                </div>

                            </div>
                        </div>
                        <div className="row">
                            <div className="col-sm-6">
                            <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Employee Salary</label>
                                    <input type="text" className="form-control" id="employee-name" placeholder="Enter Salary" value={employeeSalary} onChange={(e) => { setEmployeeSalary(e.target.value) }} />
                                </div>
                            </div>
                            <div className="col-sm-6">
                            <label htmlFor="exampleInputEmail1">Hire Date</label>
                                    <input type="date" className="form-control" id="employee-name" placeholder="Enter Salary" value={employeehireDate} onChange={(e) => { setEmployeeHireDate(e.target.value) }} />

                            </div>
                        </div>
                    </div>
                    
                    <div className="card-footer" >
                    <div className="d-flex justify-content-center align-items-center">
                        
                    <button type="submit" className="btn btn-primary" >
                        {/* <Link to="/employee-view" className="btn btn-primary"
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
                        >Submit</Link> */}
                        Submit
                        </button>
                        </div>
                        
                    </div>
                </form>
            </div>
        </div>
    );
}

export default EmployeeAdd;