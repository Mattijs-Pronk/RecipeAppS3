import { createRouter, createWebHistory } from 'vue-router'
import {RouteGaurd} from '../assets/Functions/Auth'
import VerifyAccount from '../views/VerifyAccountView.vue'
import LandingView from '../views/LandingView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'landing2',
      component: LandingView
    },
    {
      path: '/recipes',
      name: 'recipes',
      component: () => import('../views/RecipesView.vue')
    },
    {
      path: '/landing',
      name: 'landing',
      component: () => import('../views/LandingView.vue')
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('../views/LoginView.vue')
    },
    {
      path: '/register',
      name: 'register',
      component: () => import('../views/RegisterView.vue')
    },
    {
      path: '/forgot',
      name: 'forgot',
      component: () => import('../views/ForgotPasswordView.vue')
    },
    {
      path: '/reset',
      name: 'reset',
      component: () => import('../views/ResetPasswordView.vue')
    },
    {
      path: '/verify',
      name: 'verify',
      component: () => import('../views/VerifyAccountView.vue')
    },
    {
      path: '/contactus',
      name: 'contactus',
      component: () => import('../views/ContactUsView.vue')
    },
    {
      path: '/addrecipe',
      name: 'addrecipe',
      beforeEnter: (to, from, next) => {
        if(!RouteGaurd()) {
          next({name: 'login'})
        }
        else{
          next()
        }
      },
      component: () => import('../views/AddRecipeView.vue')
    },
    {
      path: '/myrecipes',
      name: 'myrecipes',
      beforeEnter: (to, from, next) => {
        if(!RouteGaurd()) {
          next({name: 'login'})
        }
        else{
          next()
        }
      },
      component: () => import('../views/MyRecipesView.vue')
    },
    {
      path: '/myfavorites',
      name: 'myfavorites',
      beforeEnter: (to, from, next) => {
        if(!RouteGaurd()) {
          next({name: 'login'})
        }
        else{
          next()
        }
      },
      component: () => import('../views/MyFavoritesView.vue')
    },
    {
      path: '/myprofile',
      name: 'myprofile',
      beforeEnter: (to, from, next) => {
        if(!RouteGaurd()) {
          next({name: 'login'})
        }
        else{
          next()
        }
      },
      component: () => import('../views/MyProfileView.vue')
    },
  ]
})

export default router
