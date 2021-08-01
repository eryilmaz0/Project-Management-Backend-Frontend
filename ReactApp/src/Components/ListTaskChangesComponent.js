import React from 'react'
import { useParams } from 'react-router-dom';
import NavbarComponent from '../Components/NavbarComponent';
import {GetTaskChanges,GetTaskById} from '../Requests/Requests';
import {useState,useEffect} from 'react';

function ListTaskChangesComponent() {


    const {taskId} = useParams();
    const [taskChanges, setTaskChanges] = useState([]);
    const [task, setTask] = useState(null)

    const [pageLoaded, setPageLoaded] = useState(false)


    const GetTaskChangesRequest = () => {

        GetTaskChanges(taskId).then(response => {

            setTaskChanges(response.data.data)
        })
    }


    const GetTaskDetailRequest = () => {

        GetTaskById(taskId).then(response => {

           setTask(response.data.data)
           setPageLoaded(true);
        })
    }

    useEffect(() => {
        GetTaskChangesRequest();
        GetTaskDetailRequest();
    }, [taskId])


   

    return (

        <>
        <NavbarComponent/>
        <div>

        {/* TASK DETAIL CARD */}
        {pageLoaded === true ? 
        <div class="mb-5 card offset-md-3 col-md-6 mt-5 border border-info">
            <div className="card-body">
                <h5 className="card-title">Görevin Güncel Durumu</h5>
                <p className="mt-4"><b>Açıklama :</b> {task.taskDescription}</p>
                <p><b>Atanan Kişi : </b>
                    <img className="mr-1 mb-1 ml-1" alt="" src={'http://localhost:3000/img/profilepictures/'+task.user.picture} />
                    {task.user.name} {task.user.lastName}
                </p>
                <p><b>Görev Tipi :</b>   {{
                                            1 : <span className="text-success">Task</span>,
                                            2 : <span className="text-danger">Bug</span>,
                                            3 : <span className="text-primary">Epic</span>
                                          }[task.type]}</p>
                
                <p><b>Öncelik Durumu : </b>{{
                                            1 : <span>Low</span>,
                                            2 : <span>Normal</span>,
                                            3 : <span className="text-warning">Important</span>,
                                            4 : <span className="text-danger">Critical</span>,
         
                                            }[task.priority]}</p>


                <p><b>Görev Durumu :</b> {{
                                            1 : <span className="text-danger">To Do</span>,
                                            2 : <span className="text-primary">In Progress</span>,
                                            3 : <span className="text-success">Done</span>
                                          }[task.status]}</p>
            </div>
        </div> : null}
        
        
        
        <h4 className="mb-4">Görev Geçmişi</h4>
       
       {pageLoaded === true ? 
       
       taskChanges.map(change => {

        return  <div class="card rounded border border-info col-md-6 offset-md-3 mb-3">
        <div class="card-body">
            <b style={{fontSize:"17px"}} class="card-title">{change.user.name} {change.user.lastName}, 22/07/2021 Tarihinde Güncelledi</b><br/>
            <span class="mt-2 card-text"><b>Açıklama : </b>{change.taskDescriptionValue}</span><br/>
            <span> <b>Öncelik : </b>
                {{
                    1 : <span>Low</span>,
                    2 : <span>Normal</span>,
                    3 : <span className="text-warning">Important</span>,
                    4 : <span className="text-danger">Critical</span>,
         
                }[change.priorityValue]}
            </span><br/>

            <span><b>Durumu : </b>
                {{
                    1 : <span className="text-danger">To Do</span>,
                    2 : <span className="text-primary">In Progress</span>,
                    3 : <span className="text-success">Done</span>
                }[task.status]}
            </span>
                
             
        </div>
        </div>
    }) : null}

        </div>
        <div className="row" style={{marginTop:"90px"}}></div>
        </>
    )
}

export default ListTaskChangesComponent
