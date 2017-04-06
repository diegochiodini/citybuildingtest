using Game.Helpers;
using System.Collections.Generic;

namespace Game.Models
{
    [System.Serializable]
    class BuildingList : Serializer<List<BuildingElement>>
    {
        public BuildingList(string path) : base(path, new List<BuildingElement>()) { }
    }
}