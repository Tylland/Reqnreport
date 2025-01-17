using Reqnroll;
using Reqnroll.UnitTestProvider;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin;

public class RequirementReportTestRunner : ITestRunner
{
    private readonly IUnitTestRuntimeProvider _unitTestRuntimeApi;
    private readonly ITestRunnerManager _runnerManager;
    private readonly ITestRunner _underlyingRunner;

    public string TestWorkerId => _underlyingRunner.TestWorkerId;

    public FeatureContext FeatureContext =>
        _underlyingRunner.FeatureContext;

    public ScenarioContext ScenarioContext =>
        _underlyingRunner.ScenarioContext;

    public ITestThreadContext TestThreadContext =>
        _underlyingRunner.TestThreadContext;


    public RequirementReportTestRunner(
        IUnitTestRuntimeProvider unitTestRuntimeApi,
        ITestRunnerManager runnerManager,
        ITestRunner underlyingRunner
    )
    {
        _unitTestRuntimeApi = unitTestRuntimeApi;
        _runnerManager = runnerManager;
        _underlyingRunner = underlyingRunner;
    }

    public void InitializeTestRunner(string testWorkerId) =>
        _underlyingRunner.InitializeTestRunner(testWorkerId);

    public async Task OnTestRunStartAsync() =>
        await _underlyingRunner.OnTestRunStartAsync();

    public async Task OnTestRunEndAsync() =>
        await _underlyingRunner.OnTestRunEndAsync();

    public async Task OnFeatureStartAsync(FeatureInfo featureInfo) =>
        await _underlyingRunner.OnFeatureStartAsync(featureInfo);

    public async Task OnFeatureEndAsync() =>
        await _underlyingRunner.OnFeatureEndAsync();

    public void OnScenarioInitialize(ScenarioInfo scenarioInfo)
    {
        _underlyingRunner.OnScenarioInitialize(scenarioInfo);
    }

    public async Task OnScenarioStartAsync()
    {
        await _underlyingRunner.OnScenarioStartAsync();
    }

    public async Task CollectScenarioErrorsAsync() =>
        await _underlyingRunner.CollectScenarioErrorsAsync();

    public async Task OnScenarioEndAsync() =>
        await _underlyingRunner.OnScenarioEndAsync();

    public void SkipScenario() => _underlyingRunner.SkipScenario();

    public async Task GivenAsync(string text, string multilineTextArg, Table tableArg, string? keyword = null) =>
        await _underlyingRunner.GivenAsync(text, multilineTextArg, tableArg, keyword);

    public async Task WhenAsync(string text, string multilineTextArg, Table tableArg, string? keyword = null) =>
        await _underlyingRunner.WhenAsync(text, multilineTextArg, tableArg, keyword);

    public async Task ThenAsync(string text, string multilineTextArg, Table tableArg, string? keyword = null) =>
        await _underlyingRunner.ThenAsync(text, multilineTextArg, tableArg, keyword);

    public async Task AndAsync(string text, string multilineTextArg, Table tableArg, string? keyword = null) =>
        await _underlyingRunner.AndAsync(text, multilineTextArg, tableArg, keyword);

    public async Task ButAsync(string text, string multilineTextArg, Table tableArg, string? keyword = null) =>
        await _underlyingRunner.ButAsync(text, multilineTextArg, tableArg, keyword);

    public void Pending() => _underlyingRunner.Pending();
}