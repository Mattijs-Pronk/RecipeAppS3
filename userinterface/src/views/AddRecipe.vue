<script setup>
import Header from '../components/Header.vue';
import Footer from '../components/Footer.vue';
import {AddRecipe} from '../assets/Functions/Recipe';
</script>


<template>
    <Header/>
    <p class="message">Create your own recipe</p> 
	<div class="container">
		<div class="contact-box">
			<div class="left"></div>
			<div class="right">
				<h2>Add recipe</h2>
                <br/><br/>
				<input type="text" class="field" placeholder="Title" v-model="title" @blur="checkTitle" @keyup="checkTitle">
                <span v-if="titleError" class="text-danger">{{titleError}}</span>

                <input type="text" class="field" placeholder="Preptime" v-model="preptime" @blur="checkPreptime" @keyup="checkPreptime">
                <span v-if="preptimeError" class="text-danger">{{preptimeError}}</span>

                <input type="text" class="field" placeholder="Portions" v-model="portions" @blur="checkPortions" @keyup="checkPortions">
                <span v-if="portionsError" class="text-danger">{{portionsError}}</span>

				<textarea type="text" class="field" placeholder="Ingredients" v-model="ingredients" @blur="checkIngredients" @keyup="checkIngredients"></textarea>
                <span v-if="ingredientsError" class="text-danger">{{ingredientsError}}</span>

				<textarea type="text" class="field" placeholder="Description" v-model="description" @blur="checkDescription" @keyup="checkDescription"></textarea>
                <span v-if="descriptionError" class="text-danger">{{descriptionError}}</span>

				<button class="btn" v-on:click="submitForm()">Add recipe</button>
			</div>
		</div>
	</div>
    <Footer/>
</template>


<script>
export default{
    data(){
        return{
            title: '',
            titleError: '',
            preptime: '',
            preptimeError: '',
            portions: '',
            portionsError: '',
            image: '',
            ingredients: '',
            ingredientsError: '',
            description: '',
            descriptionError: ''
        }
    },
    methods: {
    checkTitle() {
        this.titleError = this.title.length == 0 ? 'Title cannot be empty.' :
        this.titleError = this.title.length > 16 ? 'Title is to long.' : ''
    },
    checkPreptime(){
        this.preptimeError = this.preptime.length == 0 ? 'Preptime cannot be empty.' :
        (this.validatePreptime(this.preptime) ? '' : 'only use numbers')
    },
    validatePreptime(preptime) {
        const re = (/^\d+$/);
        return re.test(preptime)
    },
    checkPortions(){
        this.portionsError = this.portions.length == 0 ? 'Portions cannot be empty.' :
        (this.validatePreptime(this.portions) ? '' : 'only use numbers')
    },
    validatePortions(portions) {
        const re = (/^\d+$/);
        return re.test(portions)
    },
    checkIngredients(){
        this.ingredientsError = this.ingredients.length == 0 ? 'Ingredients cannot be empty.' :
        this.ingredientsError = this.ingredients.length > 200 ? 'Ingredients is to long' : ''
    },
    checkDescription(){
        this.descriptionError = this.description.length == 0 ? 'Description cannot be empty.' :
        this.descriptionError = this.title.length > 1000 ? 'Description is to long.' : ''
    },
        async submitForm(){
            if(this.titleError == '' && this.preptimeError == '' && this.portionsError == '' && this.ingredientsError == '' && this.descriptionError == '')
            var userid = JSON.parse(localStorage.getItem("user"))
            if(await AddRecipe(this.title, this.preptime, this.portions, this.ingredients, this.description, userid)){
                this.$toast.success('recipe has been send for approval' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 3500
                });

                this.$router.push({name: 'myrecipes'})
            }
            else{
                this.$toast.error('recipe has not been send for approval, please login' , {
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
    @import '../assets/styles/addrecipe.css';
</style>