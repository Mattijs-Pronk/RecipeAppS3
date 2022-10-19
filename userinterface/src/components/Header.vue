<script setup>
    import { RouterLink } from 'vue-router'
    import {Logout} from '../assets/Functions/Auth'
  </script>
  
  <template>
    <header>
      <a><RouterLink :to="{name: 'Homepage'}"><img src="/Images/CloudRecipeLogo.jpg" alt="Foto" class="navphoto"></RouterLink></a>
      <div class="nav">
        <h2 class = "title">Cloud recipe</h2>
        <blockquote>Nothing brings people together like good food</blockquote> <br>
        <!-- <a href = "#" class = "login-btn">Login</a> -->
        <a class="login-btn" id="loggedin" v-on:click="LogoutThis()"><RouterLink :to="{name: 'login'}"><img class="imagebutton" src="/Images/Login.jpg" alt=""></RouterLink></a>
        <a class="profile-btn" ><RouterLink :to="{name: 'Homepage'}"><img class="imagebutton" src="/Images/ProfilePic.jpg" alt=""></RouterLink></a>
        <!-- <a class="favorites-btn"><RouterLink :to="{name: 'userinterface'}"><img class="imagebutton" src="/Images/HeartFavorite.jpg" alt=""></RouterLink></a> -->
        <a class="addrecipe-btn"><RouterLink :to="{name: 'Homepage'}"><img class="imagebutton" src="/Images/AddRecipe.jpg" alt=""></RouterLink></a>
      </div>
    </header>
  </template>
  
  <script>
  export default{
    // mounted zorgt ervoor dat de functie wordt ingeladen bij het laden van de pagina.
    mounted(){
      this.GetloggedIn()
    },
    methods:{
      LogoutThis(){
      Logout()
    },
    GetloggedIn(){
      var loginbtn = document.getElementById("loggedin");
      if (localStorage.getItem("userName") === null)
      {
        loginbtn.style.backgroundColor = "red"
      }
      else{loginbtn.style.backgroundColor = "green"}
    }
  }
}

  
  </script>
  
  <style>
    @import "../assets/styles/header.css";
  </style>