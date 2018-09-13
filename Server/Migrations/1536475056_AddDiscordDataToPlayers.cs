using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Migrations
{
    [Tags("players")]
    [Migration(1536475056)]
    public class AddDiscordDataToPlayers : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("CanLinkDiscord").OnTable("characteristics").AsBoolean();
            Create.Column("LinkedDiscordId").OnTable("characteristics").AsInt64();
        }
    }
}
