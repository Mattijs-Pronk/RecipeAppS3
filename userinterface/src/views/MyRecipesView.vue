<script setup>
  import { GetUserRecipesById } from '../assets/Functions/User';
  import Header from '../components/Header.vue';
  import Footer from '../components/Footer.vue';
  import searchbar from '../components/Searchbar.vue'
  import RecipeList from '../components/RecipeList.vue';
</script>

<template>
  <Header/>  

            <!-- Start van searchbar -->
              <searchbar/>
            <!-- Eind van searchbar -->

          <div class="heading-collection">
            <h3 class="sub-heading"> Find your taste </h3>
            <h1 class="heading"> Your Recipes </h1>
          </div>

            <!-- Start van meal items -->
              <RecipeList :listdata="meallist"/>
            <!-- Eind van meal items -->

      <Footer/>
</template>

<script>
  export default {
    data(){
      return{
        user: '',
        meallist: [],
      }
    },
    //mounted zorgt ervoor dat de functie wordt ingeladen bij het laden van de pagina.
    mounted(){
      this.loadMyRecipes()
    },
    methods:{
        async loadMyRecipes(){
            this.user = JSON.parse(localStorage.getItem("user"))
            this.meallist = await GetUserRecipesById(this.user)
        }
    }
  }
</script>


<style scoped>
  @import '../assets/styles/extratext.css';
</style>