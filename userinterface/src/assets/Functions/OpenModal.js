import APIcalls from "./services/APIcalls";

//functie om het modal te openen.
export const OpenModal = async function(id){
      
  await APIcalls.GetRecipeById(id)
       .then(response => {this.mealitem = response.data;})
       .catch(error => {console.log(error)})

        document.getElementById("hidden").style.display = "block";

        console.log(this.mealitem)
        
      };