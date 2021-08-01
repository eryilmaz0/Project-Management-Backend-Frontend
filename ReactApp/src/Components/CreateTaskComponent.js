import React from 'react'
import {useState, useEffect} from 'react';
import {GetProjectMembers} from '../Requests/Requests';
import toast, { Toaster } from 'react-hot-toast';

function CreateTaskComponent(props) {

    const {projectId} = props;
    const [taskDescription, setTaskDescription] = useState("")
    const [priority, setPriority] = useState(0)
    const [type, setType] = useState(0)
    const [status, setStatus] = useState(0)
    const [assignedUser, setAssignedUser] = useState(0)


    const [projectMembers, setProjectMembers] = useState([])


    useEffect(() => {
        GetProjectMembers(projectId).then(response => {

            setProjectMembers(response.data.data);
        }).catch(error => {

            toast.error(error.response.data.message, {duration:2000});

        })
    }, [projectId])


    return (
        <div>
            <button data-toggle="modal" data-target="#createTaskModal" className="offset-md-10 mt-4 mb-0 btn btn-info btn-sm">Oluştur</button>
            {/* <!-- Create Task Modal --> */}
            <div class="modal fade" id="createTaskModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-center" id="exampleModalLongTitle">Görev Oluştur</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    
                    <b>Oluşturulacak Görevin Bilgilerini Giriniz.</b>
                    <div class="input-group mb-3 mt-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="inputGroup-sizing-default">Açıklama*</span>
                            </div>
                            <input onChange={(e) => setTaskDescription(e.target.value)} type="text" class="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default"/>
                    </div>

                    <div class="input-group mb-3 mt-3">
                        <div class="input-group-prepend">
                            <label class="input-group-text" for="inputGroupSelect01">Öncelik*</label>
                        </div>
                        <select onChange={(e) => setPriority(e.target.value)} class="custom-select" id="inputGroupSelect01">
                            <option selected value="0">Öncelik Değerleri</option>
                            <option value="1">Low</option>
                            <option className="text-primary" value="2">Normal</option>
                            <option className="text-warning" value="3">Important</option>
                            <option className="text-danger" value="4">Critical</option>
                        </select>
                    </div>


                    <div class="input-group mb-3 mt-3">
                        <div class="input-group-prepend">
                            <label class="input-group-text" for="inputGroupSelect01">Tip*</label>
                        </div>
                        <select onChange={(e) => setType(e.target.value)} class="custom-select" id="inputGroupSelect01">
                            <option selected value="0">Görev Tipi</option>
                            <option className="text-success" value="1">Task</option>
                            <option className="text-danger" value="2">Bug</option>
                            <option className="text-primary" value="3">Epic</option>
                        </select>
                    </div>


                    <div class="input-group mb-3 mt-3">
                        <div class="input-group-prepend">
                            <label class="input-group-text" for="inputGroupSelect01">Üye*</label>
                        </div>
                        <select onChange={(e) => setAssignedUser(e.target.value)} class="custom-select" id="inputGroupSelect01">
                            <option selected value="0">Üye</option>
                            {projectMembers.map(member => {

                                return <option value={member.id}>{member.name} {member.lastName}</option>

                            })}
                            
                        </select>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Kapat</button>
                    <button type="button" class="btn btn-primary">Oluştur</button>
                </div>
                </div>
            </div>
            </div>
        </div>
    )
}

export default CreateTaskComponent
