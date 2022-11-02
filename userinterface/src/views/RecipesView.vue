<script setup>
  import { GetAllRecipes } from '../assets/Functions/Recipe';
  import Header from '../components/Header.vue'
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
            <p class="message">Find all your desired recipes</p>  
            <div class = "meal-search">   
              <searchbar/>
            </div>

            <!-- Start van meal items -->
              <RecipeList :listdata="meallist"/>
            <!-- Eind van meal items -->
            
          </div>
      </div>
      <Footer/>
</template>

<script>
  export default {
    name: "recipes",
    components: {
      searchbar,
      Header,
      Footer,
      RecipeList,
    },
    data(){
      return{
        meallist: [],
      }
    },
    //mounted zorgt ervoor dat de functie wordt ingeladen bij het laden van de pagina.
    mounted(){
      this.GetAllRecipes()
    },
    methods:{

    }
  }
</script>


<style scoped>
    @import "../assets/styles/recipes.css";
</style>