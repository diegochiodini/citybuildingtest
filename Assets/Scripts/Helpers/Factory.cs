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

        private IGridModel<BuildingModel> _gridModel;

        private void Awake()
        {
            Assert.IsNotNull(_gridView);
            _gridView.CreateBuilding = CreateBuilding;

            _gridModel = Locator.GetModel<GridModel>();
        }

        public void CreateBuilding(BuildingModel model, Vector2 gridPosition, Vector3 worldPosition)
        {
            GameObject building = Instantiate(model.Mesh, transform) as GameObject;
            Assert.IsNotNull(building, "Can't create building: " + model.Mesh.name);

            building.transform.position = worldPosition;

            _gridModel.Set((int)gridPosition.x, (int)gridPosition.y, model);
        }
    }
}