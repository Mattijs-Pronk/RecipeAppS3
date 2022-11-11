<script setup>
import {ChangeProfile} from '../assets/Functions/User';
import {DoubleUsernameExcludeCurrentUserName} from '../assets/Functions/User';
</script>

<template>
    <!-- Edit profile -->
<div class="right">
    <div class="edit-card">
        <div class="title-edit">
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
                    <br/><br/><br/><br/>
                    <p1>Leave Username/Adress/Phone empty if its unchanged</p1>
                    <a v-on:click="submitChangeProfile()" class="btn">Change profile</a>
                </form>
            </div>
        </div>
    </div>
</div>
</template>


<script>
export default{
    name: 'editprofile',
    props: [
        'userid',
        'currentusername'
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

            if(await DoubleUsernameExcludeCurrentUserName(this.currentusername, this.username)){ 
                this.usernameError = 'Username already taken'
            }

        }, 1200);

        this.usernameError = this.username.length = 0 ? '' :
        this.usernameError = this.username.length >= 14 ? 'Username is to long' : ''
        }
    },

    async submitChangeProfile(){
        this.checkUsername();

        if(this.usernameError == '' && this.checkEditProfile() == false)
        {
            if(await ChangeProfile(this.userid, this.username, this.adress, this.phone))
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