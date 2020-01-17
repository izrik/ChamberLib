#!/bin/bash
set -e
nuget restore ChamberLib.sln
msbuild /p:Configuration=Debug ChamberLib.sln
mono ./packages/NUnit.ConsoleRunner.3.10.0/tools/nunit3-console.exe ./ChamberLibTests/bin/Debug/ChamberLibTests.dll
