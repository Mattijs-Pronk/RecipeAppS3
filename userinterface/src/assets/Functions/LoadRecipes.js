import { openModal } from "./OpenModal";
import axios from "axios";

//functie om alle recepten asynchroon in te laden.
export const LoadRecipes = async function(){
    const mealList = document.getElementById('meal');
        axios.get(`https://localhost:7108/api/Recipe`)
        .then(response => {this.data = response.data;

                let html = "";
                this.data.forEach((meal) => {
                    html += `
                    <div class = "meal-item">
                    <div class = "meal-img">
                      <img src = "/Images/Hamburger2.jpg" alt = "food">
                    </div>
                    <div class = "meal-name">
                      <h3>${meal.title}</h3>
                      <p>Made by: Mattijs Pronk</p>
                      
                      <p>${meal.rating} out of 5 <span class="fa fa-star checked"></span></P>

                      <button id="${meal.recipeId}" class = "recipe-btn">See Recipe</button>
                    </div>
                  </div>
                    `;
                });

                mealList.innerHTML = html;   
            
            //functie aan de button geven in de class "Meal-item".
            const buttons = document.getElementsByClassName("meal-item")
            
            //id van de button ophalen en openModal uitvoeren.
            const pressed = e =>{
                var id = e.target.id
                openModal(id)
            }

            //funcite aan knoppen geven.
            for(let button of buttons){
                button.addEventListener("click", pressed)
            }
        })
    }
    
            

    

