# コンパイル構文
```
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./bin/Result/YYYYMMDD_HHMMSS/NUnitTestResult.xml
reportgenerator -reports:./bin/Result/YYYYMMDD_HHMMSS/NUnitTestResult.xml -targetdir:./bin/Result/YYYYMMDD_HHMMSS -reporttypes:Html
```
