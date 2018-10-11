using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1539233451)]
    public class AddGameplayModeToMaps : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("gameplay_mode").OnTable("map_data").AsInt32().WithDefaultValue(0);
        }
    }
}
