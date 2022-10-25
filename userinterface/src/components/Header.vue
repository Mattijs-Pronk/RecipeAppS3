  <script setup>
    import { RouterLink } from 'vue-router'
    import {Logout} from '../assets/Functions/Auth'
  </script>
  
  <template>
    <header>
      <a><RouterLink :to="{name: 'home'}"><img src="/Images/CloudRecipeLogo.jpg" alt="Foto" class="navphoto"></RouterLink></a>
      <div class="nav">
        <h1 class = "title">Cloud recipes</h1>
        <blockquote>Nothing brings people together like good food</blockquote>
        <!-- <a href = "#" class = "login-btn">Login</a> -->
        <RouterLink :to="{name: 'login'}" class="login-btn" id="login" v-on:click="LogoutThis()">{{login}}</RouterLink>
        <RouterLink :to="{name: 'home'}" class="profile-btn" id="profile">{{profile}}</RouterLink>
        <!-- <a class="favorites-btn"><RouterLink :to="{name: 'userinterface'}"><img class="imagebutton" src="/Images/HeartFavorite.jpg" alt=""></RouterLink></a> -->
        <RouterLink :to="{name: 'addrecipe'}" class="addrecipe-btn" id="addrecipe">{{addrecipe}}</RouterLink>
      </div>
    </header>
  </template>
  
  <script>
  export default{
    name: 'header',
    data(){
    return{
      login: '',
      profile: '',
      addrecipe: ''
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
      var user = JSON.parse(localStorage.getItem("user"))
      var loginbtn = document.getElementById("login");
      var profilebtn = document.getElementById("profile");
      var addrecipebtn = document.getElementById("addrecipe");

      if (user == null)
      {
        loginbtn.style.backgroundColor = "green"
        this.login = 'Login'
        profilebtn.style.display = "none"
        this.profile = ''
        addrecipebtn.style.display = "none"
        this.addrecipe = ''
      }
      else{
        loginbtn.style.backgroundColor = "red"
        this.login = 'Logout'
        profilebtn.style.display = "block"
        this.profile = 'Profile'
        addrecipebtn.style.display = "block"
        this.addrecipe = '+ recipe'
    }
    }
  }
} 
  </script>
  
  <style>
    @import "../assets/styles/header.css";
  </style>