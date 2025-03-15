import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import Landing from './pages/Landing';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import EmployeeManagement from './pages/EmployeeManagement';
import ProjectManagement from './pages/ProjectManagement';
import PageNotFound from './components/PageNotFound';
import AuditLogs from './pages/AuditLogs';
import Header from './components/Header';
import EmployeeView from './pages/EmployeeView';
import EmployeeEdit from './components/EmployeeEdit';
import ProjectMappingEdit from './components/ProjectMappingEdit';
import EmployeeAdd from './components/EmployeeAdd';
import ProjectView from './pages/ProjectView';
import ProjectEdit from './components/ProjectEdit';
import ProjectAdd from './components/ProjectAdd';
function App() {
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route path='/login' element={<Login />} />
          <Route path='/dashboard' element={<Dashboard />} />
          <Route path='/landing' element={<Landing />} />
          <Route path='/employee-management' element={<EmployeeManagement />} />
          <Route path='/project-management' element={<ProjectManagement />} />
          <Route path='/audit-logs' element={<AuditLogs />} />
          <Route path='employee-view' element={<EmployeeView/>}/>
          <Route path='/employee-edit' element={<EmployeeEdit/>}/>
          <Route path='/project-mapping-edit' element={<ProjectMappingEdit/>}/>
          <Route path='/employee-add' element={<EmployeeAdd/>}/>
          <Route path='/project-view' element={<ProjectView/>}/>
          <Route path='/project-edit' element={<ProjectEdit/>}/>
          <Route path='/project-add' element={<ProjectAdd/>}/>
          <Route path='*' element={<PageNotFound />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
