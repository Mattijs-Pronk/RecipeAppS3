<script setup>
import {Register} from '../assets/Functions/Auth'
import {CheckUser} from '../assets/Functions/Auth'
import SimpleHeader from '../components/SimpleHeader.vue'
import Footer from '../components/Footer.vue';
</script>

<template>
    <SimpleHeader/>
<div class="container2">
    <div class="container">
        <div class="forms">
            <div class="form login">
                <span class="title">Register</span>
                
                <form action="#">
                    <br>
                    <div class="input-field">
                        <input type="text" id="username" placeholder="Username" v-model="username" @blur="checkUsername" @keyup="checkUsername">
                    </div>
                    <span v-if="usernameError" class="text-danger">{{usernameError}}</span>

                    <div class="input-field">
                        <input type="text" id="email" placeholder="Email" v-model="email" @blur="checkEmail" @keyup="checkEmail">
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
                    <a class="btn" id="submit" v-on:click="submitForm()">Register</a>
                </form>

                <div class="login-signup">
                    <span class="text">Already have an account?
                        <a class="text signup-link"><RouterLink to="/login">Login</RouterLink></a>
                    </span><br><br>
                    <span class="text">Verify your account?
                        <a class="text signup-link"><RouterLink to="/verify">Verify</RouterLink></a>
                    </span>
                </div>
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
      this.emailError = this.email.length == 0 ? 'Email cannot be empty.' 
      : (this.validateEmail(this.email) ? '' : this.email + ' is not an email.')
    },

    validateEmail(email) {
      const re = /^([\w-.]+@([\w-]+\.)+[\w-]{2,4})?$/;
      return re.test(email);
    },

    checkPassword() {
      this.passwordError = this.password.length < 6 ? 'Password is to short.' : ''
      this.checkRePassword();
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

            if(await CheckUser(this.username)){ 
                this.usernameError = 'Username already taken'
            }

        }, 1200);
        }
        
        this.usernameError = this.username.length == 0 ? 'Username cannot be empty.' :
        this.usernameError = this.username.length >= 14 ? 'Username is to long' : ''
    },

    async submitForm(){
        this.checkUsername();
        this.checkEmail();
        this.checkPassword();
        this.checkRePassword();

        if(this.passwordError == '' && this.RepasswordError == '' && this.emailError == '' && this.usernameError == '')
        {
            if(await Register(this.username, this.email, this.password)){
                this.$toast.success('account has been created, check your email to verify your account' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 5000
                });

            this.$router.push({name: 'login'})
            }
            else{
                this.$toast.error('account not created, username or password already taken' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 5000
                });
            } 
        }
        else{
            this.$toast.error('account not created, please fill in all forms correctly' , {
            position: 'top',
            dismissible: true,
            pauseOnHover: true,
            duration: 5000
        });
        }
        },
    },

}
</script>

<style scoped>
    @import "../assets/styles/auth.css";
</style>