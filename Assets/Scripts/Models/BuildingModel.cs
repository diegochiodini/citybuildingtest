using UnityEngine;

namespace Game.Models
{
    public enum BuildingType
    {
        Unique0,
        Unique1,
        Unique2,
        Unique3,
        Unique4,
        Unique5,
        Unique6,
        Unique7,
        Unique8,
        Unique9,
        Unique10,
        Unique11,
        Unique12,
        Unique13,
        Unique14,
        Common0,
        Common1,
        Common2,
        Common3,
        Common4,
    }

    public class BuildingModel :ScriptableObject
    {
        public int Width;
        public int Height;
        public BuildingType Type;
        public GameObject Mesh;
    }
}