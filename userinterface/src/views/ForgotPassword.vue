<script setup>
import SimpleHeader from '../components/SimpleHeader.vue'
import {ForgotPassword} from '../assets/Functions/Auth'
</script>

<template>
<SimpleHeader/>
    <div class="container">
        <div class="forms"> 
            <div class="form login">
                <span class="title">Forgot password</span>
                <br><br><br>
                <form action="#">
                    <div class="input-field">
                        <input type="email" placeholder="Enter your email" v-model="email" @blur="checkEmail" @keyup="checkEmail">
                    </div>
                    <span v-if="emailError" class="text-danger">{{emailError}}</span>                

                    <br>
                    <div class="input-field button">
                        <input type="button" value="Send email" v-on:click="submitForm()">
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
    name: 'forgot',
    data(){
    return{
      email: '',
      emailError: '',
    }
  },
  methods: {
    checkEmail() {
      this.emailError = this.email.length == 0 ? 'Email cannot be empty.' 
      : (this.validateEmail(this.email) ? '' : this.email + ' is not an email.')
    },
    validateEmail(email) {
      const re = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
      return re.test(email);
    },
    async submitForm() {
      this.checkEmail();

      if(this.emailError == '')
      {
        if(await ForgotPassword(this.email))
        {
          this.$toast.success('Email has been send, check you email for more information' , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 3500
          });

          this.$router.push({name: 'login'})
        }
        else{
          this.$toast.error('email was not found' , {
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
    @import "../assets/styles/loginregistration.css";
</style>