using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Models;

public class MenuView : MonoBehaviour
{
    [SerializeField]
    private ColorTileView _tileTemplate;

    [SerializeField]
    private RectTransform _scrollableArea;

    [SerializeField]
    private RectTransform _contentsArea;

    private bool _isOpen = false;

    private void Awake()
    {
        Assert.IsNotNull(_tileTemplate);
        Assert.IsNotNull(_scrollableArea);

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
            tile.TileSelected -= OnTileSelected;
            Destroy(tile.gameObject);
        }
    }

    private void CreateTiles()
    {
        BuildingModel[] models = Locator.GetModels<BuildingModel>();
        foreach (var model in models)
        {
            var tile = Instantiate<ColorTileView>(_tileTemplate);
            tile.transform.SetParent(_contentsArea, false);
            tile.Model = model;
            tile.TileSelected += OnTileSelected;
        }
    }

    private void OnTileSelected(BuildingModel buildingModel)
    {
        ToggleMenu();
        Locator.GetWriteableModel<SharedModel>().SelectedBuilding.Value = buildingModel.Id;
    }

    public void ToggleMenu()
    {
        transform.DOLocalMoveY(_isOpen ? 0f : _scrollableArea.rect.height, 0.15f);
        _isOpen = !_isOpen;
    }
}