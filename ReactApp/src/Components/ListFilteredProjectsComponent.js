import React from 'react'
import { useParams } from 'react-router-dom'
import {UserIsAuthenticated} from '../Services/AuthService';
import NavbarComponent from './NavbarComponent';
import ListingProjectsInfoComponent from '../Components/ListingProjectsInfoComponent';
import {useState,useEffect} from 'react';
import {GetFilteredProjects} from '../Requests/Requests';
import ShowProjectCardComponent from '../Components/ShowProjectCardComponent';

function ListFilteredProjectsComponent() {

    const [filteredProjects, setFilteredProjects] = useState([]);
    const {filtertext} = useParams();


    useEffect(() => {
        if(UserIsAuthenticated() === true){
            GetFilteredProjects(filtertext).then(response => {

                setFilteredProjects(response.data.data);
            })
        }
    }, [filtertext])

    
    return (
        <div>
             <NavbarComponent/> 

            <ListingProjectsInfoComponent  message={"'" +filtertext +"' Kelimesine Göre Filtreleniyor"} projectCount={filteredProjects.length}/>
            
            {
                filteredProjects.map(project => {

                return <ShowProjectCardComponent project={project} />
            })}
        </div>
    )
}

export default ListFilteredProjectsComponent
