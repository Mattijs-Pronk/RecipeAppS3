<script setup>
    import { RouterLink } from 'vue-router'
  </script>
  
  <template>
    <header>
      <a><RouterLink :to="{name: 'userinterface'}"><img src="CloudRecipeLogo.jpg" alt="Foto" class="navphoto"></RouterLink></a>
      <div class="nav">
        <h2 class = "title">Cloud recipe</h2>
        <blockquote>Nothing brings people together like good food</blockquote> <br>
      </div>

        <!-- The sidebar -->
<div class="sidebar">
  <a><RouterLink :to="{name: 'userinterface'}">MyProfile</RouterLink></a>
  <a><RouterLink :to="{name: 'userinterface'}">MyFavorites</RouterLink></a>
  <a><RouterLink :to="{name: 'userinterface'}">Add</RouterLink></a>
  <a><RouterLink :to="{name: 'userinterface'}">Logout</RouterLink></a>
</div>
    </header>
  </template>
  
  <script>

  </script>
  
  <style>
    @import "../assets/styles/header.css";
  </style>