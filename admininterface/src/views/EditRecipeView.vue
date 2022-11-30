<script setup>
import Header from '../components/Header.vue';
import Footer from '../components/Footer.vue';
import { GetRecipeById } from '../assets/Functions/Admin';
import { EditRecipeRequest } from '../assets/Functions/Admin';
import { AcceptRecipeRequest } from '../assets/Functions/Admin';
</script>


<template>
    <Header/>
    
    <div class="heading-collection">
        <h3 class="sub-heading"> Admin panel </h3>
        <h1 class="heading"> Edit Recipe </h1>
    </div>

	<div class="container">
		<div class="contact-box">
			<div class="left"></div>
			<div class="right">
				<h2>Edit recipe</h2>
                <br/><br/>
				<input type="text" id="title" class="field" placeholder="Title" v-model="title" @blur="checkTitle" @keyup="checkTitle">
                <span v-if="titleError" class="text-danger">{{titleError}}</span>

                <input type="text" id="preptime" class="field" placeholder="Preptime" v-model="preptime" @blur="checkPreptime" @keyup="checkPreptime">
                <span v-if="preptimeError" class="text-danger">{{preptimeError}}</span>

                <input type="text" id="portions" class="field" placeholder="Portions" v-model="portions" @blur="checkPortions" @keyup="checkPortions">
                <span v-if="portionsError" class="text-danger">{{portionsError}}</span>

				<textarea type="text" id="ingredients" class="field-texterea" placeholder="Ingredients" v-model="ingredients" @blur="checkIngredients" @keyup="checkIngredients"></textarea>
                <span v-if="ingredientsError" class="text-danger">{{ingredientsError}}</span>

				<textarea type="text" id="description" class="field-texterea" placeholder="Description" v-model="description" @blur="checkDescription" @keyup="checkDescription"></textarea>
                <span v-if="descriptionError" class="text-danger">{{descriptionError}}</span>
                <br/>
                <br/>
                <br/>
                <p>By editing you also accept the recipe request</p>
				<button class="btn" id="submit" v-on:click="submitForm()">Edit recipe</button>
			</div>
		</div>
	</div>
    <Footer/>
</template>


<script>
export default{
    name: 'addrecipe',
    components: {
      Header,
      Footer
    },
    data(){
        return{
            title: '',
            titleError: '',
            preptime: '',
            preptimeError: '',
            portions: '',
            portionsError: '',
            ingredients: '',
            ingredientsError: '',
            description: '',
            descriptionError: '',
            recipeid: '',
            recipe: []
        }
    },
    mounted(){
        this.GetRecipeInfo()
    },
    methods: {
    GetRecipeInfo(){
        //JSON.parse om de "" weg te halen.
        this.recipeid = JSON.parse(localStorage.getItem('recipeid'))

        this.SetRecipeInfo();
    },
    async SetRecipeInfo(){
        this.recipe = await GetRecipeById(this.recipeid)

        this.title = this.recipe[0].title
        this.preptime = this.recipe[0].prepTime
        this.portions = this.recipe[0].portions
        this.ingredients = this.recipe[0].ingredients
        this.description = this.recipe[0].description
    },
    checkTitle() {
        this.titleError = this.title.length == 0 ? 'Title cannot be empty.' :
        this.titleError = this.title.length > 18 ? 'Title is to long.' : ''
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
        this.checkTitle();
        this.checkPreptime();
        this.checkPortions();
        this.checkIngredients();
        this.checkDescription();

        if(this.titleError == '' && this.preptimeError == '' && this.portionsError == '' && this.ingredientsError == '' && this.descriptionError == '')
            {
                if(await EditRecipeRequest(this.recipeid, this.title, this.preptime, this.portions, this.ingredients, this.description)){
                    this.$toast.success('recipe has been edited for approval' , {
                    position: 'top',
                    dismissible: true,
                    pauseOnHover: true,
                    duration: 5000
                    });

                    await AcceptRecipeRequest(this.recipeid)
                    this.$router.push({name: 'acceptedrecipes'})
                }
                else{
                    this.$toast.error('recipe has not been edited, something went wrong' , {
                    position: 'top',
                    dismissible: true,
                    pauseOnHover: true,
                    duration: 5000
                });
                }
            }
        }
    }
}
</script>


<style scoped>
    @import '../assets/styles/editrecipe.css';
    @import '../assets/styles/extratext.css';
</style>