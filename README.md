# CLOProject
InMemory를 사용하여 따로 Persistence Layer 설정이 필요 없습니다.  
따로 실행 모드 및 환경에 대한 설명이 없어 다음 환경에서 실행 가능합니다.
- Visual Studio 2022
    - Configuration: Debug
    - Platform: Any CPU

실행 프로젝트(Host)는 5000번 포트(Http)를 사용합니다.  
호스트에서 5000번 포트를 사용중이라면 실행 프로젝트(Host) `appsettings.Development.json`에서 Kestrel.Ednpoints.Http.Url 포트를 변경하십시오.
```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:5000"
      }
    }
  }
}

```
## :point_right: Solution Directories with CleanArchitecture
```
.
├── CLOProject.sln
├── LICENSE
├── README.md
├── src
│   ├── Application
│   ├── Domain
│   ├── Host (Launch project)
│   └── Infrastructure
└── tests
    └── Application.UnitTests
```
### Uses Nuget Packages to
1. AutoMapper
    - AutoMapper.Extensions.Microsoft.DependencyInjection
3. CsvHelper
4. FluentValidation
    - FluentValidation.DependencyInjectionExtensions
5. MediatR
    - MediatR.Extensions.Microsoft.DependencyInjection
6. Microsoft.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore.InMemory
7. NUnit
    - NUnit.Analyzers
    - NUnit3TestAdapter

