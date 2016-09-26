using Game.Abstractions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using GridElement = Game.Abstractions.GridElement<Game.Models.BuildingModel>;

namespace Game.Models
{
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
                throw new System.NotImplementedException();
            }
        }

        public event Action<int, int, BuildingModel> ElementRemoved;
        public event Action<int, int, BuildingModel> ElementAdded;

        public BuildingModel Get(int row, int column)
        {
            try
            {
                var found = _buildings.Find((building) => building.Row == row && building.Column == column);
                return found.Element;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void OnEnable()
        {
            _buildings = new List<GridElement>();
        }

        public void Set(int row, int column, BuildingModel data)
        {
            Assert.IsNotNull(data);
            GridElement element = new GridElement();
            element.Row = row;
            element.Column = column;
            element.Element = data;
            _buildings.Add(element);

            if (ElementAdded != null)
            {
                ElementAdded(row, column, data);
            }
        }

        public GridElement<BuildingModel>[] FindAll(BuildingModel element)
        {
            var found = _buildings.FindAll((e) => e.Element.Id == element.Id);
            return found.ToArray();
        }
    }
}