<script setup>
import {ChangeProfile} from '../assets/Functions/User';
import {UsernameCheckerChangeUsername} from '../assets/Functions/User';
</script>

<template>
    <!-- Edit profile -->
<div class="right">
    <div class="edit-card">
        <div class="title">
                <h2>Edit profile</h2>
            </div>
            <hr>
            <div class="forms">
            <div class="form login">
            <form>
                <div class="input-field">
                        <input type="text" placeholder="Username" v-model="username" @blur="checkUsername" @keyup="checkUsername">
                        <i class="fa fa-clock-rotate-left info"></i>
                    </div>
                    <span v-if="usernameError" class="text-danger">{{usernameError}}</span>

                    <div class="input-field">
                        <input type="text" placeholder="Adress" v-model="adress">
                    </div>
                    <span v-if="usernameError" class="text-danger">{{adressError}}</span>

                    <div class="input-field">
                        <input type="text" placeholder="Phone" v-model="phone">
                    </div>
                    <span v-if="phoneError" class="text-danger">{{phoneError}}</span>
                    
                    <br/>
                    <div class="input-field button">
                        <p1 class="p1-text">Leave Username/Adress/Phone empty if its unchanged</p1>
                        <input type="button" value="change profile" v-on:click="submitChangeProfile()">
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
</template>


<script>
export default{
    name: 'profile',
    props: [
        'userdata',
    ],
    data(){
        return{
            username: '',
            usernameError: '',
            adress: '',
            adressError: '',
            phone: '',
            phoneError: '',
        }
    },
    methods:{
    checkEditProfile(){
        if(this.username == '' && this.adress == '' && this.phone == ''){
            return true;
        }
        else{ return false; }
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

            if(await UsernameCheckerChangeUsername(this.userusername, this.username)){ this.usernameError = 'Username already taken'}
            else{ this.usernameError = ''}

        }, 1200);
        }
    },

    async submitChangeProfile(){
        this.checkUsername();

        if(this.usernameError == '' && this.checkEditProfile() == false)
        {
            if(await ChangeProfile(this.userdata, this.username, this.adress, this.phone))
            {
                location.reload();
                this.$toast.success('Succesfully changed profile' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 3500
                });

                this.$router.push({name: 'myprofile'})
            }
            else{
                this.$toast.error('please fill in username' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 4500
                });
            }
        }
        else{
            this.$toast.error('please fill in all forms' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 4500
            });
        }
    }
}
}
</script>


<style scoped>
    @import '../assets/styles/profile.css';
</style>