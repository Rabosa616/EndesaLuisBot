using EndesaBot.Interfaces;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EndesaBot.Services
{
    [Serializable]
    public class ContratoService : IAction
    {
        public bool Parse(string action)
        {
            return action.ToLowerInvariant().Contains("contratar");
        }
        public string GetQuestion(IList<EntityRecommendation> entities)
        {
            string result = "¿Que tipo de servicio quiere contratar?";
            if (entities != null && entities.Any(item => item.Type == "SERVICE_TYPE"))
            {
                result = $"¿Quiere contratar un servicio de {entities.FirstOrDefault(item => item.Type == "SERVICE_TYPE").Entity}?";
            }
            return result;
        }
    }
}