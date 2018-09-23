using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Jirapi.Resources
{
    public class PokemonStat
    {
        [JsonProperty("base_stat")]
        public int BaseStat { get; set; }
        public int Effort { get; set; }
        public NamedApiResource<Stat> Stat { get; set; }
    }
}
