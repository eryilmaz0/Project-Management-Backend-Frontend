import React from 'react'
import { Link,useHistory  } from 'react-router-dom';
import {ToastContainer} from 'react-toastify';
import {UserIsLeader} from '../Services/AuthService';
import {useState} from 'react';



function NavbarComponent(props) {

    const [filterText, setFilterText] = useState();
     const history = useHistory();


     const UserIsAuthenticated = () => {

        let authenticatedUser = null;
    
        authenticatedUser = GetAuthenticatedUserInfo();
    
        return authenticatedUser != null ? true : false;
    
    }
    
    

    
     const GetAuthenticatedUserInfo = () => {
    
        var authenticatedUser = null;
    
        authenticatedUser = JSON.parse(localStorage.getItem("auth_token"));
    
        if(authenticatedUser == null){
    
            authenticatedUser = JSON.parse(sessionStorage.getItem("auth_token"));
        }
    
        return authenticatedUser;
    }



    const Logout = () => {

        if(localStorage.getItem("auth_token")){

            localStorage.removeItem("auth_token");
        }

        sessionStorage.removeItem("auth_token");
        setTimeout(() => {
            history.push('/Login')
            }, 1000);
    }


    const RedirectFilteredProjectsUri = () => {

        return "FilterProjects/" +filterText;
    }


    

    return (
        <div>
            <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
                
                <div className="collapse navbar-collapse" id="navbarNavDropdown">
                    <ul className="navbar-nav">
                    <li className="nav-item active">
                        <Link className="nav-link" to="/Departments">Departmanlar <span class="sr-only">(current)</span></Link>
                    </li>
                    <li className="nav-item active  ">
                        <Link class="nav-link" to="/Projects">Projeler</Link>
                    </li>
                    

                    
                    {UserIsLeader() == true ?<li className="nav-item active  ">
                        <Link class="nav-link" to="/Project/Create">Proje&nbsp;Ekle</Link>
                    </li>
                     : null }

                    
                    <input onChange={(e) => setFilterText(e.target.value)} style={{marginLeft:"600px"}} size="20" class=" mr-sm-2" type="search" placeholder="Arama" aria-label="Search"/>
                    <Link to={RedirectFilteredProjectsUri} class="btn btn-outline-info my-2 my-sm-0">Arama</Link>
                    
                    
                    {UserIsAuthenticated() === true ?

                    
                        <li className="nav-item dropdown active" style={{marginLeft:"5px"}}>

                        <a className="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                        {
                            <img className="mr-2 mb-1" alt="" src={'http://localhost:3000/img/profilepictures/'+GetAuthenticatedUserInfo().picture} />
                            
                        }

                        {
                            GetAuthenticatedUserInfo().name
                        }
                        </a>
                        <div className="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                        <Link onClick={Logout} className="dropdown-item">Çıkış Yap</Link>

                        </div>
                        </li>
                                     

                    :
                    <li className="nav-item active"  style={{marginLeft:"50px"}}>
                        <Link className="nav-link" to="/Login">Giriş Yap <span class="sr-only">(current)</span></Link>
                    </li>
                    }

                    </ul>
                </div>
            </nav>
            <ToastContainer/>
        </div>
    )
}

export default NavbarComponent
