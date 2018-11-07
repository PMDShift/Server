using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1541509967)]
    public class AddAdditionalAttackSaysToNPCs : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("attack_say_2").OnTable("npc").AsString().Nullable();
            Create.Column("attack_say_3").OnTable("npc").AsString().Nullable();
        }
    }
}
