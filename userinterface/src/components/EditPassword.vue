<script setup>
import {ChangePassword} from '../assets/Functions/User';
</script>

<template>
    <!-- Edit password -->
    <div class="password-card">
                <h2>Edit password</h2>
            <hr>
            <div class="forms">
            <div class="form login">
            <form>
                <div class="input-field">
                        <input type="password" id="currentpass" placeholder="Current password" v-model="currentpassword" @blur="checkCurrentPassword" @keyup="checkCurrentPassword">
                    </div>
                    <span v-if="currentpasswordError" class="text-danger">{{currentpasswordError}}</span>
                    
                    <div class="input-field">
                        <input type="password" id="pass" placeholder="New password" v-model="password" @blur="checkPassword" @keyup="checkPassword">
                    </div>
                    <span v-if="passwordError" class="text-danger">{{passwordError}}</span>

                    <div class="input-field">
                        <input type="password" id="repass" placeholder="New password retype" v-model="Repassword" @blur="checkRePassword" @keyup="checkRePassword">
                    </div>
                    <span v-if="RepasswordError" class="text-danger">{{RepasswordError}}</span>
                    
                    <div class="checkbox-text">
                        <div class="checkbox-content">
                            <input type="checkbox" id="check" v-on:click="switchInputType">
                            <label for="logCheck" class="text">see password</label>
                        </div>
                    </div>
                    
                    <br/>
                    <a class="btn" id="submitPass" v-on:click="submitChangePassword()">Change password</a>
                </form>
            </div>
        </div>
    </div>
</template>


<script>
export default{
    name: 'editpasword',
    props: [
        'userid',
    ],
    data(){
        return{
            currentpassword: '',
            currentpasswordError: '',
            password: '',
            passwordError: '',
            Repassword: '',
            RepasswordError: '',
        }
    },
    methods:{
    switchInputType(){
    var currentsee = document.getElementById("currentpass");
      var see = document.getElementById("pass");
      var seere = document.getElementById("repass");
      var cb = document.getElementById("check");

      if(cb.checked == true){see.type = "text", seere.type = "text", currentsee.type = "text"}
      else(see.type = "password", seere.type = "password", currentsee.type = "password")
    },

    checkCurrentPassword(){
        this.currentpasswordError = this.currentpassword.length == 0 ? 'Current password cannot be empty.' : ''
    },

    checkPassword() {
        this.passwordError = this.password.length == 0 ? 'password cannot be empty.' :
        this.passwordError = this.password.length < 6 ? 'Password is to short.' : ''
        this.checkRePassword();
    },

    checkRePassword(){
        this.RepasswordError = this.Repassword.length == 0 ? 'Repassword cannot be empty.' :
        this.RepasswordError = this.password != this.Repassword ? 'Password and RePassword do not match.' : ''
    },

    async submitChangePassword(){
        this.checkCurrentPassword();
        this.checkPassword();
        this.checkRePassword();

        if(this.currentpasswordError == '' && this.passwordError == '' && this.RepasswordError == ''){
            if(await ChangePassword(this.userid ,this.currentpassword, this.password))
            {
                this.currentpassword = '';
                this.password = '';
                this.Repassword = '';
                this.$toast.success('Succesfully changed password' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 5000
                });
            }
            else{
                this.$toast.error('incorrect password' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 5000
                });
            }
        }
    },
}
}
</script>


<style scoped>
    @import '../assets/styles/profile.css';
</style>