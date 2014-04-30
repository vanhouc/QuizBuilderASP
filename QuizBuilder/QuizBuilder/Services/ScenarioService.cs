using QuizBuilder.DataContexts;
using QuizBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizBuilder.Services
{
    public static class ScenarioService
    {
        public static Scenario AddScenario(Scenario scenario)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                Scenario toAdd = scenario;
                db.Scenarios.Add(toAdd);
                db.SaveChanges();
                return toAdd;
            }
        }

        public static Scenario[] GetScenarios()
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                return (from s in db.Scenarios
                        select s).ToArray<Scenario>();
            }
        }
    }
}