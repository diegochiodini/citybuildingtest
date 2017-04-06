using UnityEngine;

namespace Game.Models
{
    public enum BuildingCategory
    {
        Unique,
        Common,
    }

    public class BuildingModel :ScriptableObject, IModel
    {
        public int Id;
        public string Name = "Name";
        public BuildingCategory Category = BuildingCategory.Unique;
        public int MaxNumber = 1;
        public int Width = 1;
        public int Height = 1;
        public Color Color;
        public GameObject Mesh;
    }
}