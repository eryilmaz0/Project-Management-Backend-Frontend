export const UserIsAuthenticated = () => {

    let authenticatedUser = null;

    authenticatedUser = GetAuthenticatedUserInfo();

    return authenticatedUser != null ? true : false;

}



export const GetAuthenticatedUserInfo = () => {

    var authenticatedUser = null;

    authenticatedUser = JSON.parse(localStorage.getItem("auth_token"));

    if(authenticatedUser == null){

        authenticatedUser = JSON.parse(sessionStorage.getItem("auth_token"));
    }

    return authenticatedUser;
}


export const UserIsLeader = () => {

    if(UserIsAuthenticated() === false)
        return false;
        
    var authenticatedUser = GetAuthenticatedUserInfo();
    var sonuc = authenticatedUser.userRoles.some(function (role){
 
        return role === "Leader";
     
    });

    return sonuc;
}