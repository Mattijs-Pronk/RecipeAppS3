<script setup>
import { RouterLink, RouterView } from 'vue-router'
import Header from './components/Header.vue'
</script>

<template>
  <Header /> <!-- Zonder dit werkt de header niet -->
  <main>
    <RouterView/>
  </main>
</template>

<style scoped>
</style>
