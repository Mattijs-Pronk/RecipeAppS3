<script setup>
import {DoubleUsernameExcludeCurrentUserName} from '../assets/Functions/User';
import {DoubleEmailExcludeCurrentEmail} from '../assets/Functions/User';
import {EditUser} from '../assets/Functions/Admin';
</script>

<template>
<div id="editmodal" class="modal">
        <div class="modal-content">
          <span v-on:click="CloseEditModal" class="close">&times;</span>
            <span class="title">Edit user</span>
            <br><br>
                <form action="#">
                  <div class="input-field">
                        <input type="text" id="username" placeholder="Username" v-model="username" @blur="checkUsername" @keyup="checkUsername">
                    </div>
                    <span v-if="usernameError" class="text-danger">{{usernameError}}</span>

                    <div class="input-field">
                        <input type="email" id="email" placeholder="Email" v-model="email" @blur="checkEmail" @keyup="checkEmail">
                    </div>
                    <span v-if="emailError" class="text-danger">{{emailError}}</span>

                    <div class="input-field">
                        <input type="text" id="adress" placeholder="Adress" v-model="adress">
                    </div>

                    <div class="input-field">
                        <input type="text" id="phone" placeholder="Phone" v-model="phone">
                    </div>

                    <br><br><br>
                    <div class="input-field2">
                        <input type="password" id="pass" placeholder="New password" v-model="password" @blur="checkPassword" @keyup="checkPassword">
                    </div>
                    <span v-if="passwordError" class="text-danger">{{passwordError}}</span>

                    <div class="input-field2">
                        <input type="password" id="repass" placeholder="New password retype" v-model="Repassword" @blur="checkRePassword" @keyup="checkRePassword">
                    </div>
                    <span v-if="RepasswordError" class="text-danger">{{RepasswordError}}</span>
                    
                    <div class="checkbox-text">
                        <div class="checkbox-content">
                            <input type="checkbox" id="check" v-on:click="switchInputType">
                            <label for="logCheck" class="text">see password</label>
                        </div>
                    </div>

                    <br><br>
                    <span>isAdmin</span>
                    <br>
                    <label class="switch">
                      <input type="checkbox" id="isadmin" v-model="isadmin">
                      <span class="slider round"></span>
                    </label>
                        
                    <br><br>
                    <a id="submit" class="btn" v-on:click="submitForm">Submit</a>
                </form>
        </div>
    </div>
</template>

<script>
export default{
    name: 'edituser',
    data(){
        return{
            currentusername: '',
            username: '',
            usernameError: '',
            currentemail: '',
            email: '',
            emailError: '',
            adress: '',
            phone: '',
            password: '',
            passwordError: '',
            Repassword: '',
            RepasswordError: '',
            isadmin: ''
        }
    }, 
    methods:{
    async GetUser(id){
      var user = await GetUserById(id);

      console.log(user)
      this.currentusername = user[0].userName;
      this.username = user[0].userName;
      this.currentemail = user[0].email;
      this.email = user[0].email;
      this.adress = user[0].adress;
      this.phone = user[0].phone;
      this.isadmin = user[0].isAdmin;
      this.switchisAdminType(this.isadmin);0
    },
    CloseEditModal(){
        document.getElementById("editmodal").style.display = "none";
    },
    switchInputType(){
      var see = document.getElementById("pass");
      var seere = document.getElementById("repass");
      var cb = document.getElementById("check");

      if(cb.checked == true){see.type = "text", seere.type = "text"}
      else(see.type = "password", seere.type = "password")
    },
    switchisAdminType(isadmin){
      var cb = document.getElementById("isadmin");

      if(isadmin == true){cb.checked = true}
      else(cb.checked = false)
    },
    checkPassword() {
        this.passwordError = this.password.length == 0 ? '' :
        this.passwordError = this.password.length < 6 ? 'Password is to short.' : ''
        this.checkRePassword();
    },
    checkRePassword(){
        this.RepasswordError = this.password != this.Repassword ? 'Password and RePassword do not match.' : ''
    },
    checkUsername(){
      this.usernameError = this.username.length == 0 ? 'username cannot be empty' : ''
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

        this.usernameError = this.username.length >= 14 ? 'Username is to long' : ''
        }
    },
    checkEmail() {
      this.emailError = this.email.length == 0 ? 'Email cannot be empty'  : ''
      if(this.email.length > 0)
        {
            //timer aanmaken zodat niet bij elke @keyup de api aangeroepen wordt.
            if (this.timer) {
            clearTimeout(this.timer);
            this.timer = null;
            }
            this.timer = setTimeout(async () => {

            if(await DoubleEmailExcludeCurrentEmail(this.currentemail, this.email)){ 
                this.emailError = 'Email already taken'
            }

        }, 1200);
        (this.validateEmail(this.email) ? '' : this.emailError = this.email + ' is not an email.')
        }
    },
    validateEmail(email) {
      const re = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
      return re.test(email);
    },
    async submitForm(){
      this.checkUsername();
      this.checkEmail();
      this.checkPassword()
      this.checkRePassword();

      if(this.usernameError == '' && this.emailError == '' && this.passwordError == '' && this.RepasswordError == ''){
        if(await EditUser(this.userid, this.username, this.email, this.adress, this.phone, this.password, this.isadmin)){
          this.$toast.success(`user has been edited` , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 3500
        });
        this.CloseEditModal()
        window.location.reload()
        }
        else{
          this.$toast.error(`user not edited, selected user might be an admin` , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 4500
        });  
        }
      }
    },
    }
}
</script>

<style scoped>
@import '../assets/styles/manageusers.css';
@import '../assets/styles/confirmmodal.css';
</style>