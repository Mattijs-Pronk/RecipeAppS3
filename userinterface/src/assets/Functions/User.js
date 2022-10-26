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