<script setup>
    import { RouterLink } from 'vue-router'
  </script>
  
  <template>
    <header>
      <a><RouterLink :to="{name: 'Homepage'}"><img src="/Images/CloudRecipeLogo.jpg" alt="Foto" class="navphoto"></RouterLink></a>
    </header>
  </template>
  
  <script>

  </script>
  
  <style>
    @import "../assets/styles/header.css";
  </style>