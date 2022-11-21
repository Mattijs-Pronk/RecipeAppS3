import API from "./API"

export default{
    //Start van recipe Calls.
    async GetAllAcceptedRecipes(){
        return await API().get('/Recipe/getallaccepted')
    },

    async GetAllOnHoldRecipes(){
        return await API().get('/Recipe/getallonhold')
    },

    async GetAllDeclinedRecipes(){
        return await API().get('/Recipe/getalldeclined')
    },

    async GetRecipeById(id){
        return await API().get(`/Recipe/getrecipe?id=${id}`) //, {
        //    id: id
        //})
    },
    //Eind van recipe calls.



    //Start van authenticator calls.
    async Login(email, password){
        return await API().post(`/Auth/login`, {
            email: email,
            password: password
        })
    },

    async CheckUsername(username){
        return await API().post(`/Auth/checkusername?username=${username}`) //, {
        //     params: {
        //         username: username
        //     }
        // })
    },
   //Eind van authenticator calls.


   //Start van admin calls.
    async AcceptRecipeRequest(id){
        return await API().put(`/Admin/acceptrecipe?id=${id}`)
    },

    async EditRecipeRequest(recipeid, title, preptime, portions, ingredients, description){
        return await API().put(`/Admin/editrecipe`, {
            recipeid: recipeid,
            title: title,
            preptime: preptime,
            portions: portions,
            ingredients: ingredients,
            description: description
        })
    },

    async DeclineRecipeRequest(id){
        return await API().put(`/Admin/declinerecipe?id=${id}`)
    },

    async DeleteUserById(id){
        return await API().post(`/Admin/deleteuser?id=${id}`)
    },
   //Eind van Admin calls.


   //Start van user calls.
    async ChangePassword(userid, currentpass, newpass){
        return await API().put(`/User/changepassword` , {
            userId: userid,
            currentPassword: currentpass,
            newPassword: newpass
        })
    },

    async GetUserById(userid){
        return await API().get(`/User/getuser?id=${userid}`) //, {
        //    id: id
        //})
    },

    async GetAllUsers(){
        return await API().get(`/User/getallusers`)
    },

    async DoubleUsernameExcludeCurrentUserName(currentusername, newusername){
        return await API().post(`/User/doubleusername` , {
            currentUsername: currentusername,
            newUsername: newusername
        })
    },

    async ChangeProfile(userid, username, adress, phone){
    return await API().put(`/User/changeprofile` , {
            userId: userid,
            userName: username,
            adress: adress,
            phone: phone
        })
    },        
    //Eind van user calls.
}