<script setup>
import Header from '../components/Header.vue';
import Footer from '../components/Footer.vue';
import {GetAllUsers} from '../assets/Functions/User';
import {DeleteUserById} from '../assets/Functions/Admin';
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
      <td><a v-on:click=""><svg class="icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><!--! Font Awesome Pro 6.2.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --><path d="M471.6 21.7c-21.9-21.9-57.3-21.9-79.2 0L362.3 51.7l97.9 97.9 30.1-30.1c21.9-21.9 21.9-57.3 0-79.2L471.6 21.7zm-299.2 220c-6.1 6.1-10.8 13.6-13.5 21.9l-29.6 88.8c-2.9 8.6-.6 18.1 5.8 24.6s15.9 8.7 24.6 5.8l88.8-29.6c8.2-2.8 15.7-7.4 21.9-13.5L437.7 172.3 339.7 74.3 172.4 241.7zM96 64C43 64 0 107 0 160V416c0 53 43 96 96 96H352c53 0 96-43 96-96V320c0-17.7-14.3-32-32-32s-32 14.3-32 32v96c0 17.7-14.3 32-32 32H96c-17.7 0-32-14.3-32-32V160c0-17.7 14.3-32 32-32h96c17.7 0 32-14.3 32-32s-14.3-32-32-32H96z"/></svg></a>
        <a v-on:click="OpenConfirmModal(user.userId)"><svg class="icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><!--! Font Awesome Pro 6.2.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --><path d="M135.2 17.7L128 32H32C14.3 32 0 46.3 0 64S14.3 96 32 96H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H320l-7.2-14.3C307.4 6.8 296.3 0 284.2 0H163.8c-12.1 0-23.2 6.8-28.6 17.7zM416 128H32L53.2 467c1.6 25.3 22.6 45 47.9 45H346.9c25.3 0 46.3-19.7 47.9-45L416 128z"/></svg></a></td>
        
    <div id="confirmmodal" class="modal">
        <div class="modal-content">
            <span v-on:click="CloseConfirmModal" class="close">&times;</span>
            <p>Are you sure you want to delete this user?</p>
            <a v-on:click="DeleteUser" class="btn">Yes</a>
            <a v-on:click="CloseConfirmModal" class="btn">No</a>
        </div>
    </div>
    
    </tr>
  </tbody>
</table>
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