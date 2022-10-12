import APIcalls from "./services/APIcalls";

//functie om alle recepten asynchroon in te laden.
export const GetAllRecipes = async function(){

        await APIcalls.GetAllRecipes()
        .then(response => {this.meallist = response.data;})
        .catch(error => {console.log(error)})

        console.log(this.meallist)
        };
    
            

    

