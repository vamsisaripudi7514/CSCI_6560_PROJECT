import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import "admin-lte/dist/css/adminlte.min.css"; // AdminLTE CSS
import "bootstrap/dist/css/bootstrap.min.css"; // Bootstrap CSS
import "font-awesome/css/font-awesome.min.css"; // FontAwesome Icons

import $ from "jquery"; // Import jQuery
import "bootstrap/dist/js/bootstrap.bundle.min"; // Import Bootstrap JS
import "admin-lte/dist/js/adminlte.min"; // Import AdminLTE JS



const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
