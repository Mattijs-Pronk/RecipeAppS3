export const openModal = function(){
    document.getElementById("hidden").style.display = "block";
    
    console.log("Clicked");

    const mealDetailsContent = document.querySelector('.meal-details-content');

    let html = `
    <h2 class="recipe-title">Hamburger</h2>
    <span class="fa fa-star checked"></span>
      <span class="fa fa-star checked"></span>
      <span class="fa fa-star checked"></span>
      <span class="fa fa-star"></span>
      <span class="fa fa-star"></span> 
      <br>
      <p class="recipe-category">Made by:</p> <p1>Mattijs Pronk</p1>

    <div class="recipe-meal-img">
        <br>
        <img src="/Images/Hamburger2.jpg" alt="food">
    </div>

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
}


export const CloseModal = function(){
    document.getElementById("hidden").style.display = "none";
}


export const LoadRecipes = function(){
    const mealList = document.getElementById('meal');
        fetch(`https://localhost:7108/api/Recipe`)
        .then(response => response.json())
        .then(data => {

            
            let html = "";
            if(data){
                data.forEach(meal => {
                    html += `
                    <div class = "meal-item">
                    <div class = "meal-img">
                      <img src = "/Images/Hamburger2.jpg" alt = "food">
                    </div>
                    <div class = "meal-name">
                      <h3>${meal.title}</h3>
                      <p>Made by: Mattijs Pronk</p>
                      
                      <p>${meal.rating} out of 5 <span class="fa fa-star checked"></span></P>

                      <button class = "recipe-btn">See Recipe</button>
                    </div>
                  </div>
                    `;
                });
            }
    
            mealList.innerHTML = html;

            var btns = document.getElementsByClassName("recipe-btn");
            for (var i = 0; i < btns.length; i++) {
                btns[i].addEventListener("click", function () {
                    openModal()
                });
            }
        }); 
    }

    

