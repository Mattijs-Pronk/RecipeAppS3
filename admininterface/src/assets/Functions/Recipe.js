import APIcalls from "./services/APIcalls";

//functie om alle geaccepteerde recepten asynchroon in te laden.
export const GetAllAcceptedRecipes = async () => {
        try{
                let result = await APIcalls.GetAllAcceptedRecipes()
                return result.data;
        }
        catch(error){
                console.log(error)
        }
};

//functie om alle recepten asynchroon in te laden.
export const GetAllOnHoldRecipes = async () => {
        try{
                let result = await APIcalls.GetAllOnHoldRecipes()
                return result.data;
        }
        catch(error){
                console.log(error)
        }
};

//functie om alle recepten asynchroon in te laden.
export const GetAllDeclinedRecipes = async () => {
        try{
                let result = await APIcalls.GetAllDeclinedRecipes()
                return result.data;
        }
        catch(error){
                console.log(error)
        }
};

//functie om het modal te openen.
export const OpenModal = async function(id){
        try{         
                let response = await APIcalls.GetRecipeById(id)
                document.getElementById("hidden").style.display = "block";
                return response.data;
        }
        catch(error){
                console.log(error)
        }
};

//functie om modal te sluiten.
export const CloseModal = () => {
        document.getElementById("hidden").style.display = "none";
}
