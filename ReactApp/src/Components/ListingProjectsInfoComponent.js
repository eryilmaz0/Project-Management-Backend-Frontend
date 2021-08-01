import React from 'react'
import { useState,useEffect } from 'react';
import {GetDepartmentDetail} from '../Requests/Requests';

function ListingProjectsInfoComponent(props) {

    const {message, projectCount} = props;

   

    return (
        <div className="row">
            <div className="col-md-8 offset-md-2">
            <div class="d-flex align-items-center p-3 my-3 mt-5 text-white rounded shadow-sm bg-purple  ">
            <img class="me-3" src={'http://localhost:3000/img/icons/information.png'} alt="" />
            <div class="lh-1 ml-3 mt-1">
            {message ? <h1 class="h6 mb-0 text-white lh-1">{message}</h1>
            : null}

            {projectCount ? <small style={{marginRight:"120px"}} class="h6 mb-0 text-white lh-1">{projectCount} Proje</small> : null}
            </div>
        </div>
            </div>
        </div>
    )
}

export default ListingProjectsInfoComponent
