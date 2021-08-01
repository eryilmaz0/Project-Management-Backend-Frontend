import React from 'react'
import {useEffect} from 'react';
import { Link } from 'react-router-dom';

function ShowDepartmentCardComponent(props) {

    const {department} = props;
    const departmentpicture = "";


    const getDepartmentId = () => {

        return department.id;
    }


    const getDepartmentInfoUri = () => {

        return '/Department/'+getDepartmentId();
    }


    const getListProjectsByDepartmentUri = () => {

        return '/Projects/' + getDepartmentId();
    }
   

    return (
        <div class="col-md-4">
                <div class="card mb-4 box-shadow">
                <div class="card-header">
                    <b>{department.departmentName}</b>
                </div>
                   <img src={'img/departmentpictures/department'+getDepartmentId()+'.jpg'} />
                    <div class="card-body">
                    
                    <div class="d-flex justify-content-between align-items-center" style={{marginLeft:"90px"}}>
                        <div class="btn-group ml-2">
                        <Link to={getDepartmentInfoUri} type="button" class="btn btn-sm btn-outline-primary mr-1">Bilgi</Link>
                        <Link to={getListProjectsByDepartmentUri} type="button" class="btn btn-sm btn-outline-primary">Projeler</Link>
                        </div>
                        <small class="text-muted"><b>{department.projectCount} Proje</b></small>
                    </div>
                    </div>
                </div>
                </div>
    )
}

export default ShowDepartmentCardComponent
