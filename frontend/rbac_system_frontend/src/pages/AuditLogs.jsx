import React from "react";
import Header from "../components/Header";
import { useState } from "react";
import { useLocation } from "react-router-dom";
import { useEffect } from "react";
function AuditLogs() {
    useEffect(() => {
        const getAuditLogs = async () => {
            const employee_id = sessionStorage.getItem("employee_id");
            try {
                const response = await fetch('http://localhost:7011/api/AuditLogs/GetAuditLogs', {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ employeeID: employee_id })
                });
                const data = await response.json();
                setAuditLogs((Array.isArray(data) ? data : []));
            }
            catch (error) {
                console.error("Error fetching data:", error);
            }
        }
        getAuditLogs();
    }, []);
    const location = useLocation();
    const [searchTerm, setSearchTerm] = useState('');
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
    const [employeeID, setEmployeeID] = useState("");
    const [auditLogs, setAuditLogs] = useState([
    ]);
    function processString(string) {
        let ans = "";
        if (string == null) {
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
    const filteredAuditLogs = auditLogs.filter((auditLog) => {
        if (!searchTerm) return true;
        const term = searchTerm.toLowerCase();
        const logIdStr = auditLog.log_id ? auditLog.log_id.toString() : "";
        const usrIdStr = auditLog.user_id ? auditLog.user_id.toString() : "";
        const actionStr = auditLog.db_action ? auditLog.db_action.toLowerCase() : "";
        const tableStr = auditLog.db_table_name ? auditLog.db_table_name.toLowerCase() : "";
        const recordIdStr = auditLog.record_id ? auditLog.record_id.toString() : "";
        const timestampStr = auditLog.db_timestamp ? processString(auditLog.db_timestamp).toLowerCase() : "";
        return (
            logIdStr.includes(term) ||
            actionStr.includes(term) ||
            tableStr.includes(term) ||
            recordIdStr.includes(term) ||
            timestampStr.includes(term) ||
            usrIdStr.includes(term)
        );
    });
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
            <div className="col-md-5 offset-md-2" style={{ margin: '50px auto' }}>
                <form action="simple-results.html">
                    <div className="input-group">
                        <input type="search" className="form-control form-control-lg" placeholder="Enter Search Term"
                            onChange={(e) => { setSearchTerm(e.target.value) }} />
                        {/* <div className="input-group-append">
                            <button type="submit" className="btn btn-lg btn-default">
                                <i className="fa fa-search"></i>
                            </button>
                        </div> */}
                    </div>
                </form>
            </div>
            <div className="card-body" style={{ width: "60%", margin: "0 auto" }}>
                <table className="table table-bordered">
                    <thead>
                        <tr>
                            <th>Log ID</th>
                            <th>User ID</th>
                            <th>Action</th>
                            <th>Table</th>
                            <th>Record ID</th>
                            <th>Timestamp</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filteredAuditLogs.map((auditLog, index) => (
                            <tr key={auditLog.log_id}>
                                <td>{auditLog.log_id}</td>
                                <td>{auditLog.user_id}</td>
                                <td>{auditLog.db_action}</td>
                                <td>{auditLog.db_table_name}</td>
                                <td>{auditLog.record_id}</td>
                                <td>{processString(auditLog.db_timestamp)}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    )
}

export default AuditLogs;