import React, { use } from "react";
import { Link } from "react-router-dom";
import "../styles/header-style.css";
import "admin-lte/dist/css/adminlte.min.css";
import { useState, useEffect,useLayoutEffect } from "react";


function Header({ employee_id, token, employee_header_button, employee_add_button, employee_update_button, project_header_button, project_add_button, project_update_button, audit_header_button }) {

    // console.log("Employee Header Button:", employee_header_button);
    // console.log("Employee Add Button:", employee_add_button);
    // console.log("Employee Update Button:", employee_update_button);
    // console.log("Project Header Button:", project_header_button);
    // console.log("Project Add Button:", project_add_button);
    // console.log("Project Update Button:", project_update_button);
    // console.log("Audit Header Button:", audit_header_button);
    // console.log("Employee ID:", employee_id);
    // console.log("Token:", token);
    return (
        <nav className="main-header navbar navbar-expand navbar-white navbar-light" style={{ marginLeft: "0px", paddingLeft: "7px", backgroundColor: "#74b5e3" }}>
            <ul className="navbar-nav">
                <li className="nav-item d-none d-sm-inline-block">
                    <Link to="/dashboard" className="nav-link"
                    state={{
                        employee_id,
                        token,
                        employee_header_button,
                        employee_add_button,
                        employee_update_button,
                        project_header_button,
                        project_add_button,
                        project_update_button,
                        audit_header_button
                      }} 
                    >Home</Link>
                </li>
                {employee_header_button &&
                    <li className="nav-item d-none d-sm-inline-block">
                        <Link to="/employee-management" className="nav-link"
                        state={{
                            employee_id,
                            token,
                            employee_header_button,
                            employee_add_button,
                            employee_update_button,
                            project_header_button,
                            project_add_button,
                            project_update_button,
                            audit_header_button
                          }}
                        >Employees</Link>
                    </li>
                }
                {project_header_button &&
                    <li className="nav-item d-none d-sm-inline-block">
                        <Link to="/project-management" className="nav-link"
                        state={{
                            employee_id,
                            token,
                            employee_header_button,
                            employee_add_button,
                            employee_update_button,
                            project_header_button,
                            project_add_button,
                            project_update_button,
                            audit_header_button
                          }}
                        >Projects</Link>
                    </li>
                }
                {audit_header_button &&
                    <li className="nav-item d-none d-sm-inline-block">
                        <Link to="/audit-logs" className="nav-link"
                        state={{
                            employee_id,
                            token,
                            employee_header_button,
                            employee_add_button,
                            employee_update_button,
                            project_header_button,
                            project_add_button,
                            project_update_button,
                            audit_header_button
                          }}
                        >Audit Logs</Link>
                    </li>
                }

                {/* <li className="nav-item d-none d-sm-inline-block">
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
                </li> */}
            </ul>

            {/* Right-side Dropdown */}
            <ul className="order-1 order-md-2 navbar-nav navbar-no-expand ml-auto" style={{ marginRight: "110px" }}>
                <li className="nav-item dropdown">
                    {/* Dropdown Toggle */}
                    <a id="dropdownSubMenu2" href="#" className="nav-link dropdown-toggle"
                        role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <ion-icon name="person-outline"></ion-icon>
                    </a>

                    {/* Dropdown Menu */}
                    <ul className="dropdown-menu border-0 shadow" aria-labelledby="dropdownSubMenu2">
                        <li><Link to="/profile" className="dropdown-item"
                        state={{
                            employee_id,
                            token,
                            employee_header_button,
                            employee_add_button,
                            employee_update_button,
                            project_header_button,
                            project_add_button,
                            project_update_button,
                            audit_header_button
                          }}
                        >Profile</Link></li>
                        <li><Link to="/update-password" className="dropdown-item"
                        state={{
                            employee_id,
                            token,
                            employee_header_button,
                            employee_add_button,
                            employee_update_button,
                            project_header_button,
                            project_add_button,
                            project_update_button,
                            audit_header_button
                          }}
                        >Update Password</Link></li>
                        <li><Link to="/" className="dropdown-item"
                        state={{
                            employee_id,
                            token,
                            employee_header_button,
                            employee_add_button,
                            employee_update_button,
                            project_header_button,
                            project_add_button,
                            project_update_button,
                            audit_header_button
                          }}
                        >Logout</Link></li>
                    </ul>
                </li>
            </ul>
        </nav>
    );
}

export default Header;
