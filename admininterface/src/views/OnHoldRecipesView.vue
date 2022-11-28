<script setup>
  import { GetAllOnHoldRecipes } from '../assets/Functions/Recipe';
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
              <h3 class="sub-heading"> Admin panel </h3>
              <h1 class="heading"> OnHold Recipes </h1>
            </div>

            <!-- Start van meal items -->
              <RecipeList :listdata="meallist"/>
            <!-- Eind van meal items -->

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
      this.FillMealList();

      window.addEventListener('NewRecipe',()=>{  
        var recipe = JSON.parse(sessionStorage.getItem("NewRecipe"));
        this.meallist.push(recipe)
      })
    },
    methods:{
      async FillMealList(){
        this.meallist = await GetAllOnHoldRecipes();
      }
    }
  }
</script>


<style scoped>
  @import '../assets/styles/extratext.css';
</style>