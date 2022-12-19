<template>
    <footer>
        <div class="footer-border">
            <RouterLink to="/landing" class="logo">Cloud Recipes</RouterLink>
            <img class="logo-img" src = "../assets/Images/CloudRecipeLogo.jpg" alt = "food">
            <p>Copyright © 2022-{{autodate}} Mattijs Pronk, Inc - All Rights Reserved</p>
            <RouterLink to="/landing">Home</RouterLink> | <RouterLink to="/contactus">Contact us</RouterLink> | <RouterLink to="/login">Login</RouterLink>
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