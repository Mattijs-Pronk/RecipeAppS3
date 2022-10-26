  <script setup>
    import { RouterLink } from 'vue-router'
    import {Logout} from '../assets/Functions/Auth'
    import {GetUserRecipesById} from '../assets/Functions/User'
  </script>
  
  <template>
    <header>
      <a><RouterLink :to="{name: 'home'}"><img src="/Images/CloudRecipeLogo.jpg" alt="Foto" class="navphoto"></RouterLink></a>
      <div class="nav">
        <h1 class = "title">Cloud recipes</h1>
        <blockquote>Nothing brings people together like good food</blockquote>

        <div class="dropdown">
          <button class="profile-btn" id="profile">Profile</button>
            <div class="dropdown-content">
            <a href="/myrecipes" v-on:click="GetMyRecipes()" class="profile-btn">Recipes</a>
            <a v-on:click="GetMyFavorites()" class="profile-btn">Favorites</a>
            </div>
        </div>

        <RouterLink :to="{name: 'login'}" class="login-btn" id="login" v-on:click="LogoutThis()">{{login}}</RouterLink>
        <RouterLink :to="{name: 'addrecipe'}" class="addrecipe-btn" id="addrecipe">{{addrecipe}}</RouterLink>
      </div>
    </header>
  </template>
  
  <script>
  export default{
    name: 'header',
    data(){
    return{
      user: '',
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
    async GetMyRecipes(){
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

    },
    GetloggedIn(){
      //JSON.parse om de "" weg te halen.
      this.user = JSON.parse(localStorage.getItem("user"))
      var loginbtn = document.getElementById("login");
      var profilebtn = document.getElementById("profile");
      var addrecipebtn = document.getElementById("addrecipe");

      if (this.user == null)
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
  
  <style scoped>
    @import "../assets/styles/header.css";
  </style>