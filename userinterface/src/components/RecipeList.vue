<script setup>
  import { OpenModal } from '../assets/Functions/Recipe';
  import { GetFavorite } from '../assets/Functions/User';
  import RecipeItem from '../components/RecipeItem.vue';
  import {recipeImageURL} from '../assets/Functions/services/ImageUrls';
</script>


<template>
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css">

<section class="dishes" id="dishes">

<h3 class="sub-heading"> All recipes </h3>
<h1 class="heading"> Find your taste </h1>

<div class="box-container">

    <div class="box" v-for="meal in listdata">
        <img :src="recipeImageURL + meal.imageName" alt="food">

        <h3>{{meal.title}}</h3>

        <span>{{meal.userName}}</span>

        <div class="stars">
          <span>{{meal.rating}} out of 5 <i class="fas fa-star"></i></span>
        </div>
        
        <a v-on:click="OpenRecipeById(meal.recipeId)" class="btn">See Recipe</a>
        
        <div class="status">{{meal.status}}</div>  
    </div>

</div>
</section>

    <!-- Start mealDetials -->
      <RecipeItem :itemdata="mealitem" :recipeId="recipeid"/>
    <!-- Eind mealDetails -->

</template>

<script>
  export default {
    name: "recipelist",
    components: {
      RecipeItem
    },
    props: [
        'listdata'
    ],
    data(){
        return{
            mealitem: [],
            recipeid: ''
        }
    }, 
    methods:{
      OpenRecipeById(id){
        this.OpenModal(id)
        this.recipeid = id;

        var user = JSON.parse(localStorage.getItem("user"))  
        
        GetFavorite(user, id)
      },
    }
}
</script>


<style scoped>
    @import "../assets/styles/recipelist.css";
</style>