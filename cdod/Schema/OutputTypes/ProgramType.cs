using System.Text.Json;
using cdod.Models;
using cdod.Services;
using cdod.Services.DataLoaders;

namespace cdod.Schema.OutputTypes
{
    public class ProgramType
    {
        [IsProjected]
        public int Id { get; set; }

        public int Hours { get; set; }

        public string Name { get; set; }

        public string? ProgramFileUrl { get; set; }

        public string? Topics { get; set; }
    }
}
