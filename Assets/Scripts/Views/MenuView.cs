using Game.Abstractions;
using Game.Models;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(IToggleMenu))]
public class MenuView : AbstractSelectionMenu<TileView, BuildingModel>
{
    [SerializeField]
    private RectTransform _contentArea;

    private IToggleMenu _toggleMenu;
    private IGridModel<BuildingModel> _gridModel;

    private void Awake()
    {
        Assert.IsNotNull(ElementTemplate);
        _toggleMenu = GetComponent<IToggleMenu>();
        _gridModel = SharedModels.Get<IGridModel<BuildingModel>>();
        _gridModel.ElementAdded += OnElementAdded;
    }

    private void OnElementAdded(int row, int column, BuildingModel model)
    {
        RefreshTiles();
    }

    private void Start()
    {
        RefreshTiles();
    }

    private void OnDestroy()
    {
        _gridModel.ElementAdded -= OnElementAdded;

        foreach (Transform child in _contentArea.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void RefreshTiles()
    {
        foreach (Transform child in _contentArea.transform)
        {
            Destroy(child.gameObject);
        }

        BuildingModel[] models = SharedModels.GetModels<BuildingModel>();
        foreach (var model in models)
        {
            var asset = Instantiate(ElementTemplate);
            var tile = asset.GetComponent<TileView>();
            Assert.IsNotNull(tile, "ElementTemplate does not contain a ColorTileView script");
            tile.transform.SetParent(_contentArea, false);
            tile.ContentModel = model;
            tile.TileSelectedEvent += OnItemSelected;
            tile.TileEnableEvent += (itile, go) => Debug.Log(go.name + " enabled: " + itile.IsEnabled);
            tile.AvailabilityDelegate = (tileModel) =>
            {
                BuildingModel buildingModel = tileModel as BuildingModel;
                int buildingsNumber = _gridModel.FindAll(buildingModel).Length;
                return buildingsNumber < buildingModel.MaxNumber;
            };
        }
    }

    protected void OnItemSelected(ITile tile, GameObject tileGameObject)
    {
        BuildingModel data = tile.ContentModel as BuildingModel;
        _toggleMenu.ToggleMenu();
        SharedModels.GetWriteableModel<SharedDataModel>().SelectedBuilding.Value = data;
    }
}