using System;
using Game.Abstractions;
using UnityEngine;

namespace Game.Models
{
    public class GridModel : ScriptableObject, IGridModel<int>
    {
        public event Action<int, int, int> TileRemovedEvent;

        [SerializeField]
        private int _rows;

        public int Rows
        {
            get
            {
                return _rows;
            }
        }

        [SerializeField]
        private int _columns;

        public int Columns
        {
            get
            {
                return _columns;
            }
        }

        [SerializeField]
        private int _numberOfTypes = 1;

        public int NumberOfTypes
        {
            get
            {
                return _numberOfTypes;
            }
        }

        public int NumberOfTiles
        {
            get
            {
                if (_tiles == null)
                {
                    return 0;
                }
                return _tiles.Length;
            }
        }

        private int[] _tiles;

        void OnEnable()
        {
            CreateRandomTiles();
        }

        private void CreateRandomTiles()
        {
            _tiles = new int[_columns * _rows];

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    _tiles[IndexOf(i, j)] = UnityEngine.Random.Range(0, _numberOfTypes);
                }
            }
        }

        private int IndexOf(int row, int column)
        {
            return (row * _columns) + column;
        }

        private void RemoveAt(int row, int column)
        {
            int index = IndexOf(row, column);
            if (TileRemovedEvent != null)
            {
                TileRemovedEvent(row, column, _tiles[index]);
            }
            _tiles[index] = -1;
        }

        public int Get(int row, int column)
        {
            return _tiles[IndexOf(row, column)];
        }
    }
}