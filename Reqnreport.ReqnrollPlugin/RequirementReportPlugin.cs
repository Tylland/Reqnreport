using System.Text;
using Reqnroll;
using Reqnroll.BoDi;
using Reqnroll.Events;
using Reqnroll.Plugins;
using Reqnroll.UnitTestProvider;
using Trafikverket.Strateg.Reqnreport.ReqnrollPlugin;
using Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Attachments;
using Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Model;

[assembly: RuntimePlugin(typeof(RequirementReportPlugin))]
namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin
{
    public class RequirementReportPlugin : IRuntimePlugin
    {
        private readonly string _outputDirectory;
        private RequirementReportDocument? _document;

        private readonly RequirementReportEventListner _eventListner = new();

        public RequirementReportPlugin()
        {
            _outputDirectory = Path.Combine(Environment.CurrentDirectory, new RequirementReportSettings().DirectoryName);

            if (!Directory.Exists(_outputDirectory))
            {
                Directory.CreateDirectory(_outputDirectory);
            }
        }

        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters, UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeGlobalDependencies += OnCustomizeGlobalDependencies;
            runtimePluginEvents.CustomizeTestThreadDependencies += OnCustomizeTestThreadDependencies;
        }

        private void OnCustomizeGlobalDependencies(object? sender, CustomizeGlobalDependenciesEventArgs e)
        {
            // Register dependencies here if needed
        }

        private void OnCustomizeTestThreadDependencies(object? sender, CustomizeTestThreadDependenciesEventArgs e)
        {
            var container = e.ObjectContainer;
            var type = typeof(TestRunner);

            container.RegisterFactoryAs<ITestRunner>(
                () => new RequirementReportTestRunner(
                    container.Resolve<IUnitTestRuntimeProvider>(),
                    container.Resolve<ITestRunnerManager>(),
                    container.Resolve<ITestRunner>(type.FullName)
                )
            );

            container.RegisterTypeAs<ITestRunner>(type, type.FullName);

            container.RegisterTypeAs<RequirementReportOutputHelper, IRequirementReportOutputHelper>();

            RegisterEventHandlers(container);
        }

        private void RegisterEventHandlers(ObjectContainer container)
        {
            if (!container.IsRegistered<ITestThreadExecutionEventPublisher>())
            {
                return;
            }

            var publisher = container.Resolve<ITestThreadExecutionEventPublisher>();


            publisher.AddListener(_eventListner);

            publisher.AddHandler<HookFinishedEvent>(HandleHookFinished);
        }

        static string ToMd5(string input)
        {

            var inputBytes = Encoding.UTF8.GetBytes(input);
            var outputBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

            return ToHexString(outputBytes);
        }

        static string ToHexString(byte[] inputBytes)
        {
            var sb = new StringBuilder();
            
            foreach (byte b in inputBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        private void HandleHookFinished(HookFinishedEvent evt)
        {
            if (evt.HookType == Reqnroll.Bindings.HookType.BeforeFeature)
            {
                var path = Path.Combine(_outputDirectory, ToMd5(evt.FeatureTitle()) + "_feature.md");

                _document = new RequirementReportDocument(path);

                _document.WithFeature(evt.FeatureTitle()).WithDescription(evt.FeatureContext.FeatureInfo.Description);
            }

            if (_document != null)
            {
                if (evt.HookType == Reqnroll.Bindings.HookType.BeforeScenario)
                {
                    var scenarioBuilder = _document.WithFeature(evt.FeatureTitle()).WithScenario(evt.ScenarioTitle());

                    _eventListner.CurrentScenarioBuilder = scenarioBuilder;

                    scenarioBuilder.WithDescription(evt.ScenarioContext.ScenarioInfo.Description);
                    scenarioBuilder.WithTags(evt.ScenarioContext.ScenarioInfo.CombinedTags);
                }

                if (evt.HookType == Reqnroll.Bindings.HookType.BeforeStep)
                {
                    _document.WithFeature(evt.FeatureTitle()).WithScenario(evt.ScenarioTitle())
                        .AddStep(evt.StepContext.StepText());
                }

                if (evt.HookType == Reqnroll.Bindings.HookType.AfterScenario)
                {
                    _document.WithFeature(evt.FeatureTitle()).WithScenario(evt.ScenarioTitle())
                        .WithTestResult(GetTestResult(evt.ScenarioContext.ScenarioExecutionStatus));

                    _eventListner.CurrentScenarioBuilder = null;
                }

                if (evt.HookType == Reqnroll.Bindings.HookType.AfterFeature)
                {
                    _document.Save();
                }
            }
        }

        private static TestResult GetTestResult(ScenarioExecutionStatus status)
        {
            if (status == ScenarioExecutionStatus.OK)
            {
                return TestResult.Passed;
            }
            else if (status == ScenarioExecutionStatus.TestError)
            {
                return TestResult.Failed;
            }

            return TestResult.Error;
        }


    }
}
