import React from 'react'
import {useEffect} from 'react'
import {GetAuthenticatedUserInfo} from '../Services/AuthService';

function TaskTableRowComponent(props) {

    const {tasks} = props;

    return (
       <div>
           {tasks ? 

                <table className="mb-3 table rounded table-hover mt-1">  
                <thead className="text-center table-primary ">
                        <th scope="col">Atanan Kişi</th>
                        <th scope="col">Açıklama</th>
                        <th scope="col">Tipi</th>
                        <th scope="col">Öncelik</th>
                        <th scope="col">Durum</th>
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
         
                         <td><button className="btn btn-sm btn-outline-info">Düzenle</button></td>
                    </tr>
                    })}
                </tbody>
                </table>
            : null}
       </div>
    )
}

export default TaskTableRowComponent
