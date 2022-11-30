<script setup>
import {VerifyAccount} from '../assets/Functions/Auth'
import SimpleHeader from '../components/SimpleHeader.vue';
import Footer from '../components/Footer.vue';
</script>

<template>
    <SimpleHeader/>
<div class="container2">
    <div class="container">
        <div class="forms"> 
            <div class="form login">
                <span class="title">Verify Account</span>
                <br><br><br>
                <form action="#">
                    <div class="input-field">
                        <input type="text" id="token" placeholder="Enter received token" v-model="activateaccounttoken">
                    </div>       

                    <div class="input-field">
                        <input type="text" id="email" placeholder="Email" v-model="email" @blur="checkEmail" @keyup="checkEmail">
                    </div>
                    <span v-if="emailError" class="text-danger">{{emailError}}</span>

                    <br>
                    <a class="btn" id="submit" v-on:click="submitForm()">Verify</a>
                </form>
            </div>
        </div>
    </div>
</div>
<Footer/>
</template>

<script>
export default{
    components: {
        SimpleHeader,
      Footer
    },
    data(){
        return{
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
                this.$toast.success('your account has been verified' , {
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