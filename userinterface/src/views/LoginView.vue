<script setup>
import SimpleHeader from '../components/SimpleHeader.vue';
import Footer from '../components/Footer.vue';
import {Login} from '../assets/Functions/Auth'
</script>

<template>
  <SimpleHeader/>
  <div class="container2">
    <div class="container">
        <div class="forms"> 
            <div class="form login">
                <span class="title">Login</span>
                <br><br>
                <form action="#">
                    <div class="input-field">
                        <input type="email" id="email" placeholder="Enter your email" v-model="email" @blur="checkEmail" @keyup="checkEmail">
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
                        
                        <a class="text"><RouterLink to="/forgot">Forgot password?</RouterLink></a>
                    </div>

                    <br>
                    <a id="submit" class="btn" v-on:click="submitForm">Login</a>
                </form>

                <div class="login-signup">
                    <span class="text">Not a member?
                        <a class="text signup-link"><RouterLink to="/register">Register</RouterLink></a>
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
      email: '',
      emailError: '',
      password: '',
      passwordError: ''
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
      this.emailError = this.email.length == 0 ? 'Email cannot be empty.' 
      : (this.validateEmail(this.email) ? '' : this.email + ' is not an email.')
    },
    validateEmail(email) {
      const re = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
      return re.test(email);
    },
    checkPassword() {
      this.passwordError = this.password.length == 0 ? 'Password cannot be empty.' : ''
    },
    async submitForm() {
      this.checkEmail();
      this.checkPassword();

      if(this.passwordError == '' && this.emailError == '')
      {
        if(await Login(this.email, this.password))
        {
          //nog melding toevoegen voor succesvol account aanmaken.
          this.$router.push({name: 'landing'});
          this.$toast.success(`Succesfully logged in` , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 5000
        });
        }
        else{
          this.$toast.error(`password or email did not match or account is not activated` , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 5000
        });
        }
      }
    }
  }
}
</script>

<style scoped>
    @import "../assets/styles/auth.css";
</style>