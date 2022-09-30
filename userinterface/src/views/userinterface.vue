<script setup>
  import { OpenModal } from '../assets/Functions/userinterface';
  import { CloseModal } from '../assets/Functions/userinterface';
</script>

<template>

<head>
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" integrity="sha512-+4zCK9k+qNFUR5X+cKL9EIR+ZOhtIloNl9GIKS57V1MyNsYpYcUrUeQc9vNfzsWfV28IaLL3i96P9sdNyeRssA==" crossorigin="anonymous" />
</head>

    <div class = "container">
          <div class = "meal-wrapper">    
            <div class = "meal-search">
              
              <!-- Searchbar -->
              <div class = "meal-search-box">
                <input type = "text" class = "search-control" placeholder="Enter an ingredient" id = "search-input">
                <button type = "submit" class = "search-btn btn" id = "search-btn">
                  <i class = "fas fa-search"></i>
                </button>
              </div>
            </div>
            <!-- Eind van searchbar -->
      
            <div class = "meal-result">
              <h2></h2>
              <div id= "meal">
                <!-- meal item -->
                <div class = "meal-item">
                  <div class = "meal-img">
                    <img src = "/Images/Hamburger2.jpg" alt = "food">
                  </div>
                  <div class = "meal-name">
                    <h3>Hamburger</h3>
                    <p>Made by: Mattijs Pronk</p>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star"></span>
                      <span class="fa fa-star"></span>
                    <button v-on:click="startModal" class = "recipe-btn">See Recipe</button>
                  </div>
                </div>
                <!-- Eind van meal item -->
                <!-- meal item -->
                <div class = "meal-item">
                  <div class = "meal-img">
                    <img src = "/Images/Hamburger2.jpg" alt = "food">
                  </div>
                  <div class = "meal-name">
                    <h3>Hamburger</h3>
                    <p>Made by: Mattijs Pronk</p>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star"></span>
                      <span class="fa fa-star"></span>
                    <button v-on:click="startModal" class = "recipe-btn">See Recipe</button>
                  </div>
                </div>
                <!-- Eind van meal item -->
                <!-- meal item -->
                <div class = "meal-item">
                  <div class = "meal-img">
                    <img src = "/Images/Hamburger2.jpg" alt = "food">
                  </div>
                  <div class = "meal-name">
                    <h3>Hamburger</h3>
                    <p>Made by: Mattijs Pronk</p>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star"></span>
                      <span class="fa fa-star"></span>
                    <button v-on:click="startModal" class = "recipe-btn">See Recipe</button>
                  </div>
                </div>
                <!-- Eind van meal item -->
                <!-- meal item -->
                <div class = "meal-item">
                  <div class = "meal-img">
                    <img src = "/Images/Hamburger2.jpg" alt = "food">
                  </div>
                  <div class = "meal-name">
                    <h3>Hamburger</h3>
                    <p>Made by: Mattijs Pronk</p>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star"></span>
                      <span class="fa fa-star"></span>
                    <button v-on:click="startModal" class = "recipe-btn">See Recipe</button>
                  </div>
                </div>
                <!-- Eind van meal item -->
                <!-- meal item -->
                <div class = "meal-item">
                  <div class = "meal-img">
                    <img src = "/Images/Hamburger2.jpg" alt = "food">
                  </div>
                  <div class = "meal-name">
                    <h3>Hamburger</h3>
                    <p>Made by: Mattijs Pronk</p>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star"></span>
                      <span class="fa fa-star"></span>
                    <button v-on:click="startModal" class = "recipe-btn">See Recipe</button>
                  </div>
                </div>
                <!-- Eind van meal item -->
                <!-- meal item -->
                <div class = "meal-item">
                  <div class = "meal-img">
                    <img src = "/Images/Hamburger2.jpg" alt = "food">
                  </div>
                  <div class = "meal-name">
                    <h3>Hamburger</h3>
                    <p>Made by: Mattijs Pronk</p>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star checked"></span>
                      <span class="fa fa-star"></span>
                      <span class="fa fa-star"></span>
                    <button v-on:click="startModal" class = "recipe-btn">See Recipe</button>
                  </div>
                </div>
                <!-- Eind van meal item -->
              </div>
            </div>

            <!-- Gekozen meal details -->
            <div id="hidden" style="display:none" class="meal-details">
                    <button v-on:click="stopModal" type="button" class="btn recipe-close-btn">
                    <i class="fas fa-times"></i>    
                    </button>
                <div class="meal-details-content">
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
                </div>
            </div>
            <!-- Einde gekozen meal details -->
            </div>
        </div>
</template>

<script>
  export default {
    methods:{
      startModal(){
        OpenModal()
      },
      stopModal(){
        CloseModal()
      }
    }
  }
</script>


<style>
    @import "../assets/styles/userinterface.css";
</style>