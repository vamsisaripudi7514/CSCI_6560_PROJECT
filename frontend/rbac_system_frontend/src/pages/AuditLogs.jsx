import React from "react";
import Header from "../components/Header";
import { useState } from "react";
function AuditLogs() {
    const [employeeID, setEmployeeID] = useState("");
    const [auditLogs, setAuditLogs] = useState([
        { id: 1, action: "delete", table: "employee", recordID: 1001, timestamp: "2021-09-01 10:00:00" },
        { id: 2, action: "create", table: "employee", recordID: 1002, timestamp: "2021-09-01 10:30:00" },
        { id: 3, action: "update", table: "employee", recordID: 1003, timestamp: "2021-09-01 11:00:00" },
    ]);
    return (
        <div>
            <Header />
            <div className="col-md-5 offset-md-2" style={{ margin: '50px auto' }}>
                <form action="simple-results.html">
                    <div className="input-group">
                        <input type="search" className="form-control form-control-lg" placeholder="Enter Project ID"
                            onChange={(e) => { setEmployeeID(e.target.value) }} />
                        <div className="input-group-append">
                            <button type="submit" className="btn btn-lg btn-default">
                                <i className="fa fa-search"></i>
                            </button>
                        </div>
                    </div>
                </form>
            </div>
            <div className="card-body" style={{ width: "60%", margin: "0 auto" }}>
                <table className="table table-bordered">
                    <thead>
                        <tr>
                            <th>Employee ID</th>
                            <th>Action</th>
                            <th>Table</th>
                            <th>Record ID</th>
                            <th>Timestamp</th>
                        </tr>
                    </thead>
                    <tbody>
                        {auditLogs.map((auditLog, index) => (
                            <tr key={auditLog.id}>
                                <td>{auditLog.id}</td>
                                <td>{auditLog.action}</td>
                                <td>{auditLog.table}</td>
                                <td>{auditLog.recordID}</td>
                                <td>{auditLog.timestamp}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    )
}

export default AuditLogs;