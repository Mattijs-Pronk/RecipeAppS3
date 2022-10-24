<script setup>
import {Login, VerifyAccount} from '../assets/Functions/Auth'
</script>

<template>
    <div class="container">
        <div class="forms"> 
            <div class="form login">
                <span class="title">Verify Account</span>
                <br><br><br>
                <form action="#">
                  <span v-if="verifyError" id="existserror" class="text-danger">{{verifyError}}</span>
                    <div class="input-field">
                        <input type="text" placeholder="Enter received token" v-model="activateaccounttoken">
                    </div>       

                    <div class="input-field">
                        <input type="text" placeholder="Email" v-model="email" @blur="checkEmail" @keyup="checkEmail">
                    </div>
                    <span v-if="emailError" class="text-danger">{{emailError}}</span>

                    <br>
                    <div class="input-field button">
                        <input type="button" value="Verify Account" v-on:click="submitForm()">
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
            verifyError: '',
            activateaccounttoken: '',
            email: '',
            emailError: '',
        }
    },
    methods:{
        checkEmail() {
        this.emailError = this.email.length == 0 ? 'Email cannot be empty.' 
      : (this.validateEmail(this.email) ? '' : this.email + ' is not an email.')
    },

    validateEmail(email) {
      const re = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
      return re.test(email);
    },

    async submitForm(){
        this.checkEmail()

        if(this.emailError == '')
        {
            if(await VerifyAccount(this.email, this.activateaccounttoken))
            {
                //nog melding toevoegen voor succesvol account activeren.
                this.$router.push({name: 'login'})
            }
            else{
                this.verifyError = 'token is incorrect or already used'
            }       
        }
    },
    },
}
</script>

<style scoped>
    @import "../assets/styles/loginregistration.css";
</style>