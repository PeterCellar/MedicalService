# Medical Service
- Grpc service implemented in ASP.NET Core
- Kestrel server is configured 
- Downloads data from https://covid19.who.int/data
- Implements rpc messages that process downloaded data and sends them to a client
- Filters data by filtering parameters from client's rpc request

## Dependencies
- Setting up of the HTTPS development certificate 
- Tooling package Grpc.Tools https://www.nuget.org/packages/Grpc.Tools/
- Package CsvHelper

## Project specificities
- File Greet.cs adjustments: 
    - Classes *DataFilter* and *MetadataFilter* each has to implement custom *IFilter* interface. Instances of that classes are used as types in generic method *FilterVaccinationData* \[MedicalService.Filtering.DataFiltering\]

## Build and Run
You can build service by following command:
```pwsh
> dotnet build
```

or build and run by command:
```pwsh
> dotnet run
```
