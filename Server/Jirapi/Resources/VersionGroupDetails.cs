using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Jirapi.Resources
{
    public class VersionGroupDetails
    {
        [JsonProperty("level_learned_at")]
        public int LevelLearnedAt { get; set; }

        [JsonProperty("version_group")]
        public VersionGroup VersionGroup { get; set; }

        [JsonProperty("move_learn_method")]
        public MoveLearnMethod MoveLearnMethod { get; set; }
    }
}
