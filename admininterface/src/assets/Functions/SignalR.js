import { HubConnectionBuilder } from "@microsoft/signalr"

export class SignalRHub {
    constructor(){
        //signalr runnen met docker = http://localhost:8000/signalr
        this.connection = new HubConnectionBuilder().withUrl("https://localhost:8000/signalr").build()
        

        this.connection.onclose(async () => {
            await this.connect();
        })

        this.connect()

        //message weergeven over gehele website
        this.connection.on("ReceiveMessage", function (message) {
            sessionStorage.setItem('NewMessage', message)
            window.dispatchEvent(NewMessage)
        });

        //recept teovoegen aan list in gekozen pagina
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

