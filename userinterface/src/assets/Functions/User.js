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