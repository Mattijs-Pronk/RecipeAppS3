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
          <li><a href="/contactus">Contact us</a></li>

            <div class="dropdown">
              <li><a v-on:click="LogoutThis()" href="/login">{{login}}</a></li>
                <div id="dropdown" class="dropdown-content">
                  <a href="/addrecipe">Add recipe</a>
                  <a href="/myrecipes">MyRecipes</a>
                  <a href="/myfavorites">MyFavorites</a>
                  <a href="/myprofile">MyProfile</a>
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
    }
  }
} 
  </script>
  
  <style scoped>
    @import "../assets/styles/header2.css";
  </style>