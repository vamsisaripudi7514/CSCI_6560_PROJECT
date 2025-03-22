import React, { useState, useEffect } from "react";
import "../styles/login-style.css";
import { useNavigate } from "react-router-dom";

const Login =  () => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const handleSubmit = async(e) => {
        e.preventDefault();
        try{
            const response = await fetch("http://localhost:7011/api/Auth/login",{
                method: "POST",
                headers:{
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({username, password})
            })
            if(!response.ok){
                console.log("Error in response");
            }
            const data = await response.json();
            console.log(data);
        }
        catch(err){
            console.log("Error in fetching data", err);
        }

        console.log("Logging in with:", { username, password });
        if (1) {
            navigate("/dashboard");
        }
    };

    return (
        <div className="login-wrapper">  {/* Wrapper for full centering */}
            <div className="login-container">
                <h2>Login</h2>
                <form onSubmit={handleSubmit}>
                    <div className="input-group">
                        <label htmlFor="username">Username</label>
                        <input
                            type="text"
                            id="username"
                            placeholder="Enter your username"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                            required
                        />
                    </div>
                    <div className="input-group">
                        <label htmlFor="password">Password</label>
                        <input
                            type="password"
                            id="password"
                            placeholder="Enter your password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                    </div>
                    <button type="submit" className="login-btn">Login</button>
                    <p className="forgot-password">
                        <a href="#" onClick={() => alert("Reset password functionality here")}>
                            Forgot Your Password?
                        </a>
                    </p>
                </form>
            </div>
        </div>
    );
};  

export default Login;
