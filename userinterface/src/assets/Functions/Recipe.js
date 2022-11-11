import APIcalls from "./services/APIcalls";

//functie om alle recepten asynchroon in te laden.
export const GetAllAcceptedRecipes = async () => {
        try{
                let result = await APIcalls.GetAllAcceptedRecipes()
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

//functie om een recept toe te voegen.
export const AddRecipe = async (fd) => {
        try{
                await APIcalls.AddRecipe(fd)
                return true
        }
        catch(error){
                console.log(error)
                return false
        }
        
}