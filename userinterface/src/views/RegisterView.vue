<script setup>
import {Login, Register} from '../assets/Functions/Auth'
import {CheckUser} from '../assets/Functions/Auth'
import {CheckEmail} from '../assets/Functions/Auth'
import SimpleHeader from '../components/SimpleHeader.vue'
</script>

<template>
    <SimpleHeader/>
    <div class="container">
        <div class="forms">
            <div class="form login">
                <span class="title">Register</span>
                
                <form action="#">
                    <br>
                    <span v-if="registerError" class="text-danger">{{registerError}}</span>
                    <div class="input-field">
                        <input type="text" placeholder="Username" v-model="username" @blur="checkUsername" @keyup="checkUsername">
                    </div>
                    <span v-if="usernameError" class="text-danger">{{usernameError}}</span>

                    <div class="input-field">
                        <input type="text" placeholder="Email" v-model="email" @blur="checkEmail" @keyup="checkEmail">
                    </div>
                    <span v-if="emailError" class="text-danger">{{emailError}}</span>

                    <div class="input-field">
                        <input type="password" id="pass" class="password" placeholder="Password" v-model="password" @blur="checkPassword" @keyup="checkPassword">
                    </div>
                    <span v-if="passwordError" class="text-danger">{{passwordError}}</span>

                    <div class="input-field">
                        <input type="password" id="repass" class="password" placeholder="Retype Password" v-model="Repassword" @blur="checkRePassword" @keyup="checkRePassword">
                    </div>
                    <span v-if="RepasswordError" class="text-danger">{{RepasswordError}}</span>

                    <div class="checkbox-text">
                        <div class="checkbox-content">
                            <input type="checkbox" id="check" v-on:click="switchInputType">
                            <label for="logCheck" class="text">see password</label>
                        </div>
                    </div>

                    <br>
                    <div class="input-field button">
                        <input type="button" value="Register" v-on:click="submitForm()">
                    </div>
                </form>

                <div class="login-signup">
                    <span class="text">Already have an account?
                        <a class="text signup-link"><RouterLink :to="{name: 'login'}">Login now</RouterLink></a>
                    </span>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default{
    name: 'register',
    data(){
        return{
            registerError: '',
            username: '',
            usernameError: '',
            email: '',
            emailError: '',
            password: '',
            passwordError: '',
            Repassword: '',
            RepasswordError: '',
        }
    },
    methods:{
    switchInputType(){
      var see = document.getElementById("pass");
      var seere = document.getElementById("repass");
      var cb = document.getElementById("check");

      if(cb.checked == true){see.type = "text", seere.type = "text"}
      else(see.type = "password", seere.type = "password")
    },

    checkEmail() {
        //timer aanmaken zodat niet bij elke @keyup de api aangeroepen wordt.
        if (this.timer) {
        clearTimeout(this.timer);
        this.timer = null;
        }

    this.timer = setTimeout(async () => {

        if(await CheckEmail(this.email)){ this.emailError = 'Email already taken'}
        else{ this.emailError = ''}

    }, 1800);
        
      this.emailError = this.email.length == 0 ? 'Email cannot be empty.' 
      : (this.validateEmail(this.email) ? '' : this.email + ' is not an email.')
    },

    validateEmail(email) {
      const re = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
      return re.test(email);
    },

    checkPassword() {
      this.passwordError = this.password.length < 6 ? 'Password is to short.' : ''
    },

    checkRePassword(){
        this.RepasswordError = this.Repassword.length == 0 ? 'Repassword cannot be empty.' :
        this.RepasswordError = this.password != this.Repassword ? 'Password and RePassword do not match.' : ''
    },

    checkUsername(){
        //timer aanmaken zodat niet bij elke @keyup de api aangeroepen wordt.
        if (this.timer) {
        clearTimeout(this.timer);
        this.timer = null;
        }
        this.timer = setTimeout(async () => {

        if(await CheckUser(this.username)){ this.usernameError = 'Username already taken'}
        else{ this.usernameError = ''}

    }, 1800);
        this.usernameError = this.username.length == 0 ? 'Username cannot be empty.' : ''
    },

    submitForm(){
        this.checkUsername();
        this.checkEmail();
        this.checkPassword();
        this.checkRePassword();

        if(this.passwordError == '' && this.RepasswordError == '' && this.emailError == '' && this.usernameError == '')
        {
            Register(this.username, this.email, this.password)
            //nog melding toevoegen voor succesvol account aanmaken.
            this.$router.push({name: 'login'})
        }
        else{
            this.registerError = 'account not created, please fill in all forms'
        }
        },
    },

}
</script>

<style scoped>
    @import "../assets/styles/loginregistration.css";
</style>