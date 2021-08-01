import React from 'react'
import { useEffect,useState } from 'react';
import { useParams } from 'react-router-dom'
import {GetProjectsByDepartment} from '../Requests/Requests';
import toast, { Toaster } from 'react-hot-toast';
import ListingProjectsInfoComponent from './ListingProjectsInfoComponent';
import '../Components/ListingProjectsInfoComponent.css';
import NavbarComponent from '../Components/NavbarComponent';
import ShowProjectCardComponent from './ShowProjectCardComponent';

function ListProjectsComponent() {

    const {departmentId} = useParams();
    const [pageLoaded, setPageLoaded] = useState(false)
    const [projects, setProjects] = useState([])


    useEffect(() => {
        
        if(pageLoaded === false){

            setPageLoaded(true);
            GetProjectsByDepartment(departmentId).then(response =>{

                setProjects(response.data.data);
                
                 
            }).catch(error => {

                toast.error(error.response.data, {duration:2000})

            })

        }
    })


    return (
        <div >
            <NavbarComponent/>
            <ListingProjectsInfoComponent message={"Departmana Göre Listeleniyor"} projectCount={projects.length}/>
            
           {projects.map(project => {
               return <ShowProjectCardComponent project={project} key={project.id}/>
           })}

            <Toaster/>
           
        </div>
    )
}

export default ListProjectsComponent
