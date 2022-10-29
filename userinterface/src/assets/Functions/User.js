import APIcalls from "./services/APIcalls";

//functie recipes van de user in te laden.
export const GetUserRecipesById = async function(id){
    try{
        await APIcalls.GetUserRecipesById(id)
        .then(response => {this.meallist = response.data;})
    }
    catch(error){
        console.log(error)
    }
};

export const AddToFavorites = async (userid, recipeid) => {
    try{
        await APIcalls.AddToFavorites(userid, recipeid)
        return true
    }
    catch(error){
        console.log(error)
        return false
    }
}

export const GetAllFavoritesById = async function(id){
    try{
        await APIcalls.GetAllFavoritesById(id)
        .then(response => {this.meallist = response.data;})
        return true
    }
    catch(error){
        console.log(error)
        return false
    }
}

export const ChangePassword = async (userid, currentpass, newpass) => {
    try{
        await APIcalls.ChangePassword(userid, currentpass, newpass)
        return true
    }
    catch(error){
        console.log(error)
        return false
    }
}

export const ChangeProfile = async (userid, username, adress, phone) => {
    try{
        await APIcalls.ChangeProfile(userid, username, adress, phone)
        return true
    }
    catch(error){
        console.log(error)
        return false
    }
}

export const GetUserById = async function(userid) {
    try{
        return await APIcalls.GetUserById(userid)
        .then(response => {this.user = response.data;})
    }
    catch(error){
        console.log(error)
    }
}

export const GetUserRecipesAmountById = async function(userid) {
    try{
        return await APIcalls.GetUserRecipesAmountById(userid)
        .then(response => {this.userrecipes = response.data;})
    }
    catch(error){
        console.log(error)
    }
}

export const GetUserFavoritesAmountById = async function(userid) {
    try{
        return await APIcalls.GetUserFavoritesAmountById(userid)
        .then(response => {this.userfavorites = response.data;})
    }
    catch(error){
        console.log(error)
    }
}

//functie om te checken of een username al in gebruik is.
export const UsernameCheckerChangeUsername = async (currentusername, newusername) => {
    let response = await APIcalls.UsernameCheckerChangeUsername(currentusername, newusername)
    return response.data
}

export const ContactUs = async (name, email, subject, body) => {
    try{
        await APIcalls.ContactUs(name, email, subject, body)
        return true
    }
    catch(error){
        console.log(error)
        return false
    }
}