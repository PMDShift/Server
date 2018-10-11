using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1539214653)]
    public class AddAdditionalMapLayers : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("Mask3").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Mask3Anim").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Mask4").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Mask4Anim").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Mask5").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Mask5Anim").OnTable("map_tiles").AsInt32().WithDefaultValue(0);

            Create.Column("Mask3Tileset").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Mask3AnimTileset").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Mask4Tileset").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Mask4AnimTileset").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Mask5Tileset").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Mask5AnimTileset").OnTable("map_tiles").AsInt32().WithDefaultValue(0);

            Create.Column("Fringe3").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Fringe3Anim").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Fringe4").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Fringe4Anim").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Fringe5").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Fringe5Anim").OnTable("map_tiles").AsInt32().WithDefaultValue(0);

            Create.Column("Fringe3Tileset").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Fringe3AnimTileset").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Fringe4Tileset").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Fringe4AnimTileset").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Fringe5Tileset").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
            Create.Column("Fringe5AnimTileset").OnTable("map_tiles").AsInt32().WithDefaultValue(0);
        }
    }
}
