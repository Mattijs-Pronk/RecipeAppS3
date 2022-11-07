<script setup>
  import Header from '../components/Header.vue'
  import Footer from '../components/Footer.vue';
  import RecipeList from '../components/RecipeList.vue';
</script>

<template>
<head>
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" integrity="sha512-+4zCK9k+qNFUR5X+cKL9EIR+ZOhtIloNl9GIKS57V1MyNsYpYcUrUeQc9vNfzsWfV28IaLL3i96P9sdNyeRssA==" crossorigin="anonymous" />
</head>
  <Header/>
    <section class="home" id="home">

    <div class="content">
        <h3>Welcome to CloudRecipes</h3>
        <p>At CloudRecipes we provide a website run by you so if you have a delicious recipe you would like to share. Login and upload your recipe, an administrator will doublecheck you recipe before approving it for all users to see!</p>
        <a href="/recipes" class="btn">See more</a>
    </div>

    <div class="image">
        <img src="../assets/Images/homeFood.png" alt="">
    </div>
</section>

  <Footer/>
</template>

<script>
  export default {
    name: "landing",
    components: {
      Header,
      Footer,
      RecipeList,
    },
    data(){
      return{
        meallist: [],
      }
    },
    methods:{

    }
  }
</script>


<style scoped>
  @import '../assets/styles/landing.css';
</style>