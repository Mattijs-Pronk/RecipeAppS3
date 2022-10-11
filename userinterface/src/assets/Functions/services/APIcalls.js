import API from "./API"

export default{
    GetAllRecipes(){
        return API().get('/Recipe')
    },

    GetRecipeById(id){
        return API().get(`/Recipe/id?id=${id}`)
    }
}