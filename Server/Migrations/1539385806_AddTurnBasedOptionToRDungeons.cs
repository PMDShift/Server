using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1539385806)]
    public class AddTurnBasedOptionToRDungeons : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("is_turn_based").OnTable("rdungeon").AsBoolean().NotNullable().WithDefaultValue(false);
        }
    }
}
