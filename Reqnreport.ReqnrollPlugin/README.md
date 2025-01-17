# Introduktion 
 Plugin till Reqnroll som producerar dokumentation av testerna i form av markdown

# Användning

Lägg till interfacet IRequirementReportOutputHelper i konstruktorn för att få tillgång till en report

```
//Constructor
public AnyStepDefinitions(IReqnrollOutputHelper testOutput, IRequirementReportOutputHelper requirementReport)
{
    _requirementReport = requirementReport ?? throw new ArgumentNullException(nameof(requirementReport));
}
```


# Exempel bilagar
## Lägg till json som bilaga
```
    _requirementReport.AddContent("Inläst OperativFärdplan", json, "json");
```

## Lägg till loggar som bilaga
```
    RequirementReport.AddLog("Log");
```

# Utveckling
Synpunkter i form av tillägg och förbättringar tas gärna emot som PRs :-)
Källkoden finns i detta repository: [http://tfs:8080/tfs/trv/TRVHub/_git/Strateg.Reqnreport](http://tfs:8080/tfs/trv/TRVHub/_git/Strateg.Reqnreport) 

## Bygga nuget
Kör kommandot nedan katalogen för projektet Reqnreport.ReqnrollPlugin
```
dotnet pack
```

