<script setup>
  import { CloseModal } from '../assets/Functions/CloseModal';
  import { OpenModal } from '../assets/Functions/OpenModal'
  import { LoadRecipes } from '../assets/Functions/LoadRecipes';
</script>

<template>

<head>
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" integrity="sha512-+4zCK9k+qNFUR5X+cKL9EIR+ZOhtIloNl9GIKS57V1MyNsYpYcUrUeQc9vNfzsWfV28IaLL3i96P9sdNyeRssA==" crossorigin="anonymous" />
</head>

    <div class = "container">
          <div class = "meal-wrapper">    
            <div class = "meal-search">
              
              <!-- Start van searchbar -->
              <div class = "meal-search-box">
                <input type = "text" class = "search-control" placeholder="Enter an ingredient" id = "search-input">
                <button type = "submit" class = "search-btn btn" id = "search-btn">
                  <i class = "fas fa-search"></i>
                </button>
              </div>
            </div>
            <!-- Einde van searchbar -->

            <!-- Start van meal items -->
            <div class = "meal-result">
              <h2></h2>
              <div id= "meal">      
                <div class = "meal-item" v-for="meal in meallist">
                    <div class = "meal-img">
                      <img src = "/Images/Hamburger2.jpg" alt = "food">
                    </div>
                    <div class = "meal-name">
                      <h3>{{meal.title}}</h3>
                      <p>Made by: Mattijs Pronk</p>          
                      <p>{{meal.rating}} out of 5 <span class="fa fa-star checked"></span></P>
                      <button v-on:click="OpenRecipeById(meal.recipeId)" class = "recipe-btn">See Recipe</button>
                    </div>
                  </div>
            </div>
          </div>
          <!-- Eind van meal items -->

          <!-- Start van modal -->
            <div id="hidden" style="display:none" class="meal-details">
                    <button v-on:click="CloseRecipe()" type="button" class="btn recipe-close-btn"><i class="fas fa-times"></i></button> 
                    <button v-on:click="" class="btn recipe-favorite-btn"><img src="/Images/HeartFavorite.jpg" alt="Foto" class="favoritephoto"></button>                   
                    <div class="meal-details-content">                      
                      <h2 class="recipe-title">{{mealitem.title}}</h2>
                      <div class="recipe-meal-img">
                      <img src="/Images/Hamburger2.jpg" alt="food">
                      </div>
                      <p>{{mealitem.rating}} out of 5 <span class="fa fa-star"></span></P>
                      <p>Made by: Mattijs Pronk</p>
                      <br>
                      <p class="recipe-category">Result:</p> <p1>Preptime: {{mealitem.prepTime}} mins, Portion('s'): {{mealitem.portions}}</p1>
                      <div class="recipe-instruct">
                      <br>
                      <h3>Ingredients:</h3>
                      <p>{{mealitem.ingredients}}</p>
                      </div>
                      <div class="recipe-instruct">
                      <h3>Preperation:</h3>
                      <p>{{mealitem.description}}</p>
                  </div>
              </div>
              <!-- Eind van modal -->

            </div>
          </div>
      </div>
</template>

<script>
  export default {
    name: "Homepage",
    data(){
      return{
        meallist: [],
        mealitem: []
      }
    },
    components:{

    },
    //mounted zorgt ervoor dat de functie wordt ingeladen bij het laden van de pagina.
    mounted(){
      this.LoadRecipes()
    },
    methods:{
      OpenRecipeById(id){
        this.OpenModal(id)
      },
      CloseRecipe(){
        this.CloseModal()
      }
    }
  }
</script>


<style>
    @import "../assets/styles/homepage.css";
</style>