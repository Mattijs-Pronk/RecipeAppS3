import APIcalls from "./services/APIcalls";

//functie om het wachtwoord van een user te veranderen.
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


//functie om het profiel van een user te veranderen.
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

//functie om een user op te halen.
export const GetUserById = async (userid) => {
    try{
        let response = await APIcalls.GetUserById(userid)
        return response.data;
    }
    catch(error){
        console.log(error)
    }
}

//functie om alle users op te halen.
export const GetAllUsers = async () => {
    try{
        let response = await APIcalls.GetAllUsers()
        return response.data;
    }
    catch(error){
        console.log(error)
    }
}

//functie om te checken of een username al in gebruik is.
export const DoubleUsernameExcludeCurrentUserName = async (currentusername, newusername) => {
    let response = await APIcalls.DoubleUsernameExcludeCurrentUserName(currentusername, newusername)
    return response.data
}

//functie om te checken of een email al in gebruik is.
export const DoubleEmailExcludeCurrentEmail = async (currentemail, newemail) => {
    let response = await APIcalls.DoubleEmailExcludeCurrentEmail(currentemail, newemail)
    return response.data
}