import React from 'react'
import {GetAllProjects} from '../Requests/Requests';
import {useState,useEffect} from 'react';
import NavbarComponent from '../Components/NavbarComponent';
import ShowProjectCardComponent from '../Components/ShowProjectCardComponent';
import ListingProjectsInfoComponent from '../Components/ListingProjectsInfoComponent';
import {UserIsAuthenticated} from '../Services/AuthService';

function ListAllProjectsComponent() {

    const [projects, setProjects] = useState([]);

    useEffect(() => {
        
        if(UserIsAuthenticated() === true){
            GetAllProjects().then(response => {

                setProjects(response.data.data);
            })
        }
      
    })
    return (

        

        <div>
        <NavbarComponent/> 
        <ListingProjectsInfoComponent  message={"Tüm Projeler Listeleniyor"} projectCount={projects.length}/>
        
        {
         projects.map(project => {

            return <ShowProjectCardComponent project={project} />
        })}
        </div>
    )
}

export default ListAllProjectsComponent
