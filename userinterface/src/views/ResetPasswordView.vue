<script setup>
import {ResetPassword} from '../assets/Functions/Auth'
import SimpleHeader from '../components/SimpleHeader.vue';
import Footer from '../components/Footer.vue';
</script>

<template>
    <SimpleHeader/>
<div class="container2">
    <div class="container">
        <div class="forms"> 
            <div class="form login">
                <span class="title">Reset password</span>
                <br><br><br>
                <form action="#">
                    <div class="input-field">
                        <input type="text" id="token" placeholder="Enter received token" v-model="passwordresettoken">
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
                    <a class="btn" id="submit" v-on:click="submitForm()">Reset</a>
                </form>
            </div>
        </div>
    </div>
</div>
<Footer/>
</template>

<script>
export default{
    data(){
        return{
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
                this.$toast.success('Password has been changed' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 5000
                });

                this.$router.push({name: 'login'})
            }
            else{
            this.$toast.error('token is incorrect or already used' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 5000
                });
            }       
        }
    },
    },
}
</script>

<style scoped>
    @import "../assets/styles/auth.css";
</style>