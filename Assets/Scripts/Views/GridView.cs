using Game.Abstractions;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Views
{
    public class GridView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _template;

        [SerializeField]
        private Vector2 _cellSize;

        private IGridModel<int> _model;
        private AbstractTile[] _tiles;

        private void Awake()
        {
            Assert.IsNotNull(_template, "You must specify a prefab template");
            _model = Locator.GetModel<IGridModel<int>>();
        }

        private void Start()
        {
            //Access models after Start so we are sure they are initialised.
            _tiles = new AbstractTile[_model.NumberOfTiles];

            Locator.PrintAssets();

            int index = 0;
            for (int i = 0; i < _model.Rows; i++)
            {
                for (int j = 0; j < _model.Columns; j++)
                {
                    GameObject tileObject = Instantiate(_template, transform) as GameObject;
                    AbstractTile tile = tileObject.GetComponent<AbstractTile>();
                    tile.Init(_model.Get(i, j));
                    tile.transform.localPosition = LocalPositionOf(i, j);
                    _tiles[index] = tile;
                    index++;
                }
            }
        }

        private Vector3 LocalPositionOf(int row, int column)
        {
            return new Vector3(column * _cellSize.x, row * _cellSize.y, 0f);
        }
    }
}