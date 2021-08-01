import React from 'react'
import {useState, useEffect } from 'react';
import { useParams } from 'react-router';
import {GetDepartmentDetail} from '../Requests/Requests';
import toast, { Toaster } from 'react-hot-toast';
import NavbarComponent from '../Components/NavbarComponent';

function DepartmentInfoComponent() {

    const [department, setDepartment] = useState(null);
    const [isDepartmentExist, setIsDepartmentExist] = useState(false);

    const {departmentId} = useParams();

    useEffect(() => {
        
        GetDepartmentDetail(departmentId).then(response => {

            setDepartment(response.data.data);
            setIsDepartmentExist(true);

        }).catch(error => {

            toast.error(error.response.data, {duration:1750});
        })

    }, [departmentId])


    const getDepartmentId = () => {

        return "department"+departmentId;
    }
    
    return (
        <div>
            <NavbarComponent />
            {isDepartmentExist ? 
            <div className="row mt-5">
            <div className="col-md-6 offset-md-3 mb-5">
            <div className="card text-center">
            <div className="card-header">
            <h4 className="mt-1">{department.departmentName}</h4>
            </div>
            <img alt=""  src={'http://localhost:3000/img/departmentpictures/'+getDepartmentId()+'.jpg'} />
            <div className="card-body">
                <h5 className="card-title">Departman Bilgisi</h5>
                <p className="card-text">{department.description}</p>
            </div>
            <div className="card-footer text-muted">
                Bu Departmanda <b>{department.projectCount}</b> Adet Proje Bulunmaktadır.
            </div>
        </div>
            </div>
        </div> : null}
            <Toaster/>
        </div>
    )
}

export default DepartmentInfoComponent
