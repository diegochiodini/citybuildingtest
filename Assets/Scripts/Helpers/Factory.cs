using Game.Abstractions;
using Game.Models;
using UnityEngine;
using UnityEngine.Assertions;

using GridModel = Game.Abstractions.IGridModel<Game.Models.BuildingModel>;

namespace Game.Helpers
{
    public class Factory : MonoBehaviour
    {
        [SerializeField]
        private BuildingsGridView _gridView;

        [SerializeField]
        private SelectedCellView _landTemplate;

        private IGridModel<BuildingModel> _gridModel;

        private void Awake()
        {
            Assert.IsNotNull(_landTemplate);
            Assert.IsNotNull(_gridView);
            _gridView.CreateBuilding = CreateBuilding;

            _gridModel = ModelLocator.Get<GridModel>();
        }

        public void CreateBuilding(BuildingModel model, Vector2 gridPosition, Vector3 worldPosition)
        {
            GameObject building = Instantiate(model.Mesh, transform) as GameObject;
            Assert.IsNotNull(building, "Can't create building: " + model.Mesh.name);

            building.transform.position = worldPosition;

            SelectedCellView landCell = Instantiate<SelectedCellView>(_landTemplate);
            landCell.transform.position = worldPosition;
            landCell.Model = model;

            _gridModel.Set((int)gridPosition.x, (int)gridPosition.y, model);
        }
    }
}