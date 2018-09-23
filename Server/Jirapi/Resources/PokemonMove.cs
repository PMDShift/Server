using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Jirapi.Resources
{
    public class PokemonMove
    {
        public NamedApiResource<Move> Move { get; set; }

        [JsonProperty("version_group_details")]
        public List<VersionGroupDetails> VersionGroupDetails { get; set; }
    }
}
