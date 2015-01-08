#!/bin/bash
VersionNumber="0.0.${TRAVIS_BUILD_NUMBER}"

echo "VersionNumber: $VersionNumber"

if [ "$TRAVIS_BRANCH" == "master" ]; then         
     mono --runtime=v4.0.30319 .nuget/NuGet.exe Pack NSquared.MvcExtensions.nuspec -NonInteractive -Version ${VersionNumber} 
     mono --runtime=v4.0.30319 .nuget/NuGet.exe Push NSquared.MvcExtensions.${VersionNumber}.nupkg $NUGET_APIKEY -NonInteractive     
fi
