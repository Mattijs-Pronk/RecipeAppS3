<script setup>
  import { CloseModal } from '../assets/Functions/Recipe';
  import {imageConvertUrl} from '../assets/Functions/services/BaseUrls';
  import {AcceptRecipeRequest} from '../assets/Functions/Admin';
  import {DeclineRecipeRequest} from '../assets/Functions/Admin';
</script>


<template>
    <div id="hidden" style="display:none" class="meal-details">
        <button v-on:click="CloseModal" class="recipe-close-btn"><i class="fas fa-times"></i></button> 
        <br/>                  
            <div class="meal-details-content" v-for="meal in itemdata">                      
                <h2 class="recipe-title">{{meal.title}}</h2>

                    <div class="recipe-meal-img">
                      <img :src="imageConvertUrl + meal.imageBase64" alt="food">
                    </div>

                        <p class="recipe-text-name">{{meal.userName}}</p>

                        <p class="recipe-category">Result</p>

                        <p class="recipe-text">Preptime: {{meal.prepTime}} minute('s'), Portion('s'): {{meal.portions}}</p><br/>

                            <h3><p class="recipe-category">Ingredients</p></h3>
                          <p class="recipe-text">{{meal.ingredients}}</p>
   
                        <h3><p class="recipe-category">Preperation</p></h3>
                      <p class="recipe-text">{{meal.description}}</p>
                    
                  <a v-on:click="OpenAcceptConfirmModal" class="btn">Accept</a>
                  <a v-on:click="EditThisRecipeRequest(meal.recipeId)" class="btn">Edit</a>
                  <a v-on:click="OpenDeclineConfirmModal" class="btn">Decline</a>

                  <!-- The Modal -->
                  <div id="declineconfirmmodal" class="modal">
                    <!-- Modal content -->
                    <div class="modal-content">
                      <span v-on:click="CloseDeclineConfirmModal" class="close">&times;</span>
                      <p>Are you sure you want to decline this recipe?</p>
                      <a v-on:click="DeclineThisRecipeRequest(meal.recipeId)" class="btn">Yes</a>
                      <a v-on:click="CloseDeclineConfirmModal" class="btn">No</a>
                    </div>
                  </div>

                  <!-- The Modal -->
                  <div id="acceptconfirmmodal" class="modal">
                    <!-- Modal content -->
                    <div class="modal-content">
                      <span v-on:click="CloseAcceptConfirmModal" class="close">&times;</span>
                      <p>Are you sure you want to accept this recipe?</p>
                      <a v-on:click="AcceptThisRecipeRequest(meal.recipeId)" class="btn">Yes</a>
                      <a v-on:click="CloseAcceptConfirmModal" class="btn">No</a>
                    </div>
                  </div>
                  
            </div>
      </div>
</template>

<script>
  export default {
    name: "recipeitem",
    data(){
      return{
        user: ''
      }
    },
    props: [
        'itemdata',
        'recipeId'
    ],
    mounted(){
      this.user = JSON.parse(localStorage.getItem("user"))
    },
    methods:{
      OpenDeclineConfirmModal(){
        document.getElementById('hidden').scroll({top:0})
        document.getElementById("declineconfirmmodal").style.display = "block";
      },
      CloseDeclineConfirmModal(){
        document.getElementById("declineconfirmmodal").style.display = "none";
      },
      OpenAcceptConfirmModal(){
        document.getElementById('hidden').scroll({top:0})
        document.getElementById("acceptconfirmmodal").style.display = "block";
      },
      CloseAcceptConfirmModal(){
        document.getElementById("acceptconfirmmodal").style.display = "none";
      },
      EditThisRecipeRequest(id){
        localStorage.setItem('recipeid', id)
        this.$router.push({name: 'editrecipe'})
      },
      async AcceptThisRecipeRequest(id){
        if(await AcceptRecipeRequest(id)){
          this.$toast.success('recipe moved to Accepted section' , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 5000
          });
          this.CloseAcceptConfirmModal();
        }
        else{
          this.$toast.error('recipe was not found' , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 5000
          });
        } 
        }
      },
      async DeclineThisRecipeRequest(id){
        if(await DeclineRecipeRequest(id)){
        this.$toast.success('recipe moved to Decline section' , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 5000
          });
          this.CloseAcceptConfirmModal();
        }
        else{
          this.$toast.error('recipe was not found' , {
          position: 'top',
          dismissible: true,
          pauseOnHover: true,
          duration: 5000
          });
        } 
      },
  }
</script>


<style scoped>
    @import "../assets/styles/recipeitem.css";
    @import '../assets/styles/confirmmodal.css';
</style>