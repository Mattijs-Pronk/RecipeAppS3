<script setup>
import Header from '../components/Header.vue';
import Footer from '../components/Footer.vue';
import {AddRecipe} from '../assets/Functions/Recipe';
</script>


<template>
    <Header/>
    
    <div class="heading-collection">
        <h3 class="sub-heading"> Find your taste </h3>
        <h1 class="heading"> Add your Recipe </h1>
    </div>

	<div class="container">
		<div class="contact-box">
			<div class="left"></div>
			<div class="right">
				<h2>Add recipe</h2>
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
                <input class="custom-file-input" type="file" accept="image/" v-on:change="GetFile"> <br/>
                <br/>
                <span v-if="fileError" class="text-danger">{{fileError}}</span>
                <br/>
				<button class="btn" id="submit" v-on:click="submitForm()">Add recipe</button>
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
            imagefile: '',
            ingredients: '',
            ingredientsError: '',
            description: '',
            descriptionError: '',
            fileError: ''
        }
    },
    methods: {
    GetFile(e){
        this.imagefile = e.target.files[0]
        this.checkFile();
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
    checkFile(){
        this.fileError = this.imagefile.length == 0 ? 'image cannot be empty' : ''
    },
        async submitForm(){
            this.checkTitle();
            this.checkPreptime();
            this.checkPortions();
            this.checkIngredients();
            this.checkDescription();
            this.checkFile();

            if(this.titleError == '' && this.preptimeError == '' && this.portionsError == '' && this.ingredientsError == '' && this.descriptionError == '' && this.fileError == '')
            {
                var userid = JSON.parse(localStorage.getItem("user"))

                const fd = new FormData()
                fd.append('Title', this.title)
                fd.append('Description', this.description)
                fd.append('Ingredients', this.ingredients)
                fd.append('imageFile', this.imagefile)
                fd.append('prepTime', this.preptime)
                fd.append('Portions', this.portions)
                fd.append('userId', userid)

                if(await AddRecipe(fd)){
                    this.$toast.success('recipe has been send for approval' , {
                    position: 'top',
                    dismissible: true,
                    pauseOnHover: true,
                    duration: 5000
                    });

                    this.$router.push({name: 'myrecipes'})
                }
                else{
                    this.$toast.error('recipe has not been send for approval, something went wrong' , {
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
    @import '../assets/styles/addrecipe.css';
    @import '../assets/styles/extratext.css';
</style>