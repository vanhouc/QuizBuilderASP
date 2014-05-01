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

        public static Scenario FindScenario(int scenarioId)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                return db.Scenarios.Find(scenarioId);
            }
        }

        public static Scenario UpdateScenario(Scenario updatedScenario)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                Scenario currentScenario = db.Scenarios.Find(updatedScenario.ScenarioID);
                if (currentScenario != null)
                {
                    currentScenario.ScenarioName = updatedScenario.ScenarioName;
                    currentScenario.QuizID = updatedScenario.QuizID;
                    currentScenario.ScenarioSequence = updatedScenario.ScenarioSequence;
                    currentScenario.ScenarioText = updatedScenario.ScenarioText;
                    currentScenario.Questions = updatedScenario.Questions;
                    currentScenario.IsRichText = updatedScenario.IsRichText;
                    db.SaveChanges();
                    return currentScenario;
                }
                else
                    return currentScenario;
            }
        }
    }
}