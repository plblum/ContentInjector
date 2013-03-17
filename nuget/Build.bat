REM Intended to be run from the nuget folder

nuget.exe update -self

cd ..\

C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe /nologo /maxcpucount /nr:true /verbosity:minimal /p:BuildInParallel=true /p:Configuration=Release /p:RestorePackages=true /t:Rebuild "ContentInjector for MVC.sln"

rd download /s /q

if not exist download mkdir download
if not exist download\package mkdir download\package
if not exist download\package\ContentInjector mkdir download\package\ContentInjector

if not exist download\package\ContentInjector\lib mkdir download\package\ContentInjector\lib
if not exist download\package\ContentInjector\lib\net40 mkdir download\package\ContentInjector\lib\net40

copy LICENSE.txt download
copy .\nuget\ReadMe.txt download\package
copy src\bin\Release\ContentInjector.* download\package\ContentInjector\lib\net40\

.\nuget\nuget.exe pack .\nuget\ContentInjector.nuspec -BasePath download\package\ContentInjector -o download

pause