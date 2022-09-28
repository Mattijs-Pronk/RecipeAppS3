const mealDetailsContent = document.querySelector('.meal-details-content');
const recipeCloseBtn = document.getElementById('recipe-close-btn');


recipeCloseBtn.addEventListener('click', () => {
    mealDetailsContent.parentElement.classList.remove('showRecipe');
});


function mealRecipeModal(meal){
    console.log(meal);
    meal = meal[0];
    let html = `
    <h2 class="recipe-title">Hamburger</h2>
    <span class="fa fa-star checked"></span>
      <span class="fa fa-star checked"></span>
      <span class="fa fa-star checked"></span>
      <span class="fa fa-star"></span>
      <span class="fa fa-star"></span> 

    <div class="recipe-meal-img">
        <br>
        <img src="/Images/Hamburger2.jpg" alt="food">
    </div>
    <br>
    <p class="recipe-category">Made by:</p> <p1>Mattijs Pronk</p1>
    <br> <br>
      <p class="recipe-category">Result:</p> <p1>Preptime: 20 mins, Portion('s') 1</p1>

    <div class="recipe-instruct">
        <br>
        <h3>Ingredients:</h3>
        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Molestias assumenda quae corrupti temporibus rem blanditiis fugit numquam expedita maiores ratione praesentium.</p>
    </div>

    <div class="recipe-instruct">
        <h3>Preperation:</h3>
        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Molestias assumenda quae corrupti temporibus rem blanditiis fugit numquam expedita maiores ratione praesentium, qui ab harum dicta modi nemo, est enim autem? Lorem ipsum, dolor sit amet consectetur adipisicing elit. Vel quia optio odio, fuga laudantium porro nam magni deleniti accusantium, unde veniam alias perferendis incidunt tempora. Ea, quos repellat. Quos, laudantium.</p>
    </div>
    `;
    mealDetailsContent.innerHTML = html;
    mealDetailsContent.parentElement.classList.add('showRecipe');
}
