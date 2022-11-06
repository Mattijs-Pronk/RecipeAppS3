<script setup>
  import { GetAllRecipes } from '../assets/Functions/Recipe';
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
      this.GetAllRecipes()
    },
    methods:{

    }
  }
</script>


<style scoped>

</style>