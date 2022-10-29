<script setup>
    import {ContactUs} from '../assets/Functions/User';
    import Header from '../components/Header.vue';
    import Footer from '../components/Footer.vue';
</script>

<template>
    <Header/>
<div class="container2">
    <div class="container">
        <div class="forms"> 
            <div class="form login">
                <span class="title">Get in touch</span>
                <br><br>
                <form action="#">
                    <div class="input-field">
                        <input type="text" placeholder="Enter your name" v-model="name" @blur="checkName" @keyup="checkName">
                    </div>
                    <span v-if="nameError" class="text-danger">{{nameError}}</span>

                    <div class="input-field">
                        <input type="email" placeholder="Enter your email" v-model="email" @blur="checkEmail" @keyup="checkEmail">
                    </div>
                    <span v-if="emailError" class="text-danger">{{emailError}}</span>

                    <div class="input-field">
                        <input type="text" placeholder="Email subject" v-model="subject" @blur="checkSubject" @keyup="checkSubject">
                    </div>
                    <span v-if="subjectError" class="text-danger">{{subjectError}}</span>

                        <textarea class="email-body" type="email" placeholder="How can we help you" v-model="body" @blur="checkBody" @keyup="checkBody"></textarea>
                        <span v-if="bodyError" class="text-danger">{{bodyError}}</span>

                        <button class="btn" v-on:click="submitForm()">submit</button>
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
            name: '',
            nameError: '',
            email: '',
            emailError: '',
            subject: '',
            subjectError: '',
            body: '',
            bodyError: ''
        }
    },
    methods:{
    checkName(){
        this.nameError = this.name.length == 0 ? 'Name cannot be empty.' :
        this.nameError = this.name.length > 30 ? 'Name is to long.' : ''
    },
    checkEmail() {
      this.emailError = this.email.length == 0 ? 'Email cannot be empty.' 
      : (this.validateEmail(this.email) ? '' : this.email + ' is not an email.')
    },
    validateEmail(email) {
      const re = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
      return re.test(email);
    },
    checkSubject(){
        this.subjectError = this.subject.length == 0 ? 'Subject cannot be empty.' :
        this.subjectError = this.subject.length > 30 ? 'Subject is to long.' : ''
    },
    checkBody(){
        this.bodyError = this.body.length == 0 ? 'Body cannot be empty.' :
        this.bodyError = this.body.length > 500 ? 'Body is to long.' : ''
    },
    async submitForm(){
        this.checkName();
        this.checkEmail();
        this.checkSubject();
        this.checkBody();

        if(this.nameError == '' && this.emailError == '' && this.subjectError == '' && this.bodyError == ''){
            if(await ContactUs(this.name, this.email, this.subject, this.body)){
                this.$toast.success('Email has been send' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 3500
                });
            }
            else{
                this.$toast.error('Email has not been send, check all fields' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 3500
                });
            }
        }
    }
}
}
</script>

<style scoped>
@import '../assets/styles/contactus.css';
</style>