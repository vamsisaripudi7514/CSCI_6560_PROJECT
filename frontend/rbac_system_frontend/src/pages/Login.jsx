import React, { useState, useEffect } from "react";
import "../styles/login-style.css";
import { useNavigate, useLocation } from "react-router-dom";
import Swal from "sweetalert2";


// useEffect(() => {



// }, []);

const Login =  () => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
      
        try {
          const response = await fetch("http://localhost:7011/api/Auth/login", {
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username, password }),
          });
          
          const data = await response.json();
          
          if (!response.ok) {
            console.error("Error in response:", data.message);
            Swal.fire({
              icon: 'error',
              title: data.message,
              toast: true,
              position: 'top-end',
              showConfirmButton: false,
              timer: 3000,
              timerProgressBar: true,
            });
            return;
          }
          
        //   console.log("Login data:", data);
          sessionStorage.setItem("employee_id", data.employee_id);
          sessionStorage.setItem("token", "Bearer " + data.token);
          console.log("Token:", sessionStorage.getItem("token"));
          console.log("Employee ID:", sessionStorage.getItem("employee_id"));
          const buttons = await handleButtonVisibility();
          console.log("Buttons:", buttons);
          navigate("/dashboard",{
            state:{
                employee_name: username,
                employee_id: data.employee_id,
                token: data.token,
                employee_header_button: buttons.employee_header_button,
                employee_add_button: buttons.employee_add_button,
                employee_update_button: buttons.employee_update_button,
                project_header_button: buttons.project_header_button,
                project_add_button: buttons.project_add_button,
                project_update_button: buttons.project_update_button,
                audit_header_button: buttons.audit_header_button
            }
          });
          
        } catch (error) {
          console.error("Error fetching data:", error);
          Swal.fire({
            icon: 'error',
            title: 'An unexpected error occurred.',
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true,
          });
        }
      };

    async function handleButtonVisibility(){
        const employee_id = await sessionStorage.getItem("employee_id");
        try{

        }
        catch(error){
            console.error("Error in response:", data.message);
        }
        const response = await fetch("http://localhost:7011/api/Auth/ButtonVisibility",{
            method: "POST",
            headers: { 'Content-Type': 'application/json','Authorization':sessionStorage.getItem("token") },
            body: JSON.stringify({ employeeId: employee_id })
        })
        const data = await response.json();
        // console.log(data);

        if (!response.ok) {
            console.error("Error in response:", data.message);
            Swal.fire({
              icon: 'error',
              title: data.message,
              toast: true,
              position: 'top-end',
              showConfirmButton: false,
              timer: 3000,
              timerProgressBar: true,
            });
            setTimeout(navigate, 3000, "/dashboard");
            return;
          }

        sessionStorage.setItem("employee-header-button", data[3].can_update);
        sessionStorage.setItem("employee-add-button", data[3].can_insert);
        sessionStorage.setItem("employee-update-button", data[3].can_update);

        sessionStorage.setItem("project-header-button", data[4].can_update);
        sessionStorage.setItem("project-add-button", data[4].can_insert);
        sessionStorage.setItem("project-update-button", data[4].can_update);

        sessionStorage.setItem("audit-header-button", data[1].can_select);


        // console.log("Employee Header Button:", sessionStorage.getItem("employee-header-button"));
        // console.log("Employee Add Button:", sessionStorage.getItem("employee-add-button"));
        // console.log("Employee Update Button:", sessionStorage.getItem("employee-update-button"));
        // console.log("Project Header Button:", sessionStorage.getItem("project-header-button"));
        // console.log("Project Add Button:", sessionStorage.getItem("project-add-button"));
        // console.log("Project Update Button:", sessionStorage.getItem("project-update-button"));
        // console.log("Audit Header Button:", sessionStorage.getItem("audit-header-button"));
        // setTimeout(3000);
        const buttons = {
            employee_header_button: data[3].can_select,
            employee_add_button: data[3].can_insert,
            employee_update_button: data[3].can_update,
            project_header_button: data[4].can_update,
            project_add_button: data[4].can_insert,
            project_update_button: data[4].can_update,
            audit_header_button: data[1].can_select
        }

        return buttons;
        
    }

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
                    {/* <p className="forgot-password">
                        <a href="#" onClick={() =>navigate("/reset-password")}>
                            Forgot Your Password?
                        </a>
                    </p> */}
                </form>
            </div>
        </div>
    );
};  

export default Login;
