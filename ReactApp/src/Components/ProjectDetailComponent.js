import React from 'react'
import { Link, useParams } from 'react-router-dom'
import {useState,useEffect} from 'react';
import {GetProjectWithDetail,EditProject,DeactiveProject,ActivateProject} from '../Requests/Requests';
import toast, { Toaster } from 'react-hot-toast';
import ShowProjectDetailComponent from '../Components/ShowProjectDetailComponent';
import NavbarComponent from '../Components/NavbarComponent';
import ListProjectTasksComponent from './ListProjectTasksComponent';
import {UserIsLeader} from '../Services/AuthService';

function ProjectDetailComponent(props) {


    const [project, setProject] = useState(null)
    const {projectId}=useParams();
    const [userIsMember, setUserIsMember] = useState(false)
    const [pageLoaded, setPageLoaded] = useState(false)

    const [projectName, setProjectName] = useState("");
    const [projectDescription, setProjectDescription] = useState("")
    const [departmentId, setDepartmentId] = useState(0)

    const [projectChanged, setProjectChanged] = useState(false)

    useEffect(() => {

        GetProjectWithDetail(projectId).then(response => {

            setProject(response.data.data);
        
            setProjectName(response.data.data.projectName);
            setProjectDescription(response.data.data.projectDescription);
            setDepartmentId(response.data.data.department.id);

            setUserIsMember(true);
            setPageLoaded(true);
        }).catch(error => {

            toast.error(error.response.data, {duration:2500});
        
            setTimeout(function() {
                setTimeout(props.history.push('/Departments'))
            }, 3000);   
           
        })

        setProjectChanged(false);

    }, [projectId, projectChanged])


    const getProjectMemberOperationsUri = () => {

        return "/Project/"+project.id+"/MemberOperations";
    }


    const makeEditProjectRequst = () => {

        const body = 
        {
            id : projectId,
            projectName : projectName,
            projectDescription : projectDescription,
            departmentId : departmentId,
            projectLeaderId : project.projectLeader.id
        };

        EditProject(body).then(response => {

            setProjectChanged(true);
            toast.success(response.data.message, {duration:1750});
             
        }).catch(error => {
            toast.error(error.response.data.message, {duration:2000});
        })
    }


    const clearEditProjectFields = () => {

        setProjectName("");
        setProjectDescription("");
        setDepartmentId(0);
    }



    const makeDeactivateProjectRequest = () => {

        DeactiveProject(projectId).then(response => {

            setProjectChanged(true);
            toast.success(response.data.message, {duration:1750});

        }).catch(error => {

            toast.error(error.response.data.message, {duration:2000});

        })
    }



    const makeActivateProjectRequest = () => {

        ActivateProject(projectId).then(response => {

            setProjectChanged(true);
            toast.success(response.data.message, {duration:1750});

        }).catch(error => {

            toast.error(error.response.data.message, {duration:2000});

        })
    }

    return (
        <>
        <NavbarComponent/>
        <div>
        <div>
            {project ? <div className="bg-light p-5 rounded offset-md-1 col-md-10 border border-info mt-5 ">
                <h2>{project.projectName}</h2>
                <p className="lead">{project.projectDescription}</p>
                <p className="lead">Yönetici : {project.projectLeader.name} {project.projectLeader.lastName}</p>
                <p className="lead">Üye Sayısı : {project.memberCount}</p>
                <p className="lead">Oluşturulma : {project.created} , Son Güncelleme : {project.lastUpdated}</p>
               {UserIsLeader() === true ? 
               <div className="button-group">

                    {project.isActive === true
                    ? <Link to={getProjectMemberOperationsUri} className="mt-2 mr-3 btn btn-outline-primary">Üye İşlemleri</Link>
                    : <button  className="mt-2 mr-3 btn btn-outline-danger disabled">Üye İşlemleri</button>}
                    
                    {project.isActive === true
                    ? <button data-toggle="modal" data-target="#editProjectModal" className="mt-2 btn btn-outline-primary mr-3">Projeyi Güncelle</button>
                    : <button className="mt-2 btn btn-outline-danger disabled mr-3">Projeyi Güncelle</button>}
                    
                    {project.isActive === true 
                    ? <button data-toggle="modal" data-target="#deactiveProjectModal" className="mt-2 btn btn-outline-danger">Projeyi Deaktif Et</button>
                    : <button data-toggle="modal" data-target="#activeProjectModal" className="mt-2 btn btn-outline-info">Projeyi Aktif Et</button>}
               </div>
               : null}
            </div> : null}
        </div>
            {userIsMember === true ? <ListProjectTasksComponent project={project}/> : null}
            <div style={{height:"1px"}}></div>

            {/* EDİT PROJECT MODAL */}
            {pageLoaded === true ? 
            <div class="modal fade" id="editProjectModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">Projeyi Güncelle</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                <b>Güncellenecek Projenin Bilgilerini Giriniz.</b>

                <div class="input-group mb-3 mt-4">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="inputGroup-sizing-default">Proje Adı<span className="text-danger">*</span></span>
                        </div>
                        <input onChange={(e) => setProjectName(e.target.value)} value={projectName} type="text" class="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default"/>
                </div>

                <div class="input-group mb-3 mt-4">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="inputGroup-sizing-default">Açıklama<span className="text-danger">*</span></span>
                        </div>
                        <input onChange={(e) => setProjectDescription(e.target.value)} value={projectDescription}  type="text" class="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default"/>
                </div>



                <div class="input-group mb-3 mt-3">
                    <div class="input-group-prepend">
                        <label class="input-group-text" for="inputGroupSelect01">Departman<span className="text-danger">*</span></label>
                    </div>
                    <select onChange={(e) => setDepartmentId(e.target.value)} value={departmentId}  class="custom-select" id="inputGroupSelect01">
                    <option selected value="0">Departman</option>
                        <option value="1">Ödeme Sistemleri</option>
                        <option value="2">Bilişim Çözümleri</option>
                        <option value="3">İnsan Kaynakları</option>
                        <option value="4">Simülasyon</option>
                        <option value="55">Komuta Kontrol Sistemleri</option>
                    </select>
                </div>


                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Kapat</button>
                    <button onClick={makeEditProjectRequst} type="button" class="btn btn-primary">Güncelle</button>
                </div>
                </div>
            </div>
        </div> : null}
            
            {/* DEACTIVATE PROJECT MODAL */}
            {pageLoaded === true 
            ?<div class="modal fade" id="deactiveProjectModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    </div>
                    <div class="modal-body">
                    <h5>Projeyi Deaktif Edilecek. Onaylıyor musunuz?</h5>
                    </div>
                    <div class="modal-footer">
                    <button type="button" class="btn  btn-danger" data-dismiss="modal">Kapat</button>
                    <button onClick={makeDeactivateProjectRequest} type="button" class="btn  btn-primary">Onayla</button>
                    </div>
                </div>
                </div>
          </div> : null}
            
            {/* ACTIVATE PROJECT MODAL */}
          {pageLoaded === true 
            ?<div class="modal fade" id="activeProjectModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    </div>
                    <div class="modal-body">
                    <h5>Projeyi Aktif Edilecek. Onaylıyor musunuz?</h5>
                    </div>
                    <div class="modal-footer">
                    <button type="button" class="btn  btn-danger" data-dismiss="modal">Kapat</button>
                    <button onClick={makeActivateProjectRequest} type="button" class="btn  btn-primary">Onayla</button>
                    </div>
                </div>
                </div>
          </div> : null}
            <Toaster/>
        </div>
        </>
    )
}

export default ProjectDetailComponent
