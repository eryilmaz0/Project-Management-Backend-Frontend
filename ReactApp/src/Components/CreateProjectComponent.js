import React from 'react'
import NavbarComponent from '../Components/NavbarComponent';
import {useState} from 'react';
import {CreateProject} from '../Requests/Requests';
import toast, { Toaster } from 'react-hot-toast';

function CreateProjectComponent(props) {

const [projectName, setProjecttName] = useState("")
const [department, setDepartment] = useState(0)
const [projectDescription, setProjectDescription] = useState("")

const ListProjectsUri = () => {

    return "/Projects/" +department;
}


const sendCreateProjectRequest = () => {

const body = {projectName : projectName, projectDescription : projectDescription, departmentId : department};

CreateProject(body).then(response => {

    
    toast.success(response.data.message, {duration:2000});
    setTimeout(() => {
        props.history.push(ListProjectsUri())
        }, 3000);
    
}).catch(error => {

    toast.error(error.response.data.message, {duration:2000});
})
}

    return (
        <>
            <NavbarComponent/>
            <div className="row mt-5" >
                <div className="col-md-2"></div>
                <div className="col-md-8">
                <div class="card text-center mt-4 border border-info">
                <div class="card-header">
                    <b style={{fontSize:"22px"}}>Proje Oluştur</b>
                </div>
                <div class="card-body mt-3">
                    <h5 class="card-title">Oluşturulacak Projenin Bilgilerini Giriniz.</h5>
                    
                    <div className="col-md-6 offset-md-3 input-groupp">
                    <div class="input-group mb-3 mt-5">
                        <div class="input-group-prepend">
                            <label class="input-group-text" id="inputGroup-sizing-default">Proje Adı <span className="text-danger">*</span></label>
                        </div>
                        <input onChange={(e) => setProjecttName(e.target.value)} type="text" class="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default"/>
                    </div>


                    <div class="input-group mb-3 mt-5">
                        <div class="input-group-prepend">
                            <label class="input-group-text" for="inputGroupSelect01">Departman <span className="text-danger">*</span></label>
                        </div>
                        <select onChange={(e) => setDepartment(e.target.value)} class="custom-select" id="inputGroupSelect01">
                            <option selected value="0">Departman</option>
                            <option value="1">Ödeme Sistemleri</option>
                            <option value="2">Bilişim Çözümleri</option>
                            <option value="3">İnsan Kaynakları</option>
                            <option value="4">Simülasyon</option>
                            <option value="5">Komuta Kontrol Sistemleri</option>
                        </select>
                    </div>


                    <div class="input-group mb-3 mt-5">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="inputGroup-sizing-default"> Açıklama <span className="text-danger">*</span></span>
                        </div>
                        <textarea onChange={(e) => setProjectDescription(e.target.value)} type="text" class="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default"/>
                    </div>


                    <button onClick={sendCreateProjectRequest} class="btn btn-primary mt-5 btn-block">Oluştur</button>
                    </div>
                </div> 
            </div>
                </div>
                <div className="col-md-2"></div>
                <Toaster/>
            </div>

           
        </>
    )
}

export default CreateProjectComponent
