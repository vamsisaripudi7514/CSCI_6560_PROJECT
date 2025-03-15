import React from "react";
import { Container } from 'react-bootstrap';
import Header from '../components/Header';
import { useState } from "react";
import { useNavigate } from "react-router-dom";
function EmployeeManagement() {
    const [employeeId, setEmployeeId] = useState('');
    const [employees, setEmployees] = useState([
        { id: 1, name: 'John Doe', manager: 'Jane Doe' },
        { id: 2, name: 'Jane Doe', manager: 'John Doe' },
        { id: 3, name: 'Alice', manager: 'John Doe' }
    ]);
    const navigate = useNavigate();
    return (
        <div>
            <Header />
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
                            <button className="btn btn-primary"  onClick={()=>{navigate('/employee-add')}}>Add Employee</button>
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
                        {employees.map((employee, index) => (
                            <tr key={employee.id}>
                                <td>{employee.id}</td>
                                <td>{employee.name}</td>
                                <td>{employee.manager}</td>
                                <td>
                                    <button className="btn btn-sm btn-primary" onClick={()=>{navigate('/employee-view')}}>View</button>
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