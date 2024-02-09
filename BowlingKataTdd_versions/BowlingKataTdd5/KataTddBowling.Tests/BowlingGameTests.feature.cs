﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace KataTddBowling.Tests
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class CalculDuScoreDunePartieDeBowlingFeature : object, Xunit.IClassFixture<CalculDuScoreDunePartieDeBowlingFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "BowlingGameTests.feature"
#line hidden
        
        public CalculDuScoreDunePartieDeBowlingFeature(CalculDuScoreDunePartieDeBowlingFeature.FixtureData fixtureData, KataTddBowling_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("fr"), "", "Calcul du score d\'une partie de bowling", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Le score sans strike ni spare est calculé correctement")]
        [Xunit.TraitAttribute("FeatureTitle", "Calcul du score d\'une partie de bowling")]
        [Xunit.TraitAttribute("Description", "Le score sans strike ni spare est calculé correctement")]
        [Xunit.InlineDataAttribute("0, 0", new string[0])]
        [Xunit.InlineDataAttribute("1, 2", new string[0])]
        [Xunit.InlineDataAttribute("5,4,2,1,5", new string[0])]
        [Xunit.InlineDataAttribute("8,0,1,2,5,1,3", new string[0])]
        [Xunit.InlineDataAttribute("0,9,0,9,1,4,5,1,2,3,1", new string[0])]
        public void LeScoreSansStrikeNiSpareEstCalculeCorrectement(string lancers, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("lancers", lancers);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Le score sans strike ni spare est calculé correctement", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 4
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 5
testRunner.Given(string.Format("on a fait les lancers suivants : {0}", lancers), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Etant donné que ");
#line hidden
#line 6
testRunner.When("on calcul le score de la partie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quand ");
#line hidden
#line 7
testRunner.Then("le score récupéré vaut l\'addition de tous les lancers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Alors ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Le score avec des spares est calculé correctement")]
        [Xunit.TraitAttribute("FeatureTitle", "Calcul du score d\'une partie de bowling")]
        [Xunit.TraitAttribute("Description", "Le score avec des spares est calculé correctement")]
        [Xunit.InlineDataAttribute("0, 10, 3", "16", "simple spare avec lancer bonus", new string[0])]
        [Xunit.InlineDataAttribute("5, 5, 5, 5, 1, 2", "29", "double spare avec lancers bonus", new string[0])]
        [Xunit.InlineDataAttribute("5,5", "10", "simple spare sans lancer bonus", new string[0])]
        public void LeScoreAvecDesSparesEstCalculeCorrectement(string lancers, string score_Attendu, string description, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("lancers", lancers);
            argumentsOfScenario.Add("score_attendu", score_Attendu);
            argumentsOfScenario.Add("description", description);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Le score avec des spares est calculé correctement", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 17
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 18
testRunner.Given(string.Format("on a fait les lancers suivants : {0}", lancers), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Etant donné que ");
#line hidden
#line 19
testRunner.When("on calcul le score de la partie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quand ");
#line hidden
#line 20
testRunner.Then(string.Format("le score récupéré vaut le score attendu : {0}", score_Attendu), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Alors ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Le score avec des strikes est calculé correctement")]
        [Xunit.TraitAttribute("FeatureTitle", "Calcul du score d\'une partie de bowling")]
        [Xunit.TraitAttribute("Description", "Le score avec des strikes est calculé correctement")]
        [Xunit.InlineDataAttribute("10", "10", "simple strike sans lancer bonus", new string[0])]
        [Xunit.InlineDataAttribute("10, 3", "16", "simple strike avec 1 lancer bonus", new string[0])]
        [Xunit.InlineDataAttribute("10, 3, 2", "20", "simple strike avec 2 lancers bonus", new string[0])]
        [Xunit.InlineDataAttribute("10, 10, 1, 2", "37", "double strike avec 2 lancers bonus", new string[0])]
        public void LeScoreAvecDesStrikesEstCalculeCorrectement(string lancers, string score_Attendu, string description, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("lancers", lancers);
            argumentsOfScenario.Add("score_attendu", score_Attendu);
            argumentsOfScenario.Add("description", description);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Le score avec des strikes est calculé correctement", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 28
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 29
testRunner.Given(string.Format("on a fait les lancers suivants : {0}", lancers), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Etant donné que ");
#line hidden
#line 30
testRunner.When("on calcul le score de la partie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quand ");
#line hidden
#line 31
testRunner.Then(string.Format("le score récupéré vaut le score attendu : {0}", score_Attendu), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Alors ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                CalculDuScoreDunePartieDeBowlingFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                CalculDuScoreDunePartieDeBowlingFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion