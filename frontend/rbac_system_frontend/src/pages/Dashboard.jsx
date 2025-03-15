import React from "react";
import Header from "../components/Header";
import '../styles/dashboard-style.css';


function Dashboard() {
    return (
        <div className="dashboard">
            <Header />
            <div style={{alignItems: 'center', marginTop: '-10%'}}>
            <div className="landing">
                <h1>Welcome to the Secure Role-Based Access System</h1>
                <p>Ensuring data privacy with role-based security enforcement</p>
            </div>
        </div>
        </div>
    )
}

export default Dashboard;