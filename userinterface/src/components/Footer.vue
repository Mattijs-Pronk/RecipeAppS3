<template>
    <footer>
        <p>Copyright © 2022-{{autodate}} Mattijs Pronk, Inc - All Rights Reserved</p>
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
    footer{
        color: black;
        background-color: rgb(237, 237, 238);
        padding: 2px;
        text-align: center;
    }
</style>