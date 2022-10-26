<script setup>
  import { CloseModal } from '../assets/Functions/Recipe';
</script>


<template>
    <div id="hidden" style="display:none" class="meal-details">
        <button v-on:click="CloseRecipe()" type="button" class="btn recipe-close-btn"><i class="fas fa-times"></i></button> 
        <button v-on:click="" class="btn recipe-favorite-btn"><img src="/Images/HeartFavorite.jpg" alt="Foto" class="favoritephoto"></button>                   
            <div class="meal-details-content" v-for="meal in itemdata">                      
                <h2 class="recipe-title">{{meal.title}}</h2>
                    <div class="recipe-meal-img">
                      <img src="/Images/Hamburger2.jpg" alt="food">
                    </div>
                        <p>{{meal.rating}} out of 5 <span class="fa fa-star"></span></P>
                        <p>Made by: {{meal.userName}}</p>
                        <br>
                        <p class="recipe-category">Result:</p> <p1>Preptime: {{meal.prepTime}} mins, Portion('s'): {{meal.portions}}</p1>
                        <div class="recipe-instruct">
                            <br>
                            <h3>Ingredients:</h3>
                            <p>{{meal.ingredients}}</p>
                        </div>
                        <div class="recipe-instruct">
                            <h3>Preperation:</h3>
                            <p>{{meal.description}}</p>
                    </div>
            </div>
    </div>
</template>

<script>
  export default {
    name: "recipeitem",
    props: [
        'itemdata'
    ],
    methods:{
      CloseRecipe(){
        this.CloseModal()
      }
  }
}
</script>


<style scoped>
    @import "../assets/styles/homepage.css";
</style>