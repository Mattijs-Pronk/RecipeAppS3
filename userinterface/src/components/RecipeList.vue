<script setup>
  import { OpenModal } from '../assets/Functions/Recipe';
  import { GetFavorite } from '../assets/Functions/User';
  import RecipeItem from '../components/RecipeItem.vue';
  import {imageConvertUrl} from '../assets/Functions/services/BaseUrls';
</script>


<template>
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css">

<section class="dishes" id="dishes">

<div class="box-container">

    <div class="box" v-for="meal in listdata" :key="meal.recipeId">
      <div class="status">{{meal.status}}</div>  
      
      <img :src="imageConvertUrl + meal.imageBase64" alt="food">

      <div class="header-items">
        <a><svg class="icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><!--! Font Awesome Pro 6.2.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --><path d="M224 256c70.7 0 128-57.3 128-128S294.7 0 224 0S96 57.3 96 128s57.3 128 128 128zm-45.7 48C79.8 304 0 383.8 0 482.3C0 498.7 13.3 512 29.7 512H418.3c16.4 0 29.7-13.3 29.7-29.7C448 383.8 368.2 304 269.7 304H178.3z"/></svg>
        <span>{{meal.userName}}</span></a>

        <a><svg class="icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><!--! Font Awesome Pro 6.2.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --><path d="M96 32V64H48C21.5 64 0 85.5 0 112v48H448V112c0-26.5-21.5-48-48-48H352V32c0-17.7-14.3-32-32-32s-32 14.3-32 32V64H160V32c0-17.7-14.3-32-32-32S96 14.3 96 32zM448 192H0V464c0 26.5 21.5 48 48 48H400c26.5 0 48-21.5 48-48V192z"/></svg>
        <span>{{meal.createDate}}</span></a>
      </div>

        <h3>{{meal.title}}</h3>
        
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

        var user = JSON.parse(localStorage.getItem("user"))  
        
        GetFavorite(user, id)
      },
    }
}
</script>


<style scoped>
    @import "../assets/styles/recipelist.css";
</style>