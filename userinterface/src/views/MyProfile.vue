<script setup>
import {CheckUser} from '../assets/Functions/Auth';
import {CheckEmail} from '../assets/Functions/Auth';
import {ChangePassword} from '../assets/Functions/User';
import {GetUserById} from '../assets/Functions/User';
import { GetUserRecipesAmountById } from '../assets/Functions/User';
import { GetUserFavoritesAmountById } from '../assets/Functions/User';
import Header from '../components/Header.vue';
import Footer from '../components/Footer.vue';
</script>

<template>
    <head>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" integrity="sha512-+4zCK9k+qNFUR5X+cKL9EIR+ZOhtIloNl9GIKS57V1MyNsYpYcUrUeQc9vNfzsWfV28IaLL3i96P9sdNyeRssA==" crossorigin="anonymous" />
    </head>
    <Header/>
<div class="container">

    <!-- Profile -->
    <div class="profile-card">
        <div class="image-container">
            <img src="../assets/Images/ProfilePic.jpg" style="width: 100%;">
            <div class="title">
                <h2>{{userusername}}</h2>
            </div>
        </div>
        <div class="main-container">
            <p><i class="fa fa-envelope info"></i>{{useremail}}</p>
            <p><i class="fa fa-home info"></i>{{useradres}}</p>
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

    <!-- Edit profile -->
    <div class="edit-card">
        <div class="title">
                <h2>Edit profile</h2>
            </div>
            <hr>
            <div class="forms">
            <div class="form login">
            <form>
                <div class="input-field">
                        <input type="text" placeholder="Username" v-model="username" @blur="checkUsername" @keyup="checkUsername">
                        <i class="fa fa-clock-rotate-left info"></i>
                    </div>
                    <span v-if="usernameError" class="text-danger">{{usernameError}}</span>
                    
                    <div class="input-field">
                        <input type="text" placeholder="Email" v-model="email" @blur="checkEmail" @keyup="checkEmail">
                    </div>
                    <span v-if="emailError" class="text-danger">{{emailError}}</span>

                    <div class="input-field">
                        <input type="text" placeholder="Adress" v-model="adress" @blur="" @keyup="">
                    </div>
                    <span v-if="usernameError" class="text-danger">{{adressError}}</span>

                    <div class="input-field">
                        <input type="text" placeholder="Phone" v-model="phone" @blur="" @keyup="">
                    </div>
                    <span v-if="phoneError" class="text-danger">{{phoneError}}</span>
                    
                    <br/>
                    <div class="input-field button">
                        <input type="button" value="change profile" v-on:click="submitChangePassword()">
                    </div>
                </form>
            </div>
        </div>
    </div>

    <!-- Edit password -->
    <div class="password-card">
        <div class="title">
                <h2>Edit password</h2>
            </div>
            <hr>
            <div class="forms">
            <div class="form login">
            <form>
                <div class="input-field">
                        <input type="password" id="currentpass" placeholder="Current password" v-model="currentpassword" @blur="checkCurrentPassword" @keyup="checkCurrentPassword">
                    </div>
                    <span v-if="currentpasswordError" class="text-danger">{{currentpasswordError}}</span>
                    
                    <div class="input-field">
                        <input type="password" id="pass" placeholder="New password" v-model="password" @blur="checkPassword" @keyup="checkPassword">
                    </div>
                    <span v-if="passwordError" class="text-danger">{{passwordError}}</span>

                    <div class="input-field">
                        <input type="password" id="repass" placeholder="New password retype" v-model="Repassword" @blur="checkRePassword" @keyup="checkRePassword">
                    </div>
                    <span v-if="RepasswordError" class="text-danger">{{RepasswordError}}</span>
                    
                    <div class="checkbox-text">
                        <div class="checkbox-content">
                            <input type="checkbox" id="check" v-on:click="switchInputType">
                            <label for="logCheck" class="text">see password</label>
                        </div>
                    </div>
                    
                    <br/>
                    <div class="input-field button">
                        <input type="button" value="Change password" v-on:click="submitChangePassword()">
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
    <Footer/>
