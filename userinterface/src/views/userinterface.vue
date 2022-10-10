<script setup>
  import { CloseModal } from '../assets/Functions/CloseModal';
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
                
                <!-- Meal-item word hier ingeladen -->

            </div>
          </div>
          <!-- Eind van meal items -->

          <!-- Start van modal -->
            <div id="hidden" style="display:none" class="meal-details">
                    <button v-on:click="CloseModal()" type="button" class="btn recipe-close-btn">
                    <i class="fas fa-times"></i></button> 
                    <div class="meal-details-content"></div>   
                    
                <!-- Meal-item-content word hier ingeladen -->

                </div>
          <!-- Eind van modal -->


          </div>
      </div>
</template>

<script>
  export default {
    //mounted zorgt ervoor dat de functie wordt ingeladen bij het laden van de pagina.
    mounted(){
      this.LoadRecipes()
    },
    methods:{
    }
  }
</script>


<style>
    @import "../assets/styles/userinterface.css";
</style>