import axios from "axios";

//functie om het modal te openen.
export const openModal = async function(id){
  const mealDetailsContent = document.querySelector('.meal-details-content');
      
  axios.get(`https://localhost:7108/api/Recipe/id?id=${id}`)
       .then(function(data){

        var meal = data.data;
             let html = `
              <h2 class="recipe-title">${meal.title}</h2>

              <div class="recipe-meal-img">
              <img src="/Images/Hamburger2.jpg" alt="food">
              </div>

              <p>${meal.rating} out of 5 <span class="fa fa-star"></span></P>

              

              <p>Made by: Mattijs Pronk</p>
              <br>
              <p class="recipe-category">Result:</p> <p1>Preptime: ${meal.prepTime} mins, Portion('s') ${data.Portions}</p1>

              <div class="recipe-instruct">
              <br>
              <h3>Ingredients:</h3>
              <p>${meal.ingredients}</p>
              </div>

              <div class="recipe-instruct">
              <h3>Preperation:</h3>
              <p>${meal.description}</p>
              </div>
              `;

              document.getElementById("hidden").style.display = "block";

          mealDetailsContent.innerHTML = html;
      });
    }