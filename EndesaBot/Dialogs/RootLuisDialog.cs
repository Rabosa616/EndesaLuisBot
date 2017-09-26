using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using EndesaBot.Interfaces;
using EndesaBot.Services;

namespace EndesaBot.Dialogs
{
    [LuisModel("8d050e61-542c-4e1a-8bfc-79c3bbc546b8", "256078b147dd45cb85845d5037447464")]
    [Serializable]
    public class RootLuisDialog : LuisDialog<object>
    {
        private IGreeting _greeting;
        private IHelp _help;
        private List<IAction> _actions;

        public RootLuisDialog()
        {
            _greeting = new GreetingService();
            _help = new HelpService();
            _actions = new List<IAction> { new FacturaService(), new ContratoService(), new ConsumoService() };
        }

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Perdon, no he entendido '{result.Query}'. Escriba 'ayuda' si necessita mas información.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("ACTIONS")]
        public async Task Actions(IDialogContext context, LuisResult result)
        {
            string response= _help.GetHelp();
            string action = result.Entities.FirstOrDefault(entity => entity.Type == "ACTION").Entity;
            if (_actions.Any(item => item.Parse(action)))
            {
                response = _actions.FirstOrDefault(item => item.Parse(action)).GetQuestion(result.Entities);
            }
            await context.PostAsync(response.ToString());
            //if (result.Entities != null && result.Entities.Any(item => item.Type == "SERVICE_TYPE"))
            //{ await context.PostAsync($"Quieres contratar un servicio de {result.Entities.FirstOrDefault(item => item.Type == "SERVICE_TYPE").Entity}, ¿correcto?"); }

            //if (result.Entities != null && !result.Entities.Any(item => item.Type == "SERVICE_TYPE"))
            //{ await context.PostAsync($"Quieres contratar un servicio, ¿de que tipo?"); }


            context.Wait(this.MessageReceived);
        }

        [LuisIntent("HELP")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(_help.GetHelp().ToString());
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("CUSTOMER_IDENTIFICATION")]
        public async Task CustomerIdenfitication(IDialogContext context, LuisResult result)
        {
            if (result.Entities != null && result.Entities.Any())
            {
                await context.PostAsync(_greeting.GetGreeting(result.Entities.FirstOrDefault().Entity));
            }
            else
            {
                await context.PostAsync(_greeting.GetGreeting().ToString());
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("GREETING")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(_greeting.GetGreeting().ToString());

            context.Wait(this.MessageReceived);
        }
    }
}