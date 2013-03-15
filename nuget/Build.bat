REM Intended to be run from the nuget folder

nuget.exe update -self

cd ..\

C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe /nologo /maxcpucount /nr:true /verbosity:minimal /p:BuildInParallel=true /p:Configuration=Release /p:RestorePackages=true /t:Rebuild "InsertionsManager for MVC.sln"

rd download /s /q

if not exist download mkdir download
if not exist download\package mkdir download\package
if not exist download\package\InsertionsManager mkdir download\package\InsertionsManager

if not exist download\package\InsertionsManager\lib mkdir download\package\InsertionsManager\lib
if not exist download\package\InsertionsManager\lib\net40 mkdir download\package\InsertionsManager\lib\net40

copy LICENSE.txt download
copy ReadMe.txt download
copy src\bin\Release\InsertionsManager.* download\package\InsertionsManager\lib\net40\

.\nuget\nuget.exe pack .\nuget\InsertionsManager.nuspec -BasePath download\package\InsertionsManager -o download

pause