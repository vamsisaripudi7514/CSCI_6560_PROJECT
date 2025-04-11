import React from "react";
import {Container,Button} from 'react-bootstrap';
import {useNavigate} from 'react-router-dom';
import { useLocation } from "react-router-dom";
function PageNotFound(){
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
    const navigate = useNavigate();
    return(
        <Container className="text-center d-flex flex-column align-items-center justify-content-center vh-100">
            <h1 className="display-1 text-danger">404</h1>
            <h2 className="mb-3">Page Not Found</h2>
            <p className="text-muted">Oops! The page you are looking for does not exist.</p>
            <Button variant="primary" onClick={() => navigate("/")}>
                Go Home
            </Button>
        </Container>
    );
}

export default PageNotFound;