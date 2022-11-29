<script setup>
  import { CloseModal } from '../assets/Functions/Recipe';
  import { AddToFavorites } from '../assets/Functions/User';
  import {imageConvertUrl} from '../assets/Functions/services/BaseUrls';
</script>


<template>
    <div id="hidden" style="display:none" class="meal-details">
        <button v-on:click="CloseRecipe()" class="recipe-close-btn"><i class="fas fa-times"></i></button> 
        <button v-on:click="AddRecipeToFavorites()" id="favorite" class="recipe-favorite-btn"><i class="fas fa-heart"></i></button> 
        <br/>                  
            <div class="meal-details-content" v-for="meal in itemdata">                      
                <h2 class="recipe-title">{{meal.title}}</h2>

                    <div class="recipe-meal-img">
                      <img :src="imageConvertUrl + meal.imageBase64" alt="food">
                    </div>

                        <p class="recipe-text-name">{{meal.userName}}</p>

                        <p class="recipe-category">Result</p>

                        <p class="recipe-text">Preptime: {{meal.prepTime}} minute('s'), Portion('s'): {{meal.portions}}</p><br/>

                            <h3><p class="recipe-category">Ingredients</p></h3>
                          <p class="recipe-text">{{meal.ingredients}}</p>
   
                        <h3><p class="recipe-category">Preperation</p></h3>
                      <p class="recipe-text">{{meal.description}}</p>
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
        CloseModal()
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
    @import "../assets/styles/recipeitem.css";
</style>