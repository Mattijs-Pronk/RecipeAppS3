<script setup>
import {ResetPassword} from '../assets/Functions/Auth'
</script>

<template>
    <div class="container">
        <div class="forms"> 
            <div class="form login">
                <span class="title">Reset password</span>
                <br><br><br>
                <form action="#">
                  <span v-if="resetError" id="existserror" class="text-danger">{{resetError}}</span>
                    <div class="input-field">
                        <input type="text" placeholder="Enter received token" v-model="passwordresettoken">
                    </div>             

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
                        <input type="button" value="Reset password" v-on:click="submitForm()">
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
export default{
    name: 'register',
    data(){
        return{
            resetError: '',
            passwordresettoken: '',
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

    checkPassword() {
      this.passwordError = this.password.length < 6 ? 'Password is to short.' : ''
    },

    checkRePassword(){
        this.RepasswordError = this.Repassword.length == 0 ? 'Repassword cannot be empty.' :
        this.RepasswordError = this.password != this.Repassword ? 'Password and RePassword do not match.' : ''
    },

    async submitForm(){
        this.checkPassword();
        this.checkRePassword();

        if(this.passwordError == '' && this.RepasswordError == '')
        {
            if(await ResetPassword(this.passwordresettoken, this.password))
            {
                this.$router.push({name: 'login'})
            }
            else{
            this.resetError = 'token is incorrect or already used'
            }       
        }
    },
    },
}
</script>

<style scoped>
    @import "../assets/styles/loginregistration.css";
</style>