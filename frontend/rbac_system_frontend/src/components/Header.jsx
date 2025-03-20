import React from "react";
import { Link } from "react-router-dom";
import "../styles/header-style.css";
import "admin-lte/dist/css/adminlte.min.css";

function Header() {
    return (
        <nav className="main-header navbar navbar-expand navbar-white navbar-light" style={{ marginLeft: "0px", paddingLeft: "7px", backgroundColor: "#74b5e3" }}>
            <ul className="navbar-nav">
                <li className="nav-item d-none d-sm-inline-block">
                    <Link to="/dashboard" className="nav-link">Home</Link>
                </li>
                <li className="nav-item d-none d-sm-inline-block">
                    <Link to="/employee-management" className="nav-link">Employees</Link>
                </li>
                <li className="nav-item d-none d-sm-inline-block">
                    <Link to="/project-management" className="nav-link">Projects</Link>
                </li>
                <li className="nav-item d-none d-sm-inline-block">
                    <Link to="/audit-logs" className="nav-link">Audit Logs</Link>
                </li>
            </ul>

            {/* Right-side Dropdown */}
            <ul className="order-1 order-md-2 navbar-nav navbar-no-expand ml-auto" style={{marginRight: "110px"}}>
                <li className="nav-item dropdown">
                    {/* Dropdown Toggle */}
                    <a id="dropdownSubMenu2" href="#" className="nav-link dropdown-toggle"
                        role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <ion-icon name="person-outline"></ion-icon>
                    </a>

                    {/* Dropdown Menu */}
                    <ul className="dropdown-menu border-0 shadow" aria-labelledby="dropdownSubMenu2">
                        <li><Link to="/profile" className="dropdown-item">Profile</Link></li>
                        <li><Link to="/update-password" className="dropdown-item">Update Password</Link></li>
                        <li><Link to="/" className="dropdown-item">Logout</Link></li>
                    </ul>
                </li>
            </ul>
        </nav>
    );
}

export default Header;