</template>


<script>
export default{
    name: 'profile',
    data(){
        return{
            userid: '',
            username: '',
            usernameError: '',
            email: '',
            emailError: '',
            adress: '',
            adressError: '',
            phone: '',
            phoneError: '',
            currentpassword: '',
            currentpasswordError: '',
            password: '',
            passwordError: '',
            Repassword: '',
            RepasswordError: '',

            user: '',
            userusername: '',
            useremail: '',
            useradres: '',
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
        await this.GetUserById(this.userid);
        await this.GetUserRecipesAmountById(this.userid)
        await this.GetUserFavoritesAmountById(this.userid)

        this.userusername = this.user[0].userName;
        this.useremail = this.user[0].email;

        if(this.user[0].adress == ""){this.useradres = 'Adress'}
        else{this.useradres == this.user[0].adress}

        if(this.user[0].phone == ""){this.userphone = 'Phone'}
        else{this.userphone == this.user[0].phone}

        this.useractive = this.user[0].activeSince;
        this.useraverige = 0;
    },

    switchInputType(){
    var currentsee = document.getElementById("currentpass");
      var see = document.getElementById("pass");
      var seere = document.getElementById("repass");
      var cb = document.getElementById("check");

      if(cb.checked == true){see.type = "text", seere.type = "text", currentsee.type = "text"}
      else(see.type = "password", seere.type = "password", currentsee.type = "password")
    },

    checkEmail() {
        if(this.email.length > 0)
        {
            //timer aanmaken zodat niet bij elke @keyup de api aangeroepen wordt.
            if (this.timer) {
            clearTimeout(this.timer);
            this.timer = null;
            }

            this.timer = setTimeout(async () => {

            if(await CheckEmail(this.email)){ this.emailError = 'Email already taken'}
            else{ this.emailError = ''}

        }, 1200);
        }
        
      this.emailError = this.email.length == 0 ? 'Email cannot be empty.' 
      : (this.validateEmail(this.email) ? '' : this.email + ' is not an email.')
    },

    validateEmail(email) {
      const re = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
      return re.test(email);
    },

    checkCurrentPassword(){
        this.currentpasswordError = this.currentpassword.length == 0 ? 'Current password cannot be empty.' : ''
    },

    checkPassword() {
        this.passwordError = this.password.length == 0 ? 'password cannot be empty.' :
        this.passwordError = this.password.length < 6 ? 'Password is to short.' : ''
    },

    checkRePassword(){
        this.RepasswordError = this.Repassword.length == 0 ? 'Repassword cannot be empty.' :
        this.RepasswordError = this.password != this.Repassword ? 'Password and RePassword do not match.' : ''
    },

    checkUsername(){
        if(this.username.length > 0)
        {
            //timer aanmaken zodat niet bij elke @keyup de api aangeroepen wordt.
            if (this.timer) {
            clearTimeout(this.timer);
            this.timer = null;
            }
            this.timer = setTimeout(async () => {

            if(await CheckUser(this.username)){ this.usernameError = 'Username already taken'}
            else{ this.usernameError = ''}

        }, 1200);
        }
        this.usernameError = this.username.length == 0 ? 'Username cannot be empty.' : ''
    },

    async submitChangePassword(){
        this.checkCurrentPassword();
        this.checkPassword();
        this.checkRePassword();

        if(this.currentpasswordError == '' && this.passwordError == '' && this.RepasswordError == ''){
            if(await ChangePassword(this.userid ,this.currentpassword, this.password))
            {
                this.$toast.success('Succesfully changed password' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 3500
                });
            }
            else{
                this.$toast.error('incorrect password' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 4500
                });
            }
        }
    }
}
}
</script>


<style scoped>
    @import '../assets/styles/myprofile.css';
</style>