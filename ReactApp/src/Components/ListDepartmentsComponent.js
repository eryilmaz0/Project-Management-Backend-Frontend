import React from 'react'
import {useState,useEffect} from 'react';
import {GetDepartmentsRequest} from '../Requests/Requests'
import {ToastContainer,toast, Slide} from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import NavbarComponent  from './NavbarComponent';
import '../Components/ListDepartmentsComponent.css';
import ShowDepartmentCardComponent from './ShowDepartmentCardComponent';


function ListDepartmentsComponent() {


    const [departments, setDepartments] = useState([])
    const[pageLoaded, setpageLoaded] = useState(false);


    useEffect(() => {
       
        if(!pageLoaded){

            setpageLoaded(true);
            GetDepartmentsRequest().then(response => {

                setDepartments(response.data.data);
            }).catch(error => {
                listDepartmentsFailToast(error.response.data);
            })
        }

    })


    const listDepartmentsFailToast = (errorMessage) => 
        toast.error(errorMessage, {
          position: "top-right",
          autoClose: 1500,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
          transition: Slide
          }
    );

    return (

       
        <div>
             <NavbarComponent/>
             <main role="main">
             <section className="jumbotron text-center bg-white">
                <div className="container">
                <h1 className="jumbotron-heading">Proje Yönetim Sistemi</h1>
                <p className="lead text-muted mt-4">
                    Proje Yönetim Sistemine Hoşgeldiniz. Bu Sistem Üzerinde Yeni Projeler Oluşturabilir,
                    Oluşturulan Projelere Üye Ekleyip Çıkarabilir, Projede Görev Alan Üyelere Görevler Atayabilir,
                    Bu Görevleri Güncelleyebilirsiniz. 
                </p>
                </div>
      </section>

      <div class="album py-5 bg-light">
        <div class="container">

        <h3 className="text-center mb-5">Departmanlar</h3>
        <div class="row">
           
           {departments.map(department => {

               return <ShowDepartmentCardComponent key={department.id} department={department}/>
           })}
                
            </div>
        </div>
      </div>


             </main>
            <ToastContainer/>
        </div>
    )
}

export default ListDepartmentsComponent
