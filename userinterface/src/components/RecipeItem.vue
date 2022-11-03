<script setup>
  import { CloseModal } from '../assets/Functions/Recipe';
  import { AddToFavorites } from '../assets/Functions/User';
  import {recipeImageURL} from '../assets/Functions/services/ImageUrls';
</script>


<template>
    <div id="hidden" style="display:none" class="meal-details">
        <button v-on:click="CloseRecipe()" type="button" class="btn recipe-close-btn"><i class="fas fa-times"></i></button> 
        <button v-on:click="AddRecipeToFavorites()" id="favorite" class="btn recipe-favorite-btn"><i class="fas fa-heart"></i></button> 
        <br/>                  
            <div class="meal-details-content" v-for="meal in itemdata">                      
                <h2 class="recipe-title">{{meal.title}}</h2>
                    <div class="recipe-meal-img">
                      <img :src="recipeImageURL + meal.imageName" alt="food">
                    </div>
                        <p>Made by: {{meal.userName}}</p>
                        <p>{{meal.rating}} out of 5 <span class="fa fa-star"></span></p>
                        <br>
                        <p class="recipe-category">Result</p>
                        <br/>
                        <p1>Preptime: {{meal.prepTime}} minute('s'), Portion('s'): {{meal.portions}}</p1>
                        <br/><br/>
                        <div class="recipe-instruct">
                            <br/>
                            <h3><p class="recipe-category">Ingredients</p></h3>
                            <p>{{meal.ingredients}}</p>
                        </div>
                        <br/>
                        <div class="recipe-instruct">
                            <h3><p class="recipe-category">Preperation</p></h3>
                            <p>{{meal.description}}</p>
                    </div>
            </div>
    </div>
</template>

<script>
  export default {
    name: "recipeitem",
    data(){
      return{
        user: ''
      }
    },
    props: [
        'itemdata',
        'recipeId'
    ],
    mounted(){
      this.user = JSON.parse(localStorage.getItem("user"))
    },
    methods:{
      CloseRecipe(){
        this.CloseModal()
      },
      async AddRecipeToFavorites(){
        if(await AddToFavorites(this.user, this.recipeId)){
          this.$toast.success('recipe added to favorites' , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 4500
          });
        }
        else{
          this.$toast.error('recipe removed from favorites' , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 4500
          });
        } 
      }
  }
}
</script>


<style scoped>
    @import "../assets/styles/recipes.css";
</style>