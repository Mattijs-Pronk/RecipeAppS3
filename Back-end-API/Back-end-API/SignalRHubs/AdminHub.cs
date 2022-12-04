using Back_end_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Back_end_API.SignalRHubs
{
    public class AdminHub:Hub
    {
        public async Task NewMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task NewRecipe(ActionResult recipe)
        {
            await Clients.All.SendAsync("ReceiveRecipe", recipe);
        }

        public async Task RemoveRecipe(int id)
        {
            await Clients.All.SendAsync("RemoveRecipe", id);
        }
    }
}
