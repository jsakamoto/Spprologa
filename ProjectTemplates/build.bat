@echo off
cd /d %~dp0
.\.bin\nuget.exe pack Spprologa.Templates.nuspec -OutputDirectory ..\_dist