<script setup>
import { RouterLink, RouterView } from 'vue-router'
import {SignalRHub} from '../src/assets/Functions/SignalR';
</script>

<template>
  <main>
    <RouterView/>
  </main>
</template>

<script>
export default{
  mounted(){
    this.signalr = new SignalRHub()

    window.addEventListener('NewMessage',()=>{  
    var message = sessionStorage.getItem('NewMessage')

    this.$toast.info(message , {
    position: 'bottom-right',
    dismissible: true,
    pauseOnHover: true,
    duration: 5000
  });
  })
  }
}

</script>

<style scoped>

</style>
