using EndesaBot.Interfaces;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;

namespace EndesaBot.Services
{
    [Serializable]
    public class FacturaService : IAction
    {
        public bool Parse(string action)
        {
            return action.ToLowerInvariant().Contains("factura");
        }
        public string GetQuestion(IList<EntityRecommendation> entities)
        {
            return "";
        }
    }
}