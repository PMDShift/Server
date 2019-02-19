using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("players")]
    [Migration(1550541487)]
    public class AddCostumeToRecruitData : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("Costume").OnTable("recruit_data").AsInt32().WithDefaultValue(-1);
        }
    }
}
