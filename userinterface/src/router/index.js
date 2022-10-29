import { createRouter, createWebHistory } from 'vue-router'
import {RouteGaurd} from '../assets/Functions/Auth'
import RecipesView from '../views/RecipesView.vue'
import LoginView from '../views/LoginView.vue'
import RegisterView from  '../views/RegisterView.vue'
import ForgotPassword from '../views/ForgotPassword.vue'
import ResetPassword from '../views/ResetPassword.vue'
import VerifyAccount from '../views/VerifyAccount.vue'
import AddRecipe from '../views/AddRecipe.vue'
import MyRecipes from '../views/MyRecipes.vue'
import MyFavorites from '../views/MyFavorites.vue'
import LandingView from '../views/LandingView.vue'
import MyProfile from '../views/MyProfile.vue'
import ContactUs from '../views/ContactUs.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/recipes',
      name: 'recipes',
      component: RecipesView
    },
    {
      path: '/landing',
      name: 'landing',
      component: LandingView
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
      path: '/contactus',
      name: 'contactus',
      component: ContactUs
    },
    {
      path: '/addrecipe',
      name: 'addrecipe',
      component: AddRecipe,
      beforeEnter: (to, from, next) => {
        if(!RouteGaurd()) {
          next({name: 'login'})
        }
        else{
          next()
        }
      }
    },
    {
      path: '/myrecipes',
      name: 'myrecipes',
      component: MyRecipes,
      beforeEnter: (to, from, next) => {
        if(!RouteGaurd()) {
          next({name: 'login'})
        }
        else{
          next()
        }
      }
    },
    {
      path: '/myfavorites',
      name: 'myfavorites',
      component: MyFavorites,
      beforeEnter: (to, from, next) => {
        if(!RouteGaurd()) {
          next({name: 'login'})
        }
        else{
          next()
        }
      }
    },
    {
      path: '/myprofile',
      name: 'myprofile',
      component: MyProfile,
      beforeEnter: (to, from, next) => {
        if(!RouteGaurd()) {
          next({name: 'login'})
        }
        else{
          next()
        }
      }
    },
  ]
})

export default router
