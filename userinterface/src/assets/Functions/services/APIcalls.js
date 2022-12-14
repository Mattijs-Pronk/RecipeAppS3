import API from "./API"

export default{
    //Start van recipe Calls.
    async GetAllAcceptedRecipes(){
        return await API().get('/Recipe/getallaccepted')
    },

    async GetRecipeById(id){
        return await API().get(`/Recipe/getrecipe?id=${id}`) //, {
        //    id: id
        //})
    },

    async AddRecipe(fd){
        return await API().post(`/Recipe/create` , fd)
    },
    //Eind van recipe calls.



    //Start van authenticator calls.
    async Login(email, password){
        return await API().post(`/Auth/login`, {
            email: email,
            password: password
        })
    },

    async Register(username, password, email){
        return await API().post(`/Auth/register`, {
            userName: username,
            email: email,
            password: password,
        })
    },

    async ForgotPassword(email){
        return await API().post(`/Auth/forgotpassword?email=${email}`) //,{
        // email: email
        // })
    },

    async ResetPassword(passwordresettoken, password){
        return await API().post(`/Auth/resetpassword` ,{
            passwordResetToken: passwordresettoken,
            password: password
        })
    },

    async VerifyAccount(email, activateaccounttoken){
        return await API().post(`/Auth/verify` ,{
            email: email,
            activateAccountToken: activateaccounttoken
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



   //Start van user calls.
   async GetUserRecipesById(id){
    return await API().get(`/User/getallmyrecipes?id=${id}`) //, {
    //    id: id
    //})
    },

    async AddToFavorites(userid, recipeid){
        return await API().post(`/User/addfavorite` , {
            userId: userid,
            recipeId: recipeid
        })
    },

    async GetAllFavoritesById(id){
        return await API().get(`/User/getallmyfavorites?id=${id}`) //, {
        //    id: id
        //})
    },

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

    async GetUserRecipesAmountById(userid){
        return await API().get(`/User/recipesint?id=${userid}`) //, {
        //    id: id
        //})
        },

    async GetUserFavoritesAmountById(userid){
        return await API().get(`/User/favoritesint?id=${userid}`) //, {
        //    id: id
        //})
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

    async ContactUs(name, email, subject, body){
        return await API().post(`/User/contactus` , {
                name: name,
                email: email,
                subject: subject,
                body: body
            })
        },

    async GetFavorite(userid, recipeid){
        return await API().post(`/User/getfavorite` , {
                userId: userid,
                recipeId: recipeid
            })
        },
            
    //Eind van user calls.
}