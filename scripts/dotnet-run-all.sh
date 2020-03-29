#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
DOTNET_RUN=./scripts/dotnet-run.sh
PREFIX=FliGen
SERVICE=$PREFIX.Services
REPOSITORIES=(Services/Tours/$SERVICE.Tours Services/GatewayApi/$SERVICE.Api)

for REPOSITORY in ${REPOSITORIES[*]}
do
	 echo ========================================================
	 echo Starting a service: $REPOSITORY
	 echo ========================================================
     cd $REPOSITORY
     $DOTNET_RUN &
     cd ../../..
done