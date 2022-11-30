  <script setup>
    import { RouterLink } from 'vue-router'
    import {Logout} from '../assets/Functions/Auth'
  </script>
  
  <template>
    <header>
      <nav>
        <RouterLink to="/landing" class="logo">Cloud Recipes</RouterLink>
        <ul>
          <li><RouterLink to="/landing">Home</RouterLink></li>
          <li><RouterLink to="/recipes">Recipes</RouterLink></li>
          <li><RouterLink to="/contactus">Contact us</RouterLink></li>

            <div class="dropdown">
              <li><RouterLink to="/addrecipe" v-on:click="LogoutThis()">{{login}}</RouterLink></li>
                <div id="dropdown" class="dropdown-content">
                  <RouterLink to="/addrecipe">Add recipe</RouterLink>
                  <RouterLink to="/myrecipes">My recipes</RouterLink>
                  <RouterLink to="/myfavorites">My favorites</RouterLink>
                  <RouterLink to="/myprofile">My profile</RouterLink>
                </div>
            </div>
            
        </ul>
      </nav>
      <div class="section"></div>
    </header>
  </template>
  
  <script>
  export default{
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