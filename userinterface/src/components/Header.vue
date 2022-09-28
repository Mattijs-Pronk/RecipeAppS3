<script setup>
    import { RouterLink } from 'vue-router'
  </script>
  
  <template>
    <header>
      <a><RouterLink :to="{name: 'userinterface'}"><img src="/Images/CloudRecipeLogo.jpg" alt="Foto" class="navphoto"></RouterLink></a>
      <div class="nav">
        <h2 class = "title">Cloud recipe</h2>
        <blockquote>Nothing brings people together like good food</blockquote> <br>
        <!-- <a href = "#" class = "login-btn">Login</a> -->
        <a class="login-btn"><RouterLink :to="{name: 'userinterface'}"><img class="imagebutton" src="/Images/Login.jpg" alt=""></RouterLink></a>
        <a class="profile-btn"><RouterLink :to="{name: 'userinterface'}"><img class="imagebutton" src="/Images/ProfilePic.jpg" alt=""></RouterLink></a>
        <!-- <a class="favorites-btn"><RouterLink :to="{name: 'userinterface'}"><img class="imagebutton" src="/Images/HeartFavorite.jpg" alt=""></RouterLink></a> -->
        <a class="addrecipe-btn"><RouterLink :to="{name: 'userinterface'}"><img class="imagebutton" src="/Images/AddRecipe.jpg" alt=""></RouterLink></a>
      </div>
    </header>
  </template>
  
  <script>

  </script>
  
  <style>
    @import "../assets/styles/header.css";
  </style>