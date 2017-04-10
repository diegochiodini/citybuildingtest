using System;
using Game.Abstractions;
using UnityEngine;
using UnityEngine.UI;
using Game.Models;
using UnityEngine.Assertions;

[RequireComponent(typeof(Button))]
public class ColorTileView : AbstractTile<BuildingModel>
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Text _category;

    [SerializeField]
    private Text _name;

    private IGridModel<BuildingModel> _gridModel;

    private BuildingModel _model;
    public override BuildingModel Model
    {
        get
        {
            return _model;
        }

        set
        {
            _model = value;
            _image.color = _model.Color;
            _category.text = _model.Category.ToString();
            _name.text = _model.Name;
            if (_gridModel.FindAll(_model).Length >= _model.MaxNumber)
            {
                Button button = GetComponent<Button>();
                button.interactable = false;
            }
        }
    }

    private void Awake()
    {
        Assert.IsNotNull(_image);
        Assert.IsNotNull(_category);
        Assert.IsNotNull(_name);

        _gridModel = SharedModels.Get<IGridModel<BuildingModel>>();
    }

    public override void OnSelect()
    {
        Debug.LogFormat(this, "Tile {0} seletect", _model.Name);
        FireSelectionEvent();
    }
}