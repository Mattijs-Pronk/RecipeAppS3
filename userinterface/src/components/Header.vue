  <script setup>
    import { RouterLink } from 'vue-router'
    import {Logout} from '../assets/Functions/Auth'
    import {GetUserRecipesById} from '../assets/Functions/User'
  </script>
  
  <template>
    <header>
      <nav>
        <a class="logo" href="/landing">Cloud Recipes</a>
        <ul>
          <li><a href="/landing">Home</a></li>
          <li><a href="/recipes">Recipes</a></li>
          <li><a href="#">Contact us</a></li>

            <div class="dropdown">
              <li><a v-on:click="LogoutThis()" href="/login">{{login}}</a></li>
                <div id="dropdown" class="dropdown-content">
                  <a href="/addrecipe">Add recipe</a>
                  <a href="/myrecipes">MyRecipes</a>
                  <a href="/myfavorites">MyFavorites</a>
                </div>
            </div>
            
        </ul>
      </nav>
      <div class="section"></div>
    </header>
  </template>
  
  <script>
  export default{
    name: 'header',
    data(){
    return{
      user: '',
      login: '',
      welcome: '',
    }
  },
    // mounted zorgt ervoor dat de functie wordt ingeladen bij het laden van de pagina.
    mounted(){
      this.GetloggedIn()
    },
    methods:{
      LogoutThis(){
      Logout()
    },
    GetloggedIn(){
      //JSON.parse om de "" weg te halen.
      this.user = JSON.parse(localStorage.getItem("user"))

      if (this.user == null)
      {
        this.login = 'Login'
      }
      else{
        this.login = 'Logout'
      }
    },
    GetMyRecipes(){
      if(this.user != null){
        this.$toast.success('Succesfully loaded your created recipes' , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 3500
        });
      }
      else{
        this.$toast.error('Please login first' , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 4500
        });
      }
      
    },
    GetMyFavorites(){
      if(this.user != null){
        this.$toast.success('Succesfully loaded your favorite recipes' , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 3500
        });
      }
      else{
        this.$toast.error('Please login first' , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 4500
        });
      }
    },
  }
} 
  </script>
  
  <style scoped>
    @import "../assets/styles/header2.css";
  </style>