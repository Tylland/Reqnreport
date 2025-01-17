# Introduction 
 Plugin for Reqnroll that produces documentation of the tests in form of markdown

# Usage

Add the interface IRequirementReportOutputHelper i the constructor to get access to a report

```
//Constructor
public AnyStepDefinitions(IReqnrollOutputHelper testOutput, IRequirementReportOutputHelper requirementReport)
{
    _requirementReport = requirementReport ?? throw new ArgumentNullException(nameof(requirementReport));
}
```


# Example attachement
## Add json as an attachement
```
    _requirementReport.AddContent("Indata", json, "json");
```

## Add log as an attachement
```
    RequirementReport.AddLog("Log");
```

# Development
Pull Requests are appreciated :-)
Source code lives in repository: [http://tfs:8080/tfs/trv/TRVHub/_git/Strateg.Reqnreport](https://github.com/Tylland/Reqnreport)
