import APIcalls from '../Functions/services/APIcalls';

//functie om recept te accepteren.
export const AcceptRecipeRequest = async (recipeid) => {
    try{
        await APIcalls.AcceptRecipeRequest(recipeid)
        return true;
    }
    catch(error){
        console.log(error)
        return false;
    }
}

//functie om recept te declinen.
export const DeclineRecipeRequest = async (recipeid) => {
    try{
        await APIcalls.DeclineRecipeRequest(recipeid)
        return true;
    }
    catch(error){
        console.log(error)
        return false;
    }
}

//functie om een recept op te halen.
export const GetRecipeById = async (id) => {
    try{
        let response = await APIcalls.GetRecipeById(id)
        return response.data;
    }
    catch(error){
        console.log(error)
    }
}

//functie om een recept toe te voegen.
export const EditRecipeRequest = async (recipeid, title, preptime, portions, ingredients, description) => {
    try{
        await APIcalls.EditRecipeRequest(recipeid, title, preptime, portions, ingredients, description)
        return true
    }
    catch(error){
        console.log(error)
        return false
    }
}

//functie om een recept toe te voegen.
export const DeleteUserById = async (userid) => {
    try{
        await APIcalls.DeleteUserById(userid)
        return true
    }
    catch(error){
        console.log(error)
        return false
    }
}

//functie om een user aan te passen.
export const EditUser = async (userid, username, email, adress, phone, password, isadmin) => {
    try{
        await APIcalls.EditUser(userid, username, email, adress, phone, password, isadmin)
        return true
    }
    catch(error){
        console.log(error)
        return false
    }
}