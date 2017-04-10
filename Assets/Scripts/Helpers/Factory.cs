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

            //A model could also be specified directly as ScriptableObject variable of a Monobehaviour. 
            //Just remember to cast it to the right interface.
            _gridModel = SharedModels.Get<GridModel>();
        }

        public void CreateBuilding(BuildingModel model, Vector2 gridPosition, Vector3 worldPosition)
        {
            GameObject building = Instantiate(model.Mesh, transform) as GameObject;
            Assert.IsNotNull(building, "Can't create building: " + model.Mesh.name);

            building.transform.position = worldPosition;

            SelectedCellView landCell = Instantiate<SelectedCellView>(_landTemplate, building.transform);
            landCell.Model = model;

            _gridModel.Set((int)gridPosition.x, (int)gridPosition.y, model);
        }
    }
}