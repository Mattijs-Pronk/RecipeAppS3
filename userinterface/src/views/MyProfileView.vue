<script setup>
import {GetUserById} from '../assets/Functions/User';
import { GetUserRecipesAmountById } from '../assets/Functions/User';
import { GetUserFavoritesAmountById } from '../assets/Functions/User';
import Header from '../components/Header.vue';
import Footer from '../components/Footer.vue';
import EditProfile from '../components/EditProfile.vue';
import EditPassword from '../components/EditPassword.vue';
</script>

<template>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" integrity="sha512-+4zCK9k+qNFUR5X+cKL9EIR+ZOhtIloNl9GIKS57V1MyNsYpYcUrUeQc9vNfzsWfV28IaLL3i96P9sdNyeRssA==" crossorigin="anonymous" />
    <Header/>

        <div class="heading-collection">
            <h3 class="sub-heading"> change your profile </h3>
            <h1 class="heading"> Your Profile </h1>
        </div>

<div class="container">

    <!-- Profile -->
<div class="left">
    <div class="profile-card">
        <div class="image-container">
            <img src="../assets/Images/profilePic.png" style="width: 100%;">
            <div class="title">
                <h2>{{userusername}}</h2>
            </div>
        </div>
        <div class="main-container">
            <p><i class="fa fa-envelope info"></i>{{useremail}}</p>
            <p><i class="fa fa-home info"></i>{{useradress}}</p>
            <p><i class="fa fa-phone info"></i>{{userphone}}</p>
            <p><i class="fa fa-clock info"></i>{{useractive}}</p>
            <br/>
            <hr>
            <br/>
            <p><b><i class="fa fa-asterisk info"></i>Statistics:</b></p>
            <br/>
            <p><i class="fa fa-plus info"></i>Recipes created: {{userrecipes}}</p>
            <p><i class="fa fa-star info"></i>Average rating: {{useraverige}}</p>
            <p><i class="fa fa-heart info"></i>Current favorites: {{userfavorites}}</p>
        </div>
    </div>
</div>

<div class="right">

    <!-- Edit profile -->
    <EditProfile :userid="userid" :currentusername="userusername"/>

    <!-- Edit password -->
    <EditPassword :userid="userid"/>

</div>
    
</div>
    <Footer/>
</template>


<script>
export default{
    name: 'myprofile',
    components: {
      Header,
      Footer,
      EditProfile,
      EditPassword
    },
    data(){
        return{
            userid: '',

            user: '',
            userusername: '',
            useremail: '',
            useradress: '',
            userphone: '',
            useractive: '',
            userrecipes: '',
            useraverige: '',
            userfavorites: ''
        }
    },
    mounted(){
        this.GetUserInfo()
    },
    methods:{
    GetUserInfo(){
        //JSON.parse om de "" weg te halen.
        this.userid = JSON.parse(localStorage.getItem("user"));

        this.SetUserInfo();
    },
    async SetUserInfo(){
        this.user = await GetUserById(this.userid);
        this.userrecipes = await GetUserRecipesAmountById(this.userid)
        this.userfavorites = await GetUserFavoritesAmountById(this.userid)

        this.userusername = this.user[0].userName;
        this.useremail = this.user[0].email;

        this.useradress = this.user[0].adress;
        this.userphone = this.user[0].phone;

        this.useractive = this.user[0].activeSince;
        this.useraverige = 0;
    },
}
}
</script>


<style scoped>
    @import '../assets/styles/profile.css';
    @import '../assets/styles/extratext.css';
</style>