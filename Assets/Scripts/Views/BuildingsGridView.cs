using Game.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using Game.Abstractions;

using SelectedCell = Game.Abstractions.AbstractSelectionCell<Game.Models.BuildingModel>;

public class BuildingsGridView : MonoBehaviour
{
    [SerializeField]
    private  GameObject _selectedCellObject;

    [SerializeField]
    private Vector2 _cellSize;

    [SerializeField]
    private PhysicsRaycaster _raycaster;

    [SerializeField]
    private float _mouseSensivity = 1f;

    private List<RaycastResult> _results = new List<RaycastResult>();
    private IGridModel<BuildingModel> _gridModel;
    private SharedDataModel _sharedData;
    private SelectedCell _selectedCell;

    private Vector3 _scaledCell;
    private Vector3 _scaledOffset;
    private Vector3 _lastMousePosition;

    public System.Action<BuildingModel, Vector2, Vector3> CreateBuilding;

    #region Life cycle
    private void Awake()
    {
        Assert.IsNotNull(_raycaster, "You must specify a raycaster");
        Assert.IsNotNull(_selectedCellObject, "You must provide an object to highlight the landing place of the building");

        _selectedCell = _selectedCellObject.GetComponent<SelectedCell>();

        _sharedData = ModelLocator.GetWriteableModel<SharedDataModel>();
        _sharedData.SelectedBuilding.OnChange += OnSelectedBuildingChange;

        _gridModel = ModelLocator.Get<IGridModel<BuildingModel>>();

        _scaledCell = new Vector3(
            (_cellSize.x * transform.localScale.x) / _gridModel.Rows,
            0f,
            (_cellSize.y * transform.localScale.z) / _gridModel.Columns);

        _scaledOffset = new Vector3(
            (_cellSize.x / 2f) * transform.localScale.x,
            0f,
            (_cellSize.y / 2f) * transform.localScale.z);
    }

    private void FixedUpdate()
    {
        if (_sharedData.SelectedBuilding.Value == null)
        {
            return;
        }

        if ((Input.mousePosition - _lastMousePosition).sqrMagnitude > _mouseSensivity)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                SnapSelectedCellOnGrid(hitInfo.point);
            }
            _lastMousePosition = Input.mousePosition;
        }
    }
    #endregion

    #region Grid helpers

    private Vector2 GetWorldToGridPosition(Vector3 worldPosition)
    {
        Vector2 coordinates = new Vector2();

        Vector3 localPosition = _scaledOffset - (transform.position - worldPosition);
        coordinates.x = _gridModel.Rows - (localPosition.z / _scaledCell.z);
        coordinates.y = (localPosition.x / _scaledCell.x);
        return coordinates;
    }

    private Vector3 GetGridToWorldPosition(int row, int column)
    {
        Vector3 coordinates = Vector3.zero;

        coordinates.x = (column * _scaledCell.x) - _scaledOffset.x;
        coordinates.z = (-row * _scaledCell.z) + _scaledOffset.z;

        float xUnit = 1f;
        float zUnit = 1f;
        BuildingModel building = _sharedData.SelectedBuilding.Value;
        if (building != null)
        {
            xUnit = building.Width;
            zUnit = building.Height;
        }
        Vector3 unitOffset = new Vector3(xUnit * _scaledCell.x * 0.5f, 0f, -zUnit * _scaledCell.z * 0.5f);
        coordinates += unitOffset;

        return coordinates;
    }

    private void SnapSelectedCellOnGrid(Vector3 worldPosition)
    {
        Vector2 coordinates = GetWorldToGridPosition(worldPosition);
        _selectedCell.transform.position = GetGridToWorldPosition((int)coordinates.x, (int)coordinates.y);
    }
    #endregion

    #region Events

    //This is a good example to highlight the difference between prototype and production code. See object Root/Grid.
    public void OnCLick(BaseEventData data)
    {
        _results.Clear();
        _raycaster.Raycast(data as PointerEventData, _results);
        foreach (var hit in _results)
        {
            Vector2 coordinates = GetWorldToGridPosition(hit.worldPosition);
            BuildingModel buildingModel = _sharedData.SelectedBuilding.Value;
            if (CreateBuilding != null)
            {
                CreateBuilding(buildingModel, coordinates, _selectedCell.transform.position);
            }
            _sharedData.SelectedBuilding.Value = null;
            Debug.LogFormat("{0}, {1}", (int)coordinates.x, (int)coordinates.y);
        }
    }

    private void OnSelectedBuildingChange(BuildingModel model)
    {
        Debug.Log("SelectedBuilding has changed: " + _sharedData.SelectedBuilding.Value);
        _selectedCell.Model = model;
    }
    #endregion
}