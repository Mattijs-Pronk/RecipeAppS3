import { HubConnectionBuilder } from "@microsoft/signalr"
import { HostUrl } from "./services/BaseUrls";

export class SignalRHub {
    constructor(){
        this.connection = new HubConnectionBuilder().withUrl(HostUrl + "/signalr").build()
        

        this.connection.onclose(async () => {
            await this.connect();
        })

        this.connect()

        //message weergeven over gehele website.
        this.connection.on("ReceiveMessage", function (message) {
            sessionStorage.setItem('NewMessage', message)
            window.dispatchEvent(NewMessage)
        });

        //recept toevoegen aan list in gekozen pagina.
        this.connection.on("ReceiveRecipe", function (recipe) {
            sessionStorage.setItem("NewRecipe", JSON.stringify(recipe.value[0]));
            window.dispatchEvent(NewRecipe)
        });

        const NewMessage = new Event('NewMessage')
        const NewRecipe = new Event('NewRecipe')
    }

    async connect(){
        try {
            await this.connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
        }  
    }
}

