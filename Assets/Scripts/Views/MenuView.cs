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

    private void Awake()
    {
        Assert.IsNotNull(ElementTemplate);
        _toggleMenu = GetComponent<AbstractToggleMenu>();
    }

    private void Start()
    {
        CreateTiles();
    }

    private void OnDestroy()
    {
        ColorTileView[] tiles = _contentsArea.GetComponentsInChildren<ColorTileView>();
        foreach (var tile in tiles)
        {
            tile.TileSelected -= OnItemSelected;
            Destroy(tile.gameObject);
        }
    }

    private void CreateTiles()
    {
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