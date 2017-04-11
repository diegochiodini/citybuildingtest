using System;
using Game.Abstractions;
using UnityEngine;
using UnityEngine.UI;
using Game.Models;
using UnityEngine.Assertions;

[RequireComponent(typeof(Button))]
public class TileView : MonoBehaviour, ITileView
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Text _category;

    [SerializeField]
    private Text _name;

    public delegate bool CheckAvailabilityDelegate(IModel model);
    public CheckAvailabilityDelegate AvailabilityDelegate;

    private Button _button;

#region ITile
    public event Action<ITileView, GameObject> TileSelectedEvent;
    public event Action<ITileView, GameObject> TileEnableEvent;

    private BuildingModel _model;
    public IModel ContentModel
    {
        get
        {
            return _model;
        }

        set
        {
            _model = value as BuildingModel;
            Assert.IsNotNull(_model, "You must pass a BuildingModel");
            _image.color = _model.Color;
            _category.text = _model.Category.ToString();
            _name.text = _model.Name;

            UpdateAvailability();
        }
    }

    private bool _isSelected = false;
    public bool IsSelected
    {
        get { return _isSelected; }

        set
        {
            if (_isSelected == value || !_isEnabled)
            {
                return;
            }

            _isSelected = value;
            if (TileSelectedEvent != null)
            {
                TileSelectedEvent(this, gameObject);
            }
        }
    }

    private bool _isEnabled = true;
    public bool IsEnabled
    {
        get { return _isEnabled; }

        set
        {
            if (_isEnabled == value)
            {
                return;
            }

            _isEnabled = value;
            _button.interactable = _isEnabled;

            if (TileEnableEvent != null)
            {
                TileEnableEvent(this, gameObject);
            }
        }
    }

    public void SetPosition(int order)
    {
        throw new NotImplementedException();
    }
    #endregion

#region LifeCycle
    private void Awake()
    {
        Assert.IsNotNull(_image);
        Assert.IsNotNull(_category);
        Assert.IsNotNull(_name);

        _button = GetComponent<Button>();
        _button.interactable = _isEnabled;
        _button.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        UpdateAvailability();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }
#endregion

    private void UpdateAvailability()
    {
        if (AvailabilityDelegate != null)
        {
            IsEnabled = AvailabilityDelegate(_model);
        }
    }

    public void OnClick()
    {
        IsSelected = true;
    }
}