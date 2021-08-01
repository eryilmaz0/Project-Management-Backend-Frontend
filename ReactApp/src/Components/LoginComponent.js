import React from 'react'
import loginicon from '../img/icons/loginiconn.png'; 
import '../Components/LoginComponent.css';
import { useState } from 'react';
import {LoginRequest} from '../Requests/Requests';
import 'react-toastify/dist/ReactToastify.css'
import toast, { Toaster } from 'react-hot-toast';

function LoginComponent(props) {


    const [email , setEmail] = useState("");
    const [password , setPassword] = useState("");
    const [rememberMe , setrememberMe] = useState(false);
    
    
    // const loginSuccessToast = () => 
    //     toast.success('Giriş Başarılı', {
    //       position: "top-right",
    //       autoClose: 1000,
    //       hideProgressBar: false,
    //       closeOnClick: true,
    //       pauseOnHover: true,
    //       draggable: true,
    //       progress: undefined,
    //       // transition: Slide
    //       }
    // );


    // const loginFailedToast = (errorMessage) => 
    //     toast.error("🦄" +errorMessage, {
    //       position: "top-right",
    //       autoClose: 1500,
    //       hideProgressBar: false,
    //       closeOnClick: true,
    //       pauseOnHover: true,
    //       draggable: true,
    //       progress: undefined,
    //       textAlign:'right',
    //       // transition: Slide
    //       }
    // );


    const loginSuccessToast = () => toast.success('Giriş Başarılı');
    const loginFailedToast = (failedMessage) => toast.error(failedMessage, {
      duration:1500
    });



    const makeLoginRequest = (event) => {
       
      const loginRequestData = {email: email, password : password};
      LoginRequest(loginRequestData).then(response => {


        rememberMe  ? localStorage.setItem("auth_token", JSON.stringify(response.data.data)) 
                    : sessionStorage.setItem("auth_token", JSON.stringify(response.data.data));

        loginSuccessToast();
        setTimeout(() => {
          props.history.push('/Departments')
          }, 1600);
      }).catch(error => {

        loginFailedToast(error.response.data);
      })
      
        
    }

    
    return (

    <main className="form-signin text-center" style={{marginLeft:"600px", marginTop:"150px"}}>

    <img className="mb-4" alt="" src={loginicon}/>
    <h1 className="h3 mb-3 fw-normal">Giriş Yap</h1>

        <div className="form-floating mt-4" style={{textAlign : 'left'}}>
        {/* <label style={{marginLeft:"5px"}} htmlFor="email"><b>Email</b></label> */}
        <input placeholder="Email" onChange={(e) => setEmail(e.target.value)} type="email" className="form-control" id="email" name="email"/>
        </div>


        <div className="form-floating mt-4" style={{textAlign : 'left'}}>
        {/* <label style={{marginLeft:"5px"}} htmlFor="email"><b>Şifre</b></label> */}
        <input placeholder="Şifre" onChange={(e) => setPassword(e.target.value)} type="password" className="form-control" id="password" name="password"/>
        </div>


        <div className="checkbox mb-2 mt-3" style={{marginRight:"170px"}}>
      <label>
        <input onChange={(e) => setrememberMe(e.target.value)} type="checkbox" value="remember-me" id="rememberMe" name="rememberMe"/> Remember me
      </label>
    </div>

    <button onClick={makeLoginRequest} className="w-100 btn btn-lg btn-primary" type="submit">Giriş Yap</button>
    <Toaster/>
  
</main>
    )
}

export default LoginComponent
