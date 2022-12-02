<script setup>
  import { GetAllAcceptedRecipes } from '../assets/Functions/Recipe';
  import Header from '../components/Header.vue'
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
              <h1 class="heading"> All Recipes </h1>
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
        meallist: [],
      }
    },
    //mounted zorgt ervoor dat de functie wordt ingeladen bij het laden van de pagina.
    mounted(){
      this.FillMealList();
    },
    methods:{
      async FillMealList(){
        this.meallist = await GetAllAcceptedRecipes();
      }
    }
  }
</script>


<style scoped>
  @import '../assets/styles/extratext.css';
</style>