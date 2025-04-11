import React from "react";
import { useLocation } from "react-router-dom";
import Header from "../components/Header";
import { useNavigate } from "react-router-dom";
function UpdatePassword() {
    const location = useLocation();
    const navigate = useNavigate();
    const [oldPassword, setOldPassword] = React.useState('');
    const [newPassword, setNewPassword] = React.useState('');
    const [confirmPassword, setConfirmPassword] = React.useState('');
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
    function handleSubmit(e) {
        e.preventDefault();
        if(newPassword !== confirmPassword) {
            alert("New password and confirm password do not match.");
            return;
        }
        // const data = {
        //     userName : employee_id,
        //     oldPassword: oldPassword,
        //     newPassword: newPassword
        // }
        const response = fetch("http://localhost:7011/api/Auth/UpdatePassword",{
            method: "PUT",
            body: JSON.stringify({
                userName: employee_name,
                oldPassword: oldPassword,
                newPassword: newPassword
            }),
            headers: {
                "Content-Type": "application/json",
            }
        })
        .then((response) => {
            if (response.ok) {
                alert("Password updated successfully.");
            } else {
                alert("Failed to update password.");
            }
        }).then(
            ()=>{
                navigate("/",{
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
                })
            }
        )

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
            <div className="login-wrapper">
                <div className="login-container">
                    <h2>Update Password</h2>
                    <form onSubmit={handleSubmit}>
                        <div className="input-group">
                            <label htmlFor="old password">Old Password</label>
                            <input
                                type="password"
                                id="old-password"
                                placeholder="Enter your old password"
                                value={oldPassword}
                                onChange={(e) => setOldPassword(e.target.value)}
                                required
                            />
                        </div>
                        <div className="input-group">
                            <label htmlFor="new-password">New Password</label>
                            <input
                                type="password"
                                id="new-password"
                                placeholder="Enter your new password"
                                value={newPassword}
                                onChange={(e) => setNewPassword(e.target.value)}
                                required
                            />
                        </div>
                        <div className="input-group">
                            <label htmlFor="confirm-password">Confirm New Password</label>
                            <input
                                type="password"
                                id="confirm-password"
                                placeholder="Confirm password"
                                value={confirmPassword}
                                onChange={(e) => setConfirmPassword(e.target.value)}
                                required
                            />
                        </div>
                        <button type="submit" className="login-btn">Update Password</button>
                    </form>
                </div>
            </div>

        </div>
    );
}

export default UpdatePassword;