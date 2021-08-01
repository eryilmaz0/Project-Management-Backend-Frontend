import React from 'react'
import '../Components/ListMembersStyles.css';
import {useState, useEffect} from 'react';
import {GetUsersOutOfProject,AddUserToProject} from '../Requests/Requests';
import toast, { Toaster } from 'react-hot-toast';

function ListMembersOutOfProjectComponent(props) {

    const {projectId} = props;
    const [members, setMembers] = useState([])
    const [membersUpdated, setMembersUpdated] = useState(false)


    useEffect(() => {
        GetUsersOutOfProject(projectId).then(response => {

            setMembers(response.data.data);
        }).catch(error => {

            toast.error(error.response.data.message, {duration:2000})
        })

        setMembersUpdated(false);
    }, [projectId, membersUpdated])




    const successToast = () => toast.success('Kullanıcı Projeye Eklendi.');


    const AddUserToProjectRequest = (userId) => {

        AddUserToProject(userId, projectId).then(response => {

            successToast();
            setMembersUpdated(true);
        }).catch(error => {

            toast.error(error.response.data.message, {duration:2000})
        })
    }

    return (
        
        <div className=" scrollable bg-light p-4 rounded offset-md-2 col-md-8 border border-info mt-5 mb-5">
        <h4>Üye Olmayan Kullanıcılar</h4>
        <div className="col-md-6 offset-md-3">
         <table className="mt-4 table table-hover">
                    <thead className="text-center">
                        <th>Ad Soyad</th>
                        <th>İşlem</th>
                    </thead>
                    <tbody className="text-center">
                        {members.map(member => {
                            return <tr key={member.id}>
                                <td key={member.id}>
                                <img className="mr-2 mb-1" alt="" src={'http://localhost:3000/img/profilepictures/'+member.picture} />
                                {member.name} {member.lastName}
                                </td>
                                <td>
                                    <button onClick={() => AddUserToProjectRequest(member.id)} className="btn btn-sm btn-outline-info">Projeye Ekle</button>
                                </td>
                            </tr>
                        })}
                    </tbody>
                </table>
                <Toaster/>
                </div>
       
    </div>
    
    
    )
}

export default ListMembersOutOfProjectComponent
