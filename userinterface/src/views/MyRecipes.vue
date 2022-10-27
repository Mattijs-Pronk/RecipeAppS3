<script setup>
  import { GetUserRecipesById } from '../assets/Functions/User';
  import Header from '../components/Header.vue';
  import Footer from '../components/Footer.vue';
  import searchbar from '../components/Searchbar.vue'
  import RecipeList from '../components/RecipeList.vue';
</script>

<template>
<head>
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" integrity="sha512-+4zCK9k+qNFUR5X+cKL9EIR+ZOhtIloNl9GIKS57V1MyNsYpYcUrUeQc9vNfzsWfV28IaLL3i96P9sdNyeRssA==" crossorigin="anonymous" />
</head>
  <Header/>
    <div class = "container">
          <div class = "meal-wrapper"> 
            <p class="message">Your created recipes</p>     
            <div class = "meal-search">
              <searchbar/>
            </div>

            <!-- Start van meal items -->
              <RecipeList :listdata="meallist" :itemstatus="status"/>
            <!-- Eind van meal items -->
            
          </div>
      </div>
      <Footer/>
</template>

<script>
  export default {
    name: "myrecipes",
    data(){
      return{
        user: '',
        meallist: [],
        status: ''
      }
    },
    //mounted zorgt ervoor dat de functie wordt ingeladen bij het laden van de pagina.
    mounted(){
      this.loadMyRecipes()
    },
    methods:{
        async loadMyRecipes(){
            this.user = JSON.parse(localStorage.getItem("user"))
            await this.GetUserRecipesById(this.user)
        }
    }
  }
</script>


<style scoped>
    @import "../assets/styles/homepage.css";
</style>