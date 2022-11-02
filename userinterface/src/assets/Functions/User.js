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

//functie om een favorite toe te voegen of te removen.
export const AddToFavorites = async (userid, recipeid) => {
    var favoriteHeart = document.getElementById("favorite");

    try{
        await APIcalls.AddToFavorites(userid, recipeid)

        //kleur van heart veranderen.
        if(favoriteHeart.style.color == "red"){
            favoriteHeart.style.color = "#219ebc"
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
            favoriteHeart.style.color = "#219ebc"
        }
        else{
            favoriteHeart.style.color = "red"
        }

        return false
    }
}

//functie om alle favorieten van een user op te halen.
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
export const GetUserById = async function(userid) {
    try{
        return await APIcalls.GetUserById(userid)
        .then(response => {this.user = response.data;})
    }
    catch(error){
        console.log(error)
    }
}

//functie om het aantal gecreerde recepten van een user op te halen.
export const GetUserRecipesAmountById = async function(userid) {
    try{
        return await APIcalls.GetUserRecipesAmountById(userid)
        .then(response => {this.userrecipes = response.data;})
    }
    catch(error){
        console.log(error)
    }
}

//functie om het aantal favorieten recepten van een user op te halen.
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
            document.getElementById("favorite").style.color  = "#219ebc"
        }
      }
      catch(error){
        console.log(error)
      }
}