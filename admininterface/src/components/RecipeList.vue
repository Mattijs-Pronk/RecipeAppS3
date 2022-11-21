<script setup>
  import { OpenModal } from '../assets/Functions/Recipe';
  import RecipeItem from '../components/RecipeItem.vue';
  import {imageConvertUrl} from '../assets/Functions/services/ImageUrls';
</script>


<template>
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css">

<section class="dishes" id="dishes">

<div class="box-container">

    <div class="box" v-for="meal in listdata">
      <div class="status">{{meal.status}}</div>  
      
      <img :src="imageConvertUrl + meal.imageBase64" alt="food">

        <h3>{{meal.title}}</h3>

        <span>{{meal.userName}}</span>

        <div class="stars">
          <span>{{meal.rating}} out of 5 <i class="fas fa-star"></i></span>
        </div>
        
        <a v-on:click="OpenRecipeById(meal.recipeId)" class="btn">See Recipe</a>
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
      async OpenRecipeById(id){
        this.mealitem = await OpenModal(id)
        this.recipeid = id;
      },
    }
}
</script>


<style scoped>
    @import "../assets/styles/recipelist.css";
</style>