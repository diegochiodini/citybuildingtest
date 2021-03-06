﻿using Game.Abstractions;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Models
{
    public class SparseBuildingGridModel : ScriptableObject, IGridModel<BuildingModel>
    {
        private const string SavingFile = "buildings.json";

        [SerializeField]
        private int _rows;

        [SerializeField]
        private int _columns;

        private BuildingList _buildings;

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
                return _buildings.Value.Count;
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
            var found = _buildings.Value.Find((building) => building.Row == row && building.Column == column);
            Assert.IsNotNull(found, String.Format("Grid element [{0} : {1}] not found", row, column));
            return found.Element;
        }

        private void OnEnable()
        {
            string path = Application.persistentDataPath + "/" + SavingFile;
            _buildings = new BuildingList(path);
        }

        public void Set(int row, int column, BuildingModel data)
        {
            Assert.IsNotNull(data);
            BuildingElement element = new BuildingElement();
            element.Row = row;
            element.Column = column;
            element.Element = data;
            _buildings.Value.Add(element);

            if (ElementAdded != null)
            {
                ElementAdded(row, column, data);
            }

            _buildings.Save();
        }

        public GridElementModel<BuildingModel>[] FindAll(BuildingModel element)
        {
            var found = _buildings.Value.FindAll((e) => e.Element.Id == element.Id);
            return found.ToArray();
        }
    }
}