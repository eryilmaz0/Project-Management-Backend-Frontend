import React from 'react'
import  '../Components/ListProjectTasksComponent.css';
import {GetTasksByProject,GetProjectMembers,CreateTask,UpdateTask} from '../Requests/Requests';
import {useState,useEffect} from 'react';
import { Link, useParams } from 'react-router-dom';
import { GetAuthenticatedUserInfo , UserIsAuthenticated} from '../Services/AuthService';
import toast, { Toaster } from 'react-hot-toast';
import TaskTableRowComponent from '../Components/TaskTableRowComponent';
import CreateTaskComponent from './CreateTaskComponent';

function ListProjectTasksComponent(props) {

    
    const{project} = props;

    const {projectId} = useParams();
    const [tasks, setTasks] = useState([])
    const [pageLoaded, setPageLoaded] = useState(false)


    //CREATE TASK STATES
    const [taskDescription, setTaskDescription] = useState("")
    const [priority, setPriority] = useState(0)
    const [type, setType] = useState(0)
    const [assignedUser, setAssignedUser] = useState(0)
    

    //Update TASK STATES
    const [selectedTaskId, setSelectedTaskId] = useState(0)
    const [selectedTaskDescription, setSelectedTaskDescription] = useState("")
    const [selectedPriority, setSelectedPriority] = useState(0)
    const [selectedStatus, setSelectedStatus] = useState(0)
    const [selectedAssignedUserId, setSelectedAssignedUserId] = useState(0)

    const [projectMembers, setProjectMembers] = useState([])

    const [tasksUpdated, setTasksUpdated] = useState(false)

    


    const clearCreateTaskInputs = () => {

        setTaskDescription("");
        setPriority(0);
        setType(0);
        setAssignedUser(0);
    }

    useEffect(() => {
       
        GetTasksByProject(projectId).then(response => {
            setTasks(response.data.data);
        }).catch(error => {
            toast.error(error.response.data, {duration:1750});
        })

        setPageLoaded(true);
        setTasksUpdated(false);
        console.log(project);
    }, [projectId, tasksUpdated])


    useEffect(() => {
        GetProjectMembers(projectId).then(response => {

            setProjectMembers(response.data.data);
        }).catch(error => {

            toast.error(error.response.data.message, {duration:2000});

        })
    }, [projectId])



    const makeCreateTaskRequest = () => {

        const body = 
        {
            projectId:projectId,
            taskDescription:taskDescription,
            priority : priority,
            type : type,
            assignedUserId:assignedUser
        };

        CreateTask(body).then(response => {

            toast.success("Görev Oluşturuldu.", {duration:2000});
            setTasksUpdated(true);
        }).catch(error => {

            if(error.response.status === 403)
                toast.error("Bu İşlem İçin Yetkiniz Yok.", {duration:2000});
            
            else
                toast.error(error.response.data.message, {duration:2000});
        })

        clearCreateTaskInputs();
    }


    const setSelectedTaskValues = (selectedTask) => {

        setSelectedTaskId(selectedTask.id);
        setSelectedTaskDescription(selectedTask.taskDescription);
        setSelectedPriority(selectedTask.priority);
        setSelectedStatus(selectedTask.status);
        setSelectedAssignedUserId(selectedTask.assignedUser.id);
    }


    const clearSelectedValues = () => {

        setSelectedTaskId(0);
        setSelectedTaskDescription("");
        setSelectedPriority(0);
        setSelectedStatus(0);
        setSelectedAssignedUserId(0);
    }


    const makeUpdateTaskRequest = () => {

        const body = 
        {
            Id:selectedTaskId,
            taskDescription : selectedTaskDescription,
            priority : selectedPriority,
            status : selectedStatus,
            assignedUserId : selectedAssignedUserId
        };

        UpdateTask(body).then(response => {

            toast.success("Görev Başarıyla Güncellendi.", {duration:1750});
            setTasksUpdated(true);
            clearSelectedValues();
            
        }).catch(error => {

            console.log(error.response);
            toast.error(error.response.data.message, {duration:2000})
        });

        
    }


    return (
        <div>
            <div className="bg-light p-5 rounded offset-md-1 col-md-10 border border-info mt-5 mb-5">
                <h3>Görevler</h3>
                <div className="row">
                    <div className="col-md-10"></div>
                    <div className="col-md-1">
                    {project.isActive === true 
                    ? <button data-toggle="modal" data-target="#createTaskModal" className="offset-md-11 mt-4 mb-1 btn btn-info btn">Oluştur</button>
                    : <button className="offset-md-11 mt-4 mb-1 btn btn-danger btn">Oluştur</button>}
                    </div>
                </div>


                 {/* <!-- Create Task Modal --> */}
            <div class="modal fade" id="createTaskModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-align-center" id="exampleModalLongTitle">Görev Oluştur</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    
                    <b>Oluşturulacak Görevin Bilgilerini Giriniz.</b>
                    <div class="input-group mb-3 mt-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="inputGroup-sizing-default">Açıklama  <span className="text-danger">*</span></span>
                            </div>
                            <input value={taskDescription} onChange={(e) => setTaskDescription(e.target.value)} type="text" class="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default"/>
                    </div>

                    <div class="input-group mb-3 mt-3">
                        <div class="input-group-prepend">
                            <label class="input-group-text" for="inputGroupSelect01">Öncelik  <span className="text-danger">*</span></label>
                        </div>
                        <select value={priority} onChange={(e) => setPriority(e.target.value)} class="custom-select" id="inputGroupSelect01">
                            <option selected value="0">Öncelik Değeri</option>
                            <option value="1">Low</option>
                            <option className="text-primary" value="2">Normal</option>
                            <option className="text-warning" value="3">Important</option>
                            <option className="text-danger" value="4">Critical</option>
                        </select>
                    </div>


                    <div class="input-group mb-3 mt-3">
                        <div class="input-group-prepend">
                            <label class="input-group-text" for="inputGroupSelect01">Tip <span className="text-danger">*</span></label>
                        </div>
                        <select value={type} onChange={(e) => setType(e.target.value)} class="custom-select" id="inputGroupSelect01">
                            <option selected value="0">Görev Tipi</option>
                            <option className="text-success" value="1">Task</option>
                            <option className="text-danger" value="2">Bug</option>
                            <option className="text-primary" value="3">Epic</option>
                        </select>
                    </div>


                    <div class="input-group mb-3 mt-3">
                        <div class="input-group-prepend">
                            <label class="input-group-text" for="inputGroupSelect01">Üye <span className="text-danger">*</span></label>
                        </div>
                        <select value={assignedUser} onChange={(e) => setAssignedUser(e.target.value)} class="custom-select" id="inputGroupSelect01">
                            <option selected value="0">Üye</option>
                            {projectMembers.map(member => {

                                return <option value={member.id}>{member.name} {member.lastName}</option>

                            })}
                            
                        </select>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Kapat</button>
                    <button onClick={makeCreateTaskRequest} type="button" class="btn btn-primary">Oluştur</button>
                </div>
                </div>
            </div>
            </div>
               
            {/* <!-- Edit Task Modal --> */}
            <div class="modal fade" id="editTaskModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">Görevi Güncelle</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                <b>Güncellenecek Görevin Bilgilerini Giriniz.</b>

                <div class="input-group mb-3 mt-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="inputGroup-sizing-default">Açıklama  <span className="text-danger">*</span></span>
                            </div>
                            <input value={selectedTaskDescription} onChange={(e) => setSelectedTaskDescription(e.target.value)} type="text" class="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default"/>
                    </div>

                    <div class="input-group mb-3 mt-3">
                        <div class="input-group-prepend">
                            <label class="input-group-text" for="inputGroupSelect01">Öncelik  <span className="text-danger">*</span></label>
                        </div>
                        <select value={selectedPriority} onChange={(e) => setSelectedPriority(e.target.value)} class="custom-select" id="inputGroupSelect01">
                            <option selected value="0">Öncelik Değeri</option>
                            <option value="1">Low</option>
                            <option className="text-primary" value="2">Normal</option>
                            <option className="text-warning" value="3">Important</option>
                            <option className="text-danger" value="4">Critical</option>
                        </select>
                    </div>


                    <div class="input-group mb-3 mt-3">
                        <div class="input-group-prepend">
                            <label class="input-group-text" for="inputGroupSelect01">Durum <span className="text-danger">*</span></label>
                        </div>
                        <select value={selectedStatus} onChange={(e) => setSelectedStatus(e.target.value)} class="custom-select" id="inputGroupSelect01">
                            <option selected value="0">Görev Durumu</option>
                            <option className="text-danger" value="1">To Do</option>
                            <option className="text-primary" value="2">In Progress</option>
                            <option className="text-success" value="3">Done</option>
                        </select>
                    </div>


                    <div class="input-group mb-3 mt-3">
                        <div class="input-group-prepend">
                            <label class="input-group-text" for="inputGroupSelect01">Üye <span className="text-danger">*</span></label>
                        </div>
                        <select value={selectedAssignedUserId} onChange={(e) => setSelectedAssignedUserId(e.target.value)} class="custom-select" id="inputGroupSelect01">
                            <option selected value="0">Üye</option>
                            {projectMembers.map(member => {

                                return <option value={member.id}>{member.name} {member.lastName}</option>

                            })}
                            
                        </select>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Kapat</button>
                    <button onClick={makeUpdateTaskRequest} type="button" class="btn btn-primary">Güncelle</button>
                </div>
                </div>
            </div>
            </div>

            <div>
           {tasks ? 

                //LİST TASKS
                <table className="mb-3 table rounded table-hover mt-1">  
                <thead className="text-center table-primary ">
                        <th scope="col">Atanan Kişi</th>
                        <th scope="col">Açıklama</th>
                        <th scope="col">Tipi</th>
                        <th scope="col">Öncelik</th>
                        <th scope="col">Durum</th>
                        <th scope="col">Geçmiş</th>
                        <th scope="col">Düzenle</th>
                </thead>
                <tbody className="text-center">
                    {tasks.map(task => {
                        return <tr key={task.id}>
                        <td>
                        <img className="mr-2 mb-1" alt="" src={'http://localhost:3000/img/profilepictures/'+task.assignedUser.picture} />
                            {task.assignedUser.name} {task.assignedUser.lastName}
                        </td>
                        <td>{task.taskDescription}</td>
         
                        {/* TYPE */}
                         {{
                             1 : <td className="text-success">Task</td>,
                             2 : <td className="text-danger">Bug</td>,
                             3 : <td className="text-primary">Epic</td>
         
                         }[task.type]}
         
                         {/* PRIORITY */}
                         {{
                             1 : <td className="text-success">Low</td>,
                             2 : <td className="text-info">Normal</td>,
                             3 : <td className="text-warning">Important</td>,
                             4 : <td className="text-danger">Critical</td>
         
                         }[task.priority]}
         
                         {/* STATUS */}
                         {{
                             1 : <td className="text-warning">To Do</td>,
                             2 : <td className="text-info">In Progress</td>,
                             3 : <td className="text-success">Done</td>
         
                         }[task.status]}
                        <td><Link to={"/Task/"+task.id+"/Changes"} className="btn btn-sm btn-outline-info">{task.taskChangeCount} Geçmiş</Link></td>
                         <td>
                            {project.isActive === true ? UserIsAuthenticated() === true ? GetAuthenticatedUserInfo().userId == project.projectLeader.id 
                            || GetAuthenticatedUserInfo().userId == task.assignedUser.id 
                            ? <button onClick={() =>setSelectedTaskValues(task)} data-toggle="modal" data-target="#editTaskModal" className="btn btn-sm btn-outline-info">Düzenle</button>
                            : null : null 
                            : <button className="btn btn-sm btn-outline-danger disabled">Düzenle</button>}
                         </td>
                    </tr>
                    })}
                </tbody>
                </table>
            : null}
       </div>
                <Toaster/>
               
        </div>
        </div>
    )
}

export default ListProjectTasksComponent
