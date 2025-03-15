import React from "react";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Header from "./Header";


function EmployeeAdd(){
    const navigate = useNavigate();
    const [employeeId, setEmployeeId] = useState(0);
    const [employeeName, setEmployeeName] = useState('');
    const [employeeEmail, setEmployeeEmail] = useState('');
    const [employeePhone, setEmployeePhone] = useState('');
    const [employeeRoleId, setEmployeeRoleId] = useState(0);
    const [employeeManagerId, setEmployeeManagerId] = useState(0);
    const [employeeSalary, setEmployeeSalary] = useState('');
    const [employeehireDate, setEmployeeHireDate] = useState('');
    function handleSubmit(event){
        event.preventDefault();
        console.log("Employee Data:", {employeeId, employeeName, employeeEmail, employeePhone, employeeRoleId, employeeManagerId, employeeSalary, employeehireDate});
    }

    return (
        <div>
            <Header/>
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
                                    <input type="number" className="form-control" id="employee-name" placeholder="Enter Salary" value={employeeSalary} onChange={(e) => { setEmployeeSalary(e.target.value) }} />
                                </div>
                            </div>
                            <div className="col-sm-6">
                            <label htmlFor="exampleInputEmail1">Hire Date</label>
                                    <input type="date" className="form-control" id="employee-name" placeholder="Enter Salary" value={employeeSalary} onChange={(e) => { setEmployeeSalary(e.target.value) }} />

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
        </div>
    );
}

export default EmployeeAdd;