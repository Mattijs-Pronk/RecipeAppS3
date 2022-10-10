<script setup>
import axios from "axios";
</script>




<template>
<h2 class="recipe-title">${data.title}</h2>
              <p>${data.rating} out of 5 <span class="fa fa-star"></span></P>
              <br>
              <p class="recipe-category">Made by:</p> <p1>Mattijs Pronk</p1>

              <div class="recipe-meal-img">
              <br>
              <img src="/Images/Hamburger2.jpg" alt="food">
              </div>

              <p class="recipe-category">Result:</p> <p1>Preptime: ${data.prepTime} mins, Portion('s') ${data.Portions}</p1>

              <div class="recipe-instruct">
              <br>
              <h3>Ingredients:</h3>
              <p>${data.ingredients}</p>
              </div>

              <div class="recipe-instruct">
              <h3>Preperation:</h3>
              <p>${data.description}</p>
              </div>
</template>




<script>

</script>