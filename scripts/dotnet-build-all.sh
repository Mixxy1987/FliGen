#!/bin/bash
export ASPNETCORE_ENVIRONMENT=docker
BUILD=./scripts/dotnet-build.sh
PREFIX=FliGen
SERVICE=$PREFIX.Services
REPOSITORIES=(Services/Tours/$SERVICE.Tours Services/GatewayApi/$SERVICE.Api Services/Players/$SERVICE.Players Services/Seasons/$SERVICE.Seasons Services/Leagues/$SERVICE.Leagues Services/Teams/$SERVICE.Teams)

for REPOSITORY in ${REPOSITORIES[*]}
do
	 echo ========================================================
	 echo Building a project: $REPOSITORY
	 echo ========================================================
     cd $REPOSITORY
     $BUILD
     cd ../../..
done