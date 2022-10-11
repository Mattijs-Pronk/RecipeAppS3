import APIcalls from "./services/APIcalls";

//functie om alle recepten asynchroon in te laden.
export const LoadRecipes = async function(){

        await APIcalls.GetAllRecipes()
        .then(response => {this.meallist = response.data;})
        .catch(error => {console.log(error)})

        };
    
            

    

