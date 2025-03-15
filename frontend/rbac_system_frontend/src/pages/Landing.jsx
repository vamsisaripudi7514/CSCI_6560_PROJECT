import React from "react";
import { useNavigate ,Link} from "react-router-dom";
import "../styles/landing-style.css";
import Login from "./Login";
const Landing = () => {
    

    return (
        <div style={{alignItems: 'center'}}>
            <div className="landing">
                <h1>Welcome to the Secure Role-Based Access System</h1>
                <p>Ensuring data privacy with role-based security enforcement</p>
                <Link to="/login"><button>Login</button></Link>
            </div>
        </div>
    );
};

export default Landing;
