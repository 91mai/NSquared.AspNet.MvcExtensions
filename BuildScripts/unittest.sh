#!/bin/bash

mono --runtime=v4.0.30319 .nuget/NuGet.exe install .nuget/packages.config
mono --runtime=v4.0.30319 packages/SpecRun.Runner.1.2.0/tools/SpecRun.exe run NSquared.MvcExtensions.Test.dll /baseFolder:MvcExtensions.Test/bin/Debug /toolIntegration:vs2010 /reportFile:.\TestReport.html