import React from "react";
import { useState } from "react";
import Header from "./Header";
import { useNavigate } from "react-router-dom";
function EmployeeEdit() {
    const navigate = useNavigate();
    const [employeeName, setEmployeeName] = useState("Vamsi");
    const [employeeEmail, setEmployeeEmail] = useState("sample@gmail.com");
    const [employeePhone, setEmployeePhone] = useState("1345678");
    const [employeeRoleId, setEmployeeRoleId] = useState(2);
    const [employeeManagerId, setEmployeeManagerId] = useState(30001);
    const [employeeSalary, setEmployeeSalary] = useState("500000");
    const [employeeIsWorking, setEmployeeIsWorking] = useState(true);
    function handleSubmit(event){
        event.preventDefault();
        console.log("Employee Data:", {employeeName, employeeEmail, employeePhone, employeeRoleId, employeeManagerId, employeeSalary, employeeIsWorking});
    }
    return (
        <div>
            <Header />
            <div className="card card-primary" style={{ alignItems: "center" }}>
                <form style={{ borderRadius: "5px", margin: "50px auto", border: "1px solid #007bff", width: "50%" }} onSubmit={ handleSubmit }>
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
                                    <input type="text" className="form-control" id="exampleInputEmail1" placeholder="Enter Name" value={employeeName} onChange={(e) => { setEmployeeName(e.target.value) }} />
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
                    <button type="submit" className="btn btn-primary" onClick={()=>{navigate('/employee-view')}}>Submit</button>
                        </div>
                        
                    </div>
                </form>
            </div>
        </div>
    );
}

export default EmployeeEdit;