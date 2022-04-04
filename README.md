# Spiracle.NETCore

Spiracle.NETCore is an insecure web application used to test system security controls.  
The application is vulnerable to numerous vulnerabilities such as SQL Injection (CWE-89) and XSS (CWE-79).  
Due to its insecure design, this application should NOT be deployed on an unsecured network or system.


### To get up and running;

```
$ git clone https://github.com/waratek/Spiracle.NETCore.git
$ cd Spiracle.NETCore/
$ dotnet run
```

Home page: http://localhost:5000/


Since Monday, 4 April 2022, Spiracle.NETCore/Spiracle.NETCore.csproj is configured to run with .NET 6.0 by default.  
To run Spiracle.NETCore on another .NET Core version, edit TargetFramework in Spiracle.NETCore.csproj accordingly.  

