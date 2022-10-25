<script setup>
import Header from '../components/Header.vue';
import {AddRecipe} from '../assets/Functions/Recipe';
</script>


<template>
    <Header/>
	<div class="container">
		<div class="contact-box">
			<div class="left"></div>
			<div class="right">
				<h2>Add recipe</h2>
                <br/><br/>
				<input type="text" class="field" placeholder="Title" v-model="title">
                <input type="text" class="field" placeholder="Preptime" v-model="preptime">
                <input type="text" class="field" placeholder="Portions" v-model="portions">
				<textarea type="text" class="field" placeholder="Ingredients" v-model="ingredients"></textarea>
				<textarea type="text" class="field" placeholder="Description" v-model="description"></textarea>
				<button class="btn" v-on:click="submitForm()">Add recipe</button>
			</div>
		</div>
	</div>
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
        async submitForm(){
            var userid = JSON.parse(localStorage.getItem("user"))
            if(await AddRecipe(this.title, this.preptime, this.portions, this.ingredients, this.description, userid)){
                this.$toast.success('recipe has been send for approval' , {
                position: 'top',
                dismissible: true,
                pauseOnHover: true,
                duration: 3500
                });

                this.$router.push({name: 'home'})
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