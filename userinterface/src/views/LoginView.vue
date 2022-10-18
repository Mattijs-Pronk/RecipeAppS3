<script setup>
//import {Login} from '../assets/Functions/Auth'
import APIcalls from "../assets/Functions/services/APIcalls";
</script>

<template>
    <div class="container">
        <div class="forms"> 
            <div class="form login">
                <span class="title">Login</span>
                <br><br>
                <form action="#">
                    <span v-if="loginError" class="text-danger">{{loginError}}</span>
                    <div class="input-field">
                        <input type="email" placeholder="Enter your email" v-model="email" @blur="checkEmail" @keyup="checkEmail">
                    </div>
                    <span v-if="emailError" class="text-danger">{{emailError}}</span>
                        
                    <div class="input-field">
                        <input type="password" id="pass" class="password" placeholder="Enter your password" v-model="password" @blur="checkPassword" @keyup="checkPassword">  
                    </div>
                    <span v-if="passwordError" class="text-danger">{{passwordError}}</span>

                    <div class="checkbox-text">
                        <div class="checkbox-content">
                            <input type="checkbox" id="check" v-on:click="switchInputType">
                            <label for="logCheck" class="text">see password</label>
                        </div>
                        
                        <a href="#" class="text">Forgot password?</a>
                    </div>

                    <div class="input-field button">
                        <input type="button" value="Login" v-on:click="submitForm()">
                    </div>
                </form>

                <div class="login-signup">
                    <span class="text">Not a member?
                        <a class="text signup-link"><RouterLink :to="{name: 'register'}">Signup now</RouterLink></a>
                    </span>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default{
    name: 'login',
    data(){
    return{
      email: '',
      emailError: '',
      password: '',
      passwordError: '',
      loginError: '',
    }
  },
  methods: {
    switchInputType(){
      var see = document.getElementById("pass");
      var cb = document.getElementById("check");

      if(cb.checked == true){see.type = "text"}
      else(see.type = "password")
    },
    checkEmail() {
      this.loginError = this.loginError.length > 0 ? '' : ''
      this.emailError = this.email.length == 0 ? 'Email cannot be empty.' 
      : (this.validateEmail(this.email) ? '' : this.email + ' is not an email.')
    },
    validateEmail(email) {
      const re = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
      return re.test(email);
    },
    checkPassword() {
      this.loginError = this.loginError.length > 0 ? '' : ''
      this.passwordError = this.password.length == 0 ? 'Password cannot be empty.' : ''
    },
    async submitForm() {
      this.checkEmail();
      this.checkPassword();
      this.loginError = 'incorrect information'

      if(this.passwordError == '' && this.emailError == '')
      {
        await APIcalls.Login(this.email, this.password)
        .then(response => {

        if(response.status == 200){
        //localStorage.setItem('userId', JSON.stringify(response.data))
        this.$router.push({name: 'Homepage'})
        console.log("succes")
        }
        }) 
      }
    }
  }
}
</script>

<style scoped>
    @import "../assets/styles/loginregistration.css";
</style>