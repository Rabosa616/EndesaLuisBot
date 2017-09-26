using EndesaBot.Interfaces;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;

namespace EndesaBot.Services
{
    [Serializable]
    public class ConsumoService : IAction
    {
        public bool Parse(string action)
        {
            return action.ToLowerInvariant().Contains("consumo");
        }
        public string GetQuestion(IList<EntityRecommendation> entities)
        {
            return "";
        }
    }
}