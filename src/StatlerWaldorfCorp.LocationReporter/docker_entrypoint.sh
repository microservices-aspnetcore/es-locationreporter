#!/bin/bash
cd /pipeline/source/app/publish
echo "starting"

dotnet StatlerWaldorfCorp.LocationReporter.dll --server.urls=http://0.0.0.0:${PORT-"8080"}