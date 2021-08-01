import React from 'react'
import '../Components/ListingProjectsInfoComponent.css';
import { Link } from 'react-router-dom';
import moment from 'moment'

function ShowProjectCardComponent(props) {

    const {project} = props;

    const getProjectDetailUri = () => {

        return "/Project/Detail/"+project.id;
    }
    
    return (
        
        <div>
            {project ? <div className="row">
            <div className="col-md-2"></div>
            <div className="col-md-8">
            <div class="card rounded border-info mb-3 ">
            <div class="card-body text-secondary">
                <h5 class="card-title">{project.projectName}</h5>
                <p className="card-text">Üye Sayısı: <b>{project.memberCount}</b>,&nbsp;    &nbsp; Yönetici : <b>{project.projectLeader.name}</b> <b>{project.projectLeader.lastName}</b>,    &nbsp;    &nbsp; Görev Sayısı : <b>{project.taskCount}</b>,    &nbsp;    &nbsp; Oluşturulma Tarihi : <b>{moment(project.created).format('DD/MM/YYYY')}</b>,     &nbsp;    &nbsp; Statü : {project.isActive === true ? <b>Aktif</b> : <b>Deaktif</b>}</p>
                <div className="row">
                    <div className="col-md-1"></div>
                    <div className="col-md-10 ml-1">
                        <Link to={getProjectDetailUri} className="btn btn-outline-primary">İncele</Link>
                    </div>
                    <div className="col-md-1"></div>
                </div>
                
            </div>
            </div>
            </div>
            <div className="col-md-2"></div>
        </div> :null}
        </div>
   
    )
}

export default ShowProjectCardComponent
