import React from "react";
import { useState } from "react";
import Header from "../components/Header";
import { useNavigate } from "react-router-dom";
import { useLocation} from "react-router-dom";
import { useEffect } from "react";
function EmployeeView() {
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
    const [employee_data, setEmployeeData] = useState([{}]);
    const [timesheets, setTimeSheet] = useState([
        {}
    ]);

    useEffect(() => {
        const source_employee_id = sessionStorage.getItem("employee_id");
        const source_token = sessionStorage.getItem("token");
        const target_employee_id = sessionStorage.getItem("target_employee_id");
        console.log("Target Employee ID:", target_employee_id);
        const getEmployee = async () => {
            try {
                const response = await fetch("http://localhost:7011/api/Employee/GetEmployeeDetails", {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ sourceEmployeeId: source_employee_id, targetEmployeeId: target_employee_id })
                });
                const data = await response.json();
                console.log("Data:", data);
                setEmployeeData(data);
            }
            catch (error) {
                console.error("Error fetching data:", error);
            }
        }
        const getTimesheet = async()=>{
            try{
                const response = await fetch("http://localhost:7011/api/Employee/selectTimesheet", {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ sourceEmployeeId: source_employee_id , targetEmployeeId: target_employee_id })
                })
                const data = await response.json();
                console.log("Timesheet Data:", data);
                setTimeSheet(data);
                console.log(typeof(data[0].log_date));
            }
            catch(error){
                console.error("Error fetching timesheet data:", error);
            }
        }
        getEmployee();
        getTimesheet();
    }, []);
    function processString(string) {
        let ans ="";
        if(string == null){
            return "N/A";
        }
        for (let i = 0; i < string.length; i++) {
            if (string[i] == 'T') {
                break;
            }
            ans += string[i];
        }
        return ans;
    }   
    return (
        <div>
            <Header
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
            <div className="invoice p-5 mb-2" style={{ border: '1px solid black', width: '90%', margin: '10px auto' }}>
                <div className="col-12">
                    <h4>
                        <ion-icon name="person-outline"></ion-icon> Employee Info
                        <small className="float-right">
                            {
                                1 ?
                                    (<button type="button" className="btn btn-block btn-primary" 
                                        onClick={() => { navigate('/employee-edit',{
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
                                        }) }}>Edit</button>)
                                    : (<></>)
                            }
                        </small>
                    </h4>

                </div><br />
                <div className="row invoice-info">
                    <div className="col-sm-4 invoice-col">
                        <strong>Name</strong>
                        <address>
                            {employee_data[0].name}<br />

                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>Email</strong>
                        <address>
                            {employee_data[0].email}<br />

                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>Phone</strong>
                        <address>
                            {employee_data[0].phone}<br />
                        </address>
                    </div>
                </div>
                <div className="row invoice-info">
                    <div className="col-sm-4 invoice-col">
                        <strong>Role</strong>
                        <address>
                            {employee_data[0].role}<br />
                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>Salary</strong>
                        <address>
                            {employee_data[0].salary}<br />
                        </address>
                    </div>
                    <div className="col-sm-4 invoice-col">
                        <strong>Hire Date</strong>
                        <address>
                            {employee_data[0].hire_date
                                ? processString(employee_data[0].hire_date)
                                : "N/A"
                            }<br />
                        </address>
                    </div>
                </div>
                <hr />
                {
                    Array.isArray(employee_data)  && employee_data.map((employee, index) => (
                        <>
                            <div className="col-12">
                                <h4>
                                    <ion-icon name="briefcase-outline"></ion-icon> Project Info
                                    <small className="float-right">
                                        {
                                            1 ?
                                                (<button type="button" className="btn btn-block btn-primary" onClick={() => { navigate('/project-mapping-edit') }}>Edit</button>)
                                                : (<></>)
                                        }
                                    </small>
                                </h4>
                            </div><br />
                            <div className="row invoice-info">
                                <div className="col-sm-4 invoice-col">
                                    <strong>Project Name</strong>
                                    <address>
                                        {employee?.project_name || "Not Assigned"}<br />

                                    </address>
                                </div>
                                <div className="col-sm-4 invoice-col">
                                    <strong>Manager ID</strong>
                                    <address>
                                        {employee.manager_id || "Not Assigned"}<br />

                                    </address>
                                </div>
                                <div className="col-sm-4 invoice-col">
                                    <strong>Start Date</strong>
                                    <address>
                                        {processString(employee.start_date) || "N/A"}<br />
                                    </address>
                                </div>
                            </div>
                            <div className="row invoice-info">
                                <div className="col-sm-4 invoice-col">
                                    <strong>Project Description</strong>
                                    <address>
                                        {employee.project_description || "N/A"}<br />

                                    </address>
                                </div>
                            </div>
                            <hr />
                        </>
                    ))
                }
                {/* <div className="col-12">
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
                </div> */}
                {/* <div className="row invoice-info">

                    <div className="col-sm-2 invoice-col">
                        <strong>End Date</strong>
                        <address>
                            {employee.endDate}<br />
                        </address>
                    </div>
                </div> */}
                
                <div className="col-12">
                    <h4>
                        <ion-icon name="hourglass-outline"></ion-icon> TimeSheet
                    </h4>
                </div><br />
                <div className="row" style={{ width: "40%", margin: "0 auto" }}>
                    <div className="col-12 table-responsive">
                        <table className="table table-striped" style={{ textAlign: "center", border: "1px solid black" }}>
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Hours Logged</th>
                                </tr>
                            </thead>
                            <tbody style={{ textAlign: "center" }}>
                                { Array.isArray(timesheets) && timesheets[0].timesheet_id!=null &&
                                    timesheets.map((timesheet, index) => (
                                        <tr key={timesheet.timesheet_id}>
                                            <td>{processString(timesheet.log_date)}</td>
                                            <td>{timesheet.hours_logged}</td>
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