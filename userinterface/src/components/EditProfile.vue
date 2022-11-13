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
                        <input type="text" id="username" placeholder="Username" v-model="username" @blur="checkUsername" @keyup="checkUsername">
                    </div>
                    <span v-if="usernameError" class="text-danger">{{usernameError}}</span>

                    <div class="input-field">
                        <input type="text" id="adress" placeholder="Adress" v-model="adress">
                    </div>
                    <span v-if="usernameError" class="text-danger">{{adressError}}</span>

                    <div class="input-field">
                        <input type="text" id="phone" placeholder="Phone" v-model="phone">
                    </div>
                    <span v-if="phoneError" class="text-danger">{{phoneError}}</span>
                    <br/><br/><br/><br/>
                    <p1>Leave Username/Adress/Phone empty if its unchanged</p1>
                    <a class="btn" id="submitProfile" v-on:click="submitChangeProfile()">Change profile</a>
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
                this.$toast.success('Succesfully changed profile' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 3500
                });
                
                if (this.timer) {
                clearTimeout(this.timer);
                this.timer = null;
                }
                this.timer = setTimeout( () => {
                    this.$router.go()
                }, 1200);
            }
            else{
                this.$toast.error('User not found' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 4500
                });
            }
        }
        else{
            this.$toast.error('Fill in at least one form' , {
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