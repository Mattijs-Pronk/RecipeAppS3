import { createRouter, createWebHistory } from 'vue-router'
import {RouteGaurdAdmin} from '../assets/Functions/Auth'
import LoginView from '../views/LoginView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'login2',
      component: LoginView
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('../views/LoginView.vue')
    },
    {
      path: '/acceptedrecipes',
      name: 'acceptedrecipes',
      beforeEnter: async (to, from, next) => {
        if(await RouteGaurdAdmin() == false){
          next({name: 'login'});
          return false
        }
        else{
          next();
        }
      },
      component: () => import('../views/AcceptedRecipesView.vue')
    },
    {
      path: '/onholdrecipes',
      name: 'onholdrecipes',
      beforeEnter: async (to, from, next) => {
        if(await RouteGaurdAdmin() == false){
          next({name: 'login'});
          return false
        }
        else{
          next();
        }
      },
      component: () => import('../views/OnHoldRecipesView.vue')
    },
    {
      path: '/declinedrecipes',
      name: 'declinedrecipes',
      beforeEnter: async (to, from, next) => {
        if(await RouteGaurdAdmin() == false){
          next({name: 'login'});
          return false
        }
        else{
          next();
        }
      },
      component: () => import('../views/DeclinedRecipesView.vue')
    },
    {
      path: '/editrecipe',
      name: 'editrecipe',
      beforeEnter: async (to, from, next) => {
        if(await RouteGaurdAdmin() == false){
          next({name: 'login'});
          return false
        }
        else{
          next();
        }
      },
      component: () => import('../views/EditRecipeView.vue')
    },
    {
      path: '/manageusers',
      name: 'manageusers',
      beforeEnter: async (to, from, next) => {
        if(await RouteGaurdAdmin() == false){
          next({name: 'login'});
          return false
        }
        else{
          next();
        }
      },
      component: () => import('../views/ManageUsersView.vue')
    },
  ]
})

export default router
