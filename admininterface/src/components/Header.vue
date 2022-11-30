  <script setup>
    import { RouterLink } from 'vue-router'
    import {Logout} from '../assets/Functions/Auth'
  </script>
  
  <template>
    <header>
      <nav>
        <RouterLink to="/login" class="logo">Cloud Recipes</RouterLink>
        <ul>
            <div class="dropdown">
              <li><RouterLink to="#">Recipes</RouterLink></li>
                <div id="dropdown" class="dropdown-content">
                  <RouterLink to="/acceptedrecipes">Accepted</RouterLink>
                  <RouterLink to="/onholdrecipes">OnHold</RouterLink>
                  <RouterLink to="/declinedrecipes">Declined</RouterLink>
                </div>
            </div>

            <div class="dropdown">
              <li><RouterLink to="#">Users</RouterLink></li>
                <div id="dropdown" class="dropdown-content">
                  <RouterLink to="/manageusers">Manage</RouterLink>
                </div>
            </div>
            
            <li><RouterLink to="/login" v-on:click="LogoutThis()">{{login}}</RouterLink></li>
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