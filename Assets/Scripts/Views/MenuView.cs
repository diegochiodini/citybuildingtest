using Game.Abstractions;
using Game.Models;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(AbstractToggleMenu))]
public class MenuView : AbstractSelectionMenu<ColorTileView, BuildingModel>
{
    [SerializeField]
    private RectTransform _contentsArea;

    private AbstractToggleMenu _toggleMenu;
    private IGridModel<BuildingModel> _gridModel;

    private void Awake()
    {
        Assert.IsNotNull(ElementTemplate);
        _toggleMenu = GetComponent<AbstractToggleMenu>();
        _gridModel = Locator.GetModel<IGridModel<BuildingModel>>();
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

        foreach (Transform child in _contentsArea.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void RefreshTiles()
    {
        foreach (Transform child in _contentsArea.transform)
        {
            Destroy(child.gameObject);
        }

        BuildingModel[] models = Locator.GetModels<BuildingModel>();
        foreach (var model in models)
        {
            var tile = Instantiate<ColorTileView>(ElementTemplate);
            tile.transform.SetParent(_contentsArea, false);
            tile.Model = model;
            tile.TileSelected += OnItemSelected;
        }
    }

    protected override void OnItemSelected(BuildingModel data)
    {
        _toggleMenu.ToggleMenu();
        Locator.GetWriteableModel<SharedModel>().SelectedBuilding.Value = data;
    }
}