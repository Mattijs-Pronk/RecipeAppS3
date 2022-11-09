<script setup>
  import { GetAllFavoritesById } from '../assets/Functions/User';
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
            <!-- Start van searchbar -->
              <searchbar/>
            <!-- Eind van searchbar -->

          <div class="heading-collection">
            <h3 class="sub-heading"> Find your taste </h3>
            <h1 class="heading"> Favorite Recipes </h1>
          </div>

            <!-- Start van meal items -->
              <RecipeList :listdata="meallist"/>
            <!-- Eind van meal items -->
            
      <Footer/>
</template>

<script>
  export default {
    name: "myfavorites",
    components: {
      searchbar,
      Header,
      Footer,
      RecipeList,
    },
    data(){
      return{
        user: '',
        meallist: [],
      }
    },
    //mounted zorgt ervoor dat de functie wordt ingeladen bij het laden van de pagina.
    mounted(){
        this.loadMyFavorites()
    },
    methods:{
        async loadMyFavorites(){
          this.user = JSON.parse(localStorage.getItem("user"))
          this.meallist = await this.GetAllFavoritesById(this.user)   
        }
    }
  }
</script>


<style scoped>
  @import '../assets/styles/extratext.css';
</style>