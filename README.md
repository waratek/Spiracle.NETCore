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


Spiracle.NETCore/Spiracle.NETCore.csproj is configurred to run with .Net 5.0 by default.  
To run Spiracle.NETCore on another.Net Core version, edit TargetFrameowrk in Spiracle.NETCore.csproj accordingly.  

