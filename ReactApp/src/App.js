
import './App.css';
import { BrowserRouter, BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import LoginComponent from './Components/LoginComponent';
import 'react-toastify/dist/ReactToastify.css'
import ListDepartmentsComponent from './Components/ListDepartmentsComponent';
import DepartmentInfoComponent from './Components/DepartmentInfoComponent';
import ListProjectsComponent from './Components/ListProjectsComponent';
import ProjectDetailComponent from './Components/ProjectDetailComponent';
import MemberOperationsComponent from './Components/MemberOperationsComponent';
import CreateProjectComponent from './Components/CreateProjectComponent';
import ListTaskChangesComponent from './Components/ListTaskChangesComponent';
import ListAllProjectsComponent from './Components/ListAllProjectsComponent';
import ListFilteredProjectsComponent from './Components/ListFilteredProjectsComponent';



function App() {
  return (
    <div className="App">
      
      <BrowserRouter>
      <Switch>
      <Router>
          <Route path="/Login" component={LoginComponent}></Route>
          <Route exact path="/" component={ListDepartmentsComponent}></Route>
          <Route path="/Departments" component={ListDepartmentsComponent}></Route>
          <Route  path="/Department/:departmentId" component={DepartmentInfoComponent}></Route>
          <Route  path="/Projects/:departmentId" component={ListProjectsComponent}></Route>
          <Route  path="/Project/Detail/:projectId" component={ProjectDetailComponent}></Route>
          <Route  path="/Project/:projectId/MemberOperations" component={MemberOperationsComponent}></Route>
          <Route  path="/Project/Create" component={CreateProjectComponent}></Route>
          <Route  path="/Task/:taskId/Changes" component={ListTaskChangesComponent}></Route>
          <Route exact path="/Projects" component={ListAllProjectsComponent}></Route>
          <Route exact path="/FilterProjects/:filtertext" component={ListFilteredProjectsComponent}></Route>
      </Router>
      </Switch>
      </BrowserRouter>
      
     
    </div>
  );
}

export default App;
