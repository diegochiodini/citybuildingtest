using System;
using Game.Abstractions;
using UnityEngine;
using UnityEngine.UI;
using Game.Models;
using UnityEngine.Assertions;

public class ColorTileView : AbstractTile<BuildingModel>
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Text _category;

    [SerializeField]
    private Text _name;

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
        }
    }

    private void Awake()
    {
        Assert.IsNotNull(_image);
        Assert.IsNotNull(_category);
        Assert.IsNotNull(_name);
    }
 }