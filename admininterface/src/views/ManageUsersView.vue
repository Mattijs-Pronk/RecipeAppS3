<script setup>
import Header from '../components/Header.vue';
import Footer from '../components/Footer.vue';
import { GetUserById } from '../assets/Functions/User';
import {GetAllUsers} from '../assets/Functions/User';
import {DeleteUserById} from '../assets/Functions/Admin';
import {DoubleUsernameExcludeCurrentUserName} from '../assets/Functions/User';
import {DoubleEmailExcludeCurrentEmail} from '../assets/Functions/User';
import {EditUser} from '../assets/Functions/Admin';
</script>

<template>
    <Header/>
    
    <div class="heading-collection">
        <h3 class="sub-heading"> Admin panel </h3>
        <h1 class="heading"> Manage users </h1>
    </div>

<table>
  <thead>
    <tr>
      <th scope="col">UserName</th>
      <th scope="col">Email</th>
      <th scope="col">Adress</th>
      <th scope="col">Phone</th>
      <th scope="col">ActiveSince</th>
      <th scope="col">isAdmin</th>
      <th scope="col"></th>
    </tr>
  </thead>
  <tbody>
    <tr v-for="user in userlist" :key="user.userId">
      <td data-label="Account">{{user.userName}}</td>
      <td data-label="Due Date">{{user.email}}</td>
      <td data-label="Amount">{{user.adress}}</td>
      <td data-label="Period">{{user.phone}}</td>
      <td data-label="Amount">{{user.activeSince}}</td>
      <td data-label="Period">{{user.isAdmin}}</td>
      <td><a v-on:click="OpenEditModal(user.userId)"><svg class="icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><!--! Font Awesome Pro 6.2.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --><path d="M471.6 21.7c-21.9-21.9-57.3-21.9-79.2 0L362.3 51.7l97.9 97.9 30.1-30.1c21.9-21.9 21.9-57.3 0-79.2L471.6 21.7zm-299.2 220c-6.1 6.1-10.8 13.6-13.5 21.9l-29.6 88.8c-2.9 8.6-.6 18.1 5.8 24.6s15.9 8.7 24.6 5.8l88.8-29.6c8.2-2.8 15.7-7.4 21.9-13.5L437.7 172.3 339.7 74.3 172.4 241.7zM96 64C43 64 0 107 0 160V416c0 53 43 96 96 96H352c53 0 96-43 96-96V320c0-17.7-14.3-32-32-32s-32 14.3-32 32v96c0 17.7-14.3 32-32 32H96c-17.7 0-32-14.3-32-32V160c0-17.7 14.3-32 32-32h96c17.7 0 32-14.3 32-32s-14.3-32-32-32H96z"/></svg></a>
        <a v-on:click="OpenConfirmModal(user.userId)"><svg class="icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><!--! Font Awesome Pro 6.2.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --><path d="M135.2 17.7L128 32H32C14.3 32 0 46.3 0 64S14.3 96 32 96H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H320l-7.2-14.3C307.4 6.8 296.3 0 284.2 0H163.8c-12.1 0-23.2 6.8-28.6 17.7zM416 128H32L53.2 467c1.6 25.3 22.6 45 47.9 45H346.9c25.3 0 46.3-19.7 47.9-45L416 128z"/></svg></a></td>
    </tr>
  </tbody>
</table>

<div id="confirmmodal" class="modal">
        <div class="modal-content">
            <span v-on:click="CloseConfirmModal" class="close">&times;</span>
            <p>Are you sure you want to delete this user?</p>
            <a v-on:click="DeleteUser" class="btn">Yes</a>
            <a v-on:click="CloseConfirmModal" class="btn">No</a>
        </div>
    </div>

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

<Footer/>
</template>

<script>
export default{
    name: 'manageusers',
    components: {
      Header,
      Footer
    },
    data(){
        return{
            userid: '',
            userlist: [],
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
    mounted(){
      this.FillUserList();
    },
    methods:{
    async FillUserList(){
        this.userlist = await GetAllUsers();
    },
    OpenConfirmModal(id){
        this.userid = id;
        document.getElementById("confirmmodal").style.display = "block";
    },
    CloseConfirmModal(){
        document.getElementById("confirmmodal").style.display = "none";
    },
    OpenEditModal(id){
        this.userid = id;
        document.getElementById("editmodal").style.display = "block";

        this.GetUser(id);
    },
    async GetUser(id){
      var user = await GetUserById(id);

      this.currentusername = user[0].userName;
      this.username = user[0].userName;
      this.currentemail = user[0].email;
      this.email = user[0].email;
      this.adress = user[0].adress;
      this.phone = user[0].phone;
      this.isadmin = user[0].isAdmin;
      this.switchisAdminType(this.isadmin);
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
    async DeleteUser(){
        if(await DeleteUserById(this.userid)){
            this.$toast.success(`user has been deleted` , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 3500
        });
        this.CloseConfirmModal()
        window.location.reload()
        }
        else{
          this.$toast.error(`user not deleted, selected user might be an admin` , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 4500
        });  
        }
    },
  }
}
</script>

<style scoped>
@import '../assets/styles/manageusers.css';
@import '../assets/styles/extratext.css';
@import '../assets/styles/confirmmodal.css';
</style>