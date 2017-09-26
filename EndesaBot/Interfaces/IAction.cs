using System.Collections.Generic;
using Microsoft.Bot.Builder.Luis.Models;

namespace EndesaBot.Interfaces
{
    public interface IAction
    {
        bool Parse(string action);
        string GetQuestion(IList<EntityRecommendation> entities);
    }
}