import { Login } from "../Auth"
import API from "./API"

export default{
    GetAllRecipes(){
        return API().get('/Recipe')
    },

    GetRecipeById(id){
        return API().get(`/Recipe/id?id=${id}`)
    },

    Login(){
        return API().post(`/login?email=${email}&password=${password}`)
    }
}