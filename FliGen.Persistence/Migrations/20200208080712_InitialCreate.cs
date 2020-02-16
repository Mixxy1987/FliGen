﻿using System;
using System.Collections.Generic;
using FliGen.Persistence.Helper;
using Microsoft.EntityFrameworkCore.Migrations;
namespace FliGen.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(MigrationHelpers.GetDynamicSqlFromFile(@"SqlScripts/InitialFill.sql"));

            InitialFill.NamesAndRatesFill(migrationBuilder);
            InitialFill.LeaguePlayerLinksFill(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
