<script setup>
  import { GetRandomRecipes } from '../assets/Functions/Recipe';
  import Header from '../components/Header.vue'
  import Footer from '../components/Footer.vue';
  import RecipeList from '../components/RecipeList.vue';
</script>

<template>
<head>
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" integrity="sha512-+4zCK9k+qNFUR5X+cKL9EIR+ZOhtIloNl9GIKS57V1MyNsYpYcUrUeQc9vNfzsWfV28IaLL3i96P9sdNyeRssA==" crossorigin="anonymous" />
</head>
  <Header/>
    <div class = "container">
          <div class = "meal-wrapper">  
            <p class="message">Welcome to CLoud recipes</p>
            <br/><br/><br/><br/><br/><br/>
            <div class="welcome">
                <h1 class="welcome-title">Featured recipes</h1>
            </div>  
            <div class = "meal-search">   
            </div>
                <div class="featured">
                    <button v-on:click="GetNewFeatured()" class="arrow-right-btn"><i class="fas fa-chevron-circle-right"></i></button>
                    <button v-on:click="GetNewFeatured()" class="arrow-left-btn"><i class="fas fa-chevron-circle-left"></i></button>  
                    <!-- Start van meal items -->
                        <RecipeList :listdata="meallist"/>
                    <!-- Eind van meal items -->
                    
                </div>
            <br/>
            <p class="clickme">Want to see more recipes? click <a href="/recipes">here</a></p>

            <br/><br/><br/><br/>
            <div class="welcome">
                <h1 class="welcome-title">About us</h1>
                <br/>
                <p class="welcome-text">Sed consectetur euismod erat, a venenatis nibh dapibus id. Integer cursus arcu odio. Aenean ut finibus risus. Curabitur tincidunt iaculis quam nec tristique. Donec mi nisi, porttitor aliquam sapien eu, varius vulputate nulla. Mauris congue sapien eros. Phasellus leo ligula, gravida ac tempus non, dignissim vitae purus. Nullam diam massa, vehicula a orci at, pharetra hendrerit turpis. Maecenas tempus sit amet nulla vitae scelerisque. Etiam eu mauris rutrum, cursus mi eu, lacinia tellus. Suspendisse venenatis vulputate lectus, vel dapibus ante lobortis eu. Ut vehicula viverra mauris sit amet gravida. Sed quam nulla, faucibus at dui sit amet, hendrerit condimentum ipsum. Curabitur faucibus risus non erat suscipit condimentum. Morbi vel ullamcorper augue.</p>
            </div>  
            <br/><br/><br/><br/>
          </div>
      </div>
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
    //mounted zorgt ervoor dat de functie wordt ingeladen bij het laden van de pagina.
    mounted(){
      this.GetRandomRecipes()
    },
    methods:{
      GetNewFeatured(){
        this.GetRandomRecipes()
      }
    }
  }
</script>


<style scoped>

</style>