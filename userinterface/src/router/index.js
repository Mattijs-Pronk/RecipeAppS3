import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import RegisterView from  '../views/RegisterView.vue'
import ForgotPassword from '../views/ForgotPassword.vue'
import ResetPassword from '../views/ResetPassword.vue'
import VerifyAccount from '../views/VerifyAccount.vue'
import AddRecipe from '../views/AddRecipe.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/home',
      name: 'home',
      component: HomeView
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/register',
      name: 'register',
      component: RegisterView
    },
    {
      path: '/forgot',
      name: 'forgot',
      component: ForgotPassword
    },
    {
      path: '/reset',
      name: 'reset',
      component: ResetPassword
    },
    {
      path: '/verify',
      name: 'verify',
      component: VerifyAccount
    },
    {
      path: '/addrecipe',
      name: 'addrecipe',
      component: AddRecipe
    },
  ]
})

export default router
