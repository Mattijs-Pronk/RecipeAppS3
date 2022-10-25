import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import AboutView from '../views/AboutView.vue'
import Homepage from '../views/Homepage.vue'
import LoginView from '../views/LoginView.vue'
import RegisterView from  '../views/RegisterView.vue'
import ForgotPassword from '../views/ForgotPassword.vue'
import ResetPassword from '../views/ResetPassword.vue'
import VerifyAccount from '../views/VerifyAccount.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/about',
      name: 'about',
      component: AboutView
    },
    {
      path: '/Homepage',
      name: 'Homepage',
      component: Homepage
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
  ]
})

export default router
