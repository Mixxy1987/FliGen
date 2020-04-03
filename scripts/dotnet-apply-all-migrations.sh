#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
DOTNET_RUN=./scripts/dotnet-apply-migrations.sh
PREFIX=FliGen
SERVICE=$PREFIX.Services
REPOSITORIES=(Leagues/$SERVICE.Leagues.Persistence Players/$SERVICE.Players.Persistence Seasons/$SERVICE.Seasons.Persistence Tours/$SERVICE.Tours.Persistence Teams/$SERVICE.Teams.Persistence)

for REPOSITORY in ${REPOSITORIES[*]}
do
	 echo ========================================================
	 echo Applying migrations in: $REPOSITORY
	 echo ========================================================
     cd ../Services/$REPOSITORY
     $DOTNET_RUN
     cd ../..
done