import React from "react";
import { useState } from "react";
import Header from "../components/Header";
import { useNavigate } from "react-router-dom";

function EmployeeView() {
    const navigate = useNavigate();
    const [employee, setEmployee] = useState({
        id: 1,
        name: 'John Doe',
        email: 'example@gmail.com',
        phone: '1234567890',
        role: 'Developer',
        salary: 10000,
        hireDate: '2021-01-01',
        isWorking: true,
        projectName: 'Project 1',
        managerID: 2,
        projectDescription: 'Project Description',
        startDate: '2021-01-01',
        endDate: '2021-12-31',
    });
    const [timesheets, setTimeSheet] = useState([
        { id: 1, date: '2021-01-01', hours: 8 },
        { id: 2, date: '2021-01-02', hours: 8 },
        { id: 3, date: '2021-01-03', hours: 8 },
        { id: 4, date: '2021-01-04', hours: 8 },
        { id: 5, date: '2021-01-05', hours: 8 },
    ]);
    return (
        <div>
            <Header />
            <div className="invoice p-5 mb-2" style={{ border: '1px solid black', width: '90%', margin: '10px auto' }}>
                <div className="col-12">
                    <h4>
                        <ion-icon name="person-outline"></ion-icon> Employee Info
                        <small className="float-right">
                            {
                                 1?
                                (<button type="button" className="btn btn-block btn-primary" onClick={()=>{navigate('/employee-edit')}}>Edit</button>)
                                :(<></>)
                            }
                        </small>
                    </h4>
                    
                </div><br />
                <div className="row invoice-info">
                    <div className="col-sm-4 invoice-col">
                        <strong>Name</strong>
                        <address>
                            {employee.name}<br />

                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>Email</strong>
                        <address>
                            {employee.email}<br />

                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>Phone</strong>
                        <address>
                            {employee.phone}<br />
                        </address>
                    </div>
                </div>
                <div className="row invoice-info">
                    <div className="col-sm-4 invoice-col">
                        <strong>Role</strong>
                        <address>
                            {employee.role}<br />
                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>Salary</strong>
                        <address>
                            {employee.salary}<br />
                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>Hire Date</strong>
                        <address>
                            {employee.hireDate}<br />
                        </address>
                    </div>
                </div>
                <hr />
                <div className="col-12">
                    <h4>
                        <ion-icon name="briefcase-outline"></ion-icon> Project Info
                        <small className="float-right">
                            {
                                 1?
                                (<button type="button" className="btn btn-block btn-primary" onClick={()=>{navigate('/project-mapping-edit')}}>Edit</button>)
                                :(<></>)
                            }
                        </small>
                    </h4>
                </div><br />
                <div className="row invoice-info">
                    <div className="col-sm-4 invoice-col">
                        <strong>Project Name</strong>
                        <address>
                            {employee.projectName}<br />

                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>Manager ID</strong>
                        <address>
                            {employee.managerID}<br />

                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>Start Date</strong>
                        <address>
                            {employee.startDate}<br />
                        </address>
                    </div>
                </div>
                <div className="row invoice-info">
                    <div className="col-sm-4 invoice-col">
                        <strong>Project Description</strong>
                        <address>
                            {employee.projectDescription}<br />

                        </address>
                    </div>
                </div>
                {/* <div className="row invoice-info">

                    <div className="col-sm-2 invoice-col">
                        <strong>End Date</strong>
                        <address>
                            {employee.endDate}<br />
                        </address>
                    </div>
                </div> */}
                <hr />
                <div className="col-12">
                    <h4>
                    <ion-icon name="hourglass-outline"></ion-icon> TimeSheet
                    </h4>
                </div><br />
                <div className="row" style={{ width: "40%", margin: "0 auto" }}>
                    <div className="col-12 table-responsive">
                        <table className="table table-striped" style={{textAlign: "center", border: "1px solid black"}}>
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Hours Logged</th>
                                </tr>
                            </thead>
                            <tbody style={{ textAlign: "center" }}>
                                {
                                    timesheets.map((timesheet, index) => (
                                        <tr key={timesheet.id}>
                                            <td>{timesheet.date}</td>
                                            <td>{timesheet.hours}</td>
                                        </tr>
                                    ))
                                }
                            </tbody>
                        </table>
                    </div>
                </div>




            </div>
        </div>
    );
}

export default EmployeeView;