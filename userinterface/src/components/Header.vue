<script setup>
    import { RouterLink } from 'vue-router'
  </script>
  
  <template>
    <header>
      <div class="brand-wrap">
        <h1>
          <RouterLink :to="{name: 'home'}"></RouterLink>
        </h1>
      </div>
    </header>
  </template>
  
  <script>

  </script>
  
  <style>

  </style>