import API from "./API"

export default{
    async GetAllRecipes(){
        return await API().get('/Recipe')
    },

    async GetRecipeById(id){
        return await API().get(`/Recipe/id?id=${id}`)
    },

    async Login(email, password){
        return await API().post(`/Auth/login`, {
            email: email,
            password: password
        })

    },

    async Register(username, passwordhash, email, isadmin){
        return await API().post(`/Auth/register`, {
            username: username,
            passwordHash: passwordhash,
            Email: email,
            isAdmin: isadmin
        })
    }
}