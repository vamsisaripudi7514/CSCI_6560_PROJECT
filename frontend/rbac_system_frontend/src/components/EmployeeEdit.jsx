import React from "react";
import { useState } from "react";
import Header from "./Header";
import { useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";
import { Link } from "react-router-dom";
function EmployeeEdit() {
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
        employee_name,
        employee_email,
        employee_phone,
        employee_role_id,
        employee_manager_id,
        employee_salary,
        employee_is_working
    } = location.state || {};
    const navigate = useNavigate();
    const [employeeName, setEmployeeName] = useState(employee_name);
    const [employeeEmail, setEmployeeEmail] = useState(employee_email);
    const [employeePhone, setEmployeePhone] = useState(employee_phone);
    const [employeeRoleId, setEmployeeRoleId] = useState(employee_role_id);
    const [employeeManagerId, setEmployeeManagerId] = useState(employee_manager_id);
    const [employeeSalary, setEmployeeSalary] = useState(employee_salary);
    const [employeeIsWorking, setEmployeeIsWorking] = useState(employee_is_working);
    async function handleSubmit(event) {
        event.preventDefault();
        console.log("Employee Data:", { employeeName, employeeEmail, employeePhone, employeeRoleId, employeeManagerId, employeeSalary, employeeIsWorking });
        console.log("Employee ID:", employee_id);
        console.log("Payload to be sent:", JSON.stringify(employeeManagerId, null, 2));
        try{
            const target_employee_id = sessionStorage.getItem("target_employee_id");
            const response = await fetch("http://localhost:7011/api/Employee/updateEmployee",{
                method: "PUT",
                headers: { 'Content-Type': 'application/json','Authorization':token},
                body: JSON.stringify({
                    sourceEmployeeId: employee_id,
                    targetEmployeeId: target_employee_id,
                    employeeName : employeeName,
                    employeeEmail : employeeEmail,
                    employeePhone : employeePhone,
                    employeeRoleId : employeeRoleId,
                    employeeManagerId : employeeManagerId !== '' ? Number(employeeManagerId) : null,
                    employeeSalary : employeeSalary,
                    isWorking : employeeIsWorking
                })
            })
            if(!response.ok){
                console.error("Error in response:", response);
                return;
            }
            const data = await response.json();
            console.log("Data:", data);
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
            console.error("Error in response:", error);
        }
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
                audit_header_button: audit_header_button,
                employee_name: employee_name,
                employee_email: employee_email,
                employee_phone: employee_phone,
                employee_role_id: employee_role_id,
                employee_manager_id: employee_manager_id,
                employee_salary: employee_salary,
                employee_is_working: employee_is_working
            }});

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
                        <h2 className="card-title">EDIT EMPLOYEE DATA</h2>
                    </div>

                    <div className="card-body" >
                        <div className="col-sm-6">

                        </div>
                        <div className="row">
                            <div className="col-sm-6">
                                <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Name</label>
                                    <input type="text" className="form-control" id="exampleInputEmail1" placeholder="Enter Name" value={employeeName} onChange={(e) => { setEmployeeName(e.target.value); console.log(employeeName);}} />
                                </div>

                            </div>
                            <div className="col-sm-6">
                                <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Email address</label>
                                    <input type="email" className="form-control" id="exampleInputEmail1" placeholder="Enter email" value={employeeEmail} onChange={(e) => { setEmployeeEmail(e.target.value) }} />
                                </div>

                            </div>
                        </div>
                        <div className="row">
                            <div className="col-sm-6">
                                <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Phone</label>
                                    <input type="text" className="form-control" id="employee-name" placeholder="Enter Phone" value={employeePhone} onChange={(e) => { setEmployeePhone(e.target.value) }} />
                                </div>

                            </div>
                            <div className="col-sm-6">
                                <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Role ID</label>
                                    <input type="number" className="form-control" id="employee-name" placeholder="Enter Role ID" value={employeeRoleId} onChange={(e) => { setEmployeeRoleId(e.target.value) }} />
                                </div>

                            </div>
                        </div>
                        <div className="row">
                            <div className="col-sm-6">
                                <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Manager ID</label>
                                    <input type="number" className="form-control" id="employee-name" placeholder="Enter Manager ID" value={employeeManagerId} onChange={(e) => { setEmployeeManagerId(e.target.value) }} />
                                </div>
                            </div>
                            <div className="col-sm-6">
                                <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Employee Salary</label>
                                    <input type="number" className="form-control" id="employee-name" placeholder="Enter Salary" value={employeeSalary} onChange={(e) => { setEmployeeSalary(e.target.value) }} />
                                </div>
                            </div>
                        </div>
                        <label >
                            <input type="checkbox" checked={employeeIsWorking} onChange={(e) => { setEmployeeIsWorking(e.target.checked) }} style={{ paddingRight: "5px" }} />
                            Is Working
                        </label>
                    </div>

                    <div className="card-footer" >
                        <div className="d-flex justify-content-center align-items-center">
                            
                            <button type="submit" className="btn btn-primary">
                                {/* <Link to="/employee-view" className="btn btn-primary"
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
                                    employee_name: employee_name,
                                    employee_email: employee_email,
                                    employee_phone: employee_phone,
                                    employee_role_id: employee_role_id,
                                    employee_manager_id: employee_manager_id,
                                    employee_salary: employee_salary,
                                    employee_is_working: employee_is_working
                                }}
                                >Submit</Link> */}Submit
                                </button>
                        </div>

                    </div>
                </form>
            </div>
        </div>
    );
}

export default EmployeeEdit;