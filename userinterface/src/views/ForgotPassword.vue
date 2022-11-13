<script setup>
import SimpleHeader from '../components/SimpleHeader.vue'
import Footer from '../components/Footer.vue';
import {ForgotPassword} from '../assets/Functions/Auth'
</script>

<template>
<SimpleHeader/>
<div class="container2">
    <div class="container">
        <div class="forms"> 
            <div class="form login">
                <span class="title">Forgot password</span>
                <br><br><br>
                <form action="#">
                    <div class="input-field">
                        <input type="email" id="email" placeholder="Enter your email" v-model="email" @blur="checkEmail" @keyup="checkEmail">
                    </div>
                    <span v-if="emailError" class="text-danger">{{emailError}}</span>                

                    <br><br><br>
                    <a class="btn" id="submit" v-on:click="submitForm()">Send email</a>
                </form>

                <div class="login-signup">
                    <span class="text">Not a member?
                        <a class="text signup-link"><RouterLink :to="{name: 'register'}">Signup now</RouterLink></a>
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
    name: 'forgot',
    components: {
      SimpleHeader,
      Footer
    },
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
    @import "../assets/styles/auth.css";
</style>