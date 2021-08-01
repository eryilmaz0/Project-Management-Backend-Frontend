import React from 'react'
import { useParams } from 'react-router-dom'
import NavbarComponent from '../Components/NavbarComponent';
import ListProjectMembersComponent from './ListProjectMembersComponent';
import ListMembersOutOfProjectComponent from './ListMembersOutOfProjectComponent';
import toast, { Toaster } from 'react-hot-toast';

function MemberOperationsComponent() {

    const {projectId} = useParams();

    return (
        <>
            <NavbarComponent/>
            <ListProjectMembersComponent projectId={projectId}/>
            <ListMembersOutOfProjectComponent projectId={projectId}/>
            
        </>  
        
    )
}

export default MemberOperationsComponent
