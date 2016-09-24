using Game.Abstractions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public struct GridElement
    {
        public int Row;
        public int Column;
        public BuildingModel Building;
    }

    public class SparseBuildingGridModel : ScriptableObject, IGridModel<BuildingModel>
    {
        [SerializeField]
        private int _rows;

        [SerializeField]
        private int _columns;

        private List<GridElement> _buildings;

        public int Rows
        {
            get
            {
                return _rows;
            }
        }

        public int Columns
        {
            get
            {
                return _columns;
            }
        }

        public int NumberOfTiles
        {
            get
            {
                return _buildings.Count;
            }
        }

        public int NumberOfTypes
        {
            get
            {
                return Enum.GetValues(typeof(BuildingType)).Length;
            }
        }

        public event Action<int, int, BuildingModel> ElementRemoved;
        public event Action<int, int, BuildingModel> ElementAdded;

        public BuildingModel Get(int row, int column)
        {
            try
            {
                var found = _buildings.Find((building) => building.Row == row && building.Column == column);
                return found.Building;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void Awake()
        {
            _buildings = new List<GridElement>();
        }

        public void Set(int row, int column, BuildingModel data)
        {
            GridElement element;
            element.Row = row;
            element.Column = column;
            element.Building = data;
            _buildings.Add(element);

            if (ElementAdded != null)
            {
                ElementAdded(row, column, data);
            }
        }
    }
}