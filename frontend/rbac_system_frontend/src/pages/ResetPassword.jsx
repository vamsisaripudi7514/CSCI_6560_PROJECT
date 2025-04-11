import React from "react";
import { useNavigate } from "react-router-dom";

function ResetPassword() {
    const navigate = useNavigate();
    const [oldPassword, setOldPassword] = React.useState('');
    const [newPassword, setNewPassword] = React.useState('');
    const [confirmPassword, setConfirmPassword] = React.useState('');
    function handleSubmit(e) {
        e.preventDefault();
    }
    return(
        <div>
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

export default ResetPassword;