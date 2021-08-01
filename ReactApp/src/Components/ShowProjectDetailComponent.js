import React from 'react'
import { Link } from 'react-router-dom';
import {UserIsLeader} from '../Services/AuthService';


function ShowProjectDetailComponent(props) {

    const {project} = props;

    const getProjectMemberOperationsUri = () => {

        return "/Project/"+project.id+"/MemberOperations";
    }
    return (
        <div>
            {project ? <div className="bg-light p-5 rounded offset-md-1 col-md-10 border border-info mt-5 ">
                <h2>{project.projectName}</h2>
                <p className="lead">{project.projectDescription}</p>
                <p className="lead">Yönetici : {project.projectLeader.name} {project.projectLeader.lastName}</p>
                <p className="lead">Üye Sayısı : {project.memberCount}</p>
                <p className="lead">Oluşturulma : {project.created} , Son Güncelleme : {project.lastUpdated}</p>
               {UserIsLeader() === true ? 
               <div className="button-group">
                    <Link to={getProjectMemberOperationsUri} className="mt-2 mr-3 btn btn-outline-primary">Üye İşlemleri</Link>
                    <button className="mt-2 btn btn-outline-primary">Görev Oluştur</button>
               </div>
               : null}
            </div> : null}
        </div>
    )
}

export default ShowProjectDetailComponent
