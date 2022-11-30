<template>
    <footer>
        <div class="footer-border">
            <a class="logo">Cloud Recipes</a> 
            <br/>
            <img class="logo-img" src = "../assets/Images/CloudRecipeLogo.jpg" alt = "food">
            <a>Copyright © 2022-{{autodate}} Mattijs Pronk, Inc - All Rights Reserved</a>
            <br/>
            <RouterLink to="/login">Login</RouterLink>
        </div>
    </footer>
</template>

<script>
export default{
    data(){
        return{
            autodate: ''
        }
    },
    //mounted zorgt ervoor dat de functie wordt ingeladen bij het laden van de pagina.
    mounted(){
      this.autoIncrement()
    },
    methods: {
        autoIncrement(){
            this.autodate = (new Date().getFullYear())
        }
    }
}
</script>

<style scoped>
    @import '../assets/styles/footer.css';
</style>