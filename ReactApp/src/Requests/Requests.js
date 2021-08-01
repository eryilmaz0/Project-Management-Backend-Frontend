import axios from 'axios'


const baseUri = "https://localhost:44383/api/";


export const LoginRequest = (loginRequestBody) => {

    return axios.post(baseUri+"Auth/Login", loginRequestBody);
}



export const GetDepartmentsRequest = () => {

    return axios.get(baseUri+"Departments");
}


export const GetDepartmentDetail = (departmentId) => {

    return axios.get(baseUri+"Departments/"+departmentId);
}


export const GetProjectsByDepartment = (departmentId) => {

    return axios.get(baseUri + "Projects/"+departmentId , {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}



export const GetProjectWithDetail = (projectId) => {

    return axios.get(baseUri + "Projects/Detail/"+projectId , {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}


export const GetTasksByProject = (projectId) => {

    return axios.get(baseUri + "Tasks/Project/"+projectId , {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}


export const GetProjectMembers = (projectId) => {

    return axios.get(baseUri + "Projects/"+projectId+"/Members" , {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}


export const GetUsersOutOfProject = (projectId) => {

    return axios.get(baseUri + "Projects/"+projectId+"/OutOfMembers" , {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}



export const RemoveUserFromProject = (userId, projectId) => {

    return axios.delete(baseUri + "Projects/RemoveUser?userId="+userId+"&projectId="+projectId , {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });

}



export const AddUserToProject = (userId, projectId) => {

    return axios.post((baseUri + "Projects/AddUser?userId="+userId+"&projectId="+projectId), null, {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });

}


export const CreateProject = (createProjectBody) => {

    return axios.post(baseUri + "Projects" , createProjectBody, {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}


export const CreateTask = (createTaskBody) => {

    return axios.post((baseUri + "Tasks"), createTaskBody, {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}



export const UpdateTask = (updateTaskBody) => {

    return axios.put((baseUri + "Tasks"), updateTaskBody, {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}


export const GetTaskChanges = (taskId) => {

    return axios.get(baseUri + "Tasks/"+taskId+"/Changes" , {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}


export const GetTaskById = (taskId) => {

    return axios.get(baseUri + "Tasks/"+taskId , {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}


export const EditProject = (editProjectBody) => {

    return axios.put((baseUri + "Projects"), editProjectBody, {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}



export const DeactiveProject = (projectId) => {

    return axios.get(baseUri + "Projects/"+projectId +"/Deactivate" , {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}



export const ActivateProject = (projectId) => {

    return axios.get(baseUri + "Projects/"+projectId +"/Activate" , {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}


export const GetAllProjects = () => {

    return axios.get(baseUri + "Projects/" , {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}


export const GetFilteredProjects = (filter) => {

    return axios.get(baseUri + "Projects/Filter/" +filter , {
        headers: {'Authorization': 'Bearer ' +GetAuthenticatedUserInfo().token }
    });
}


const GetAuthenticatedUserInfo = () => {
    
    var authenticatedUser = null;

    authenticatedUser = JSON.parse(localStorage.getItem("auth_token"));

    if(authenticatedUser == null){

        authenticatedUser = JSON.parse(sessionStorage.getItem("auth_token"));
    }

    return authenticatedUser;
}
