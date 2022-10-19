import APIcalls from "./services/APIcalls";

//functie om alle recepten asynchroon in te laden.
export const GetAllRecipes = async function(){

        await APIcalls.GetAllRecipes()
        .then(response => {this.meallist = response.data;})
        .catch(error => {console.log(error)})

        };

//functie om het modal te openen.
export const OpenModal = async function(id){
      
        await APIcalls.GetRecipeById(id)
         .then(response => {this.mealitem = response.data;})
         .catch(error => {console.log(error)})
  
          document.getElementById("hidden").style.display = "block";
          
        };

//functie om modal te sluiten
export const CloseModal = function(){
         document.getElementById("hidden").style.display = "none";
}