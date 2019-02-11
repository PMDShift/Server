using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("players")]
    [Migration(1549845363)]
    public class AddMoneyToPlayers : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("Money").OnTable("characteristics").AsInt32();
            Create.Column("Savings").OnTable("characteristics").AsInt32();
        }
    }
}
