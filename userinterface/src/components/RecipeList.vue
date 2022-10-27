<script setup>
  import { OpenModal } from '../assets/Functions/Recipe';
  import RecipeItem from '../components/RecipeItem.vue';
</script>


<template>
    <div class = "meal-result">
              <h2></h2>
              <div id= "meal">   
                <div class = "meal-item" v-for="meal in listdata">
                    <div class = "meal-img">
                      <img src = "../assets/Images/foodExample.jpg" alt = "food">
                    </div>
                    <div class = "meal-name">
                      <h3>{{meal.title}}</h3>
                      <p>Made by: {{meal.userName}}</p>          
                      <p>{{meal.rating}} out of 5 <span class="fa fa-star checked"></span></P>
                      <button v-on:click="OpenRecipeById(meal.recipeId)" class = "recipe-btn">See Recipe</button>
                      <div class="status">{{itemstatus}}</div>  
                    </div>
                  </div>
            </div>
          </div>

          <!-- Start mealDetials -->
          <RecipeItem :itemdata="mealitem" :recipeId="recipeid"/>
          <!-- Eind mealDetails -->

</template>

<script>
  export default {
    name: "recipelist",
    props: [
        'listdata',
        'itemstatus'
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
      },
  }
}
</script>


<style scoped>
    @import "../assets/styles/homepage.css";
</style>