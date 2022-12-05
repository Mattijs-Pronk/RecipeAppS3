<script setup>
  import Header from '../components/Header.vue'
  import Footer from '../components/Footer.vue';
</script>

<template>
  <Header/>

  <section class="home" id="home">
    <div class="content">
        <h3>Welcome to CloudRecipes</h3>
        <p>At CloudRecipes we provide a website run by you so if you have a delicious recipe you would like to share. <RouterLink to="/login">Login</RouterLink> and upload your recipe, an administrator will doublecheck you recipe before approving it for all users to see!</p>
        <RouterLink to="/recipes" class="btn">More recipes</RouterLink>
    </div>

    <div class="image">
        <img src="../assets/Images/LandingFood.png" alt="">
    </div>
  </section>

  <Footer/>
</template>

<script>

</script>


<style scoped>
  @import '../assets/styles/landing.css';
</style>