import React from "react";
import Header from "../components/Header";
import '../styles/dashboard-style.css';
import { useLocation } from "react-router-dom";

function Dashboard() {
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
    console.log("Employee ID:", employee_id);
    console.log("Token:", token);
    console.log("Employee Header Button:", employee_header_button);
    console.log("Employee Add Button:", employee_add_button);
    return (
        <div className="dashboard">
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
            <div style={{ alignItems: 'center', marginTop: '-10%' }}>
                <div className="landing">
                    <h1>Welcome to the Secure Role-Based Access System</h1>
                    <p>Ensuring data privacy with role-based security enforcement</p>
                </div>
            </div>
        </div>
    )
}

export default Dashboard;