import APIcalls from "./services/APIcalls";

//functie recipes van de user in te laden.
export const GetUserRecipesById = async (id) => {
    try{
        let response = await APIcalls.GetUserRecipesById(id)
        return response.data;
    }
    catch(error){
        console.log(error)
    }
};

//functie om een favorite toe te voegen of te removen.
export const AddToFavorites = async (userid, recipeid) => {
    var favoriteHeart = document.getElementById("favorite");

    try{
        await APIcalls.AddToFavorites(userid, recipeid)

        //kleur van heart veranderen.
        if(favoriteHeart.style.color == "red"){
            favoriteHeart.style.color = "#002366"
        }
        else{
            favoriteHeart.style.color = "red"
        }
        return true
    }
    catch(error){
        console.log(error)

        //kleur van heart veranderen.
        if(favoriteHeart.style.color == "red"){
            favoriteHeart.style.color = "#002366"
        }
        else{
            favoriteHeart.style.color = "red"
        }

        return false
    }
}

//functie om alle favorieten van een user op te halen.
export const GetAllFavoritesById = async (id) => {
    try{
        let response = await APIcalls.GetAllFavoritesById(id)
        return response.data;
    }
    catch(error){
        console.log(error)
    }
}

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

//functie om het aantal gecreerde recepten van een user op te halen.
export const GetUserRecipesAmountById = async (userid) => {
    try{
        let response = await APIcalls.GetUserRecipesAmountById(userid)
        return response.data;
    }
    catch(error){
        console.log(error)
    }
}

//functie om het aantal favorieten recepten van een user op te halen.
export const GetUserFavoritesAmountById = async (userid) => {
    try{
        let response = await APIcalls.GetUserFavoritesAmountById(userid)
        return response.data;
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

//functie om een email naar het bedrijf toe te sturen.
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

//functie om te checken of recept een favoriet is van de user.
export const GetFavorite = async (userid, recipeid) => {
    try{
        let response = await APIcalls.GetFavorite(userid, recipeid)
        if(response.data){
            document.getElementById("favorite").style.color  = "red"
        }
        else{
            document.getElementById("favorite").style.color  = "#002366"
        }
      }
      catch(error){
        console.log(error)
      }
}