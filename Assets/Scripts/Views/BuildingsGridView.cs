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
    private SparseBuildingGridModel _gridModel;
    private SharedModel _sharedModel;
    private SelectedCell _selectedCell;

    private Vector3 _scaledCell;
    private Vector3 _scaledOffset;
    private Vector3 _lastMousePosition;

    #region Life cycle
    private void Awake()
    {
        Assert.IsNotNull(_raycaster, "You must specify a raycaster");
        Assert.IsNotNull(_selectedCellObject, "You must provide an object to highlight the landing place of the building");

        _selectedCell = _selectedCellObject.GetComponent<SelectedCell>();

        _sharedModel = Locator.GetWriteableModel<SharedModel>();
        _sharedModel.SelectedBuilding.OnChange += OnSelectedBuildingChange;

        _gridModel = Locator.GetModel<SparseBuildingGridModel>();

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
        if (_sharedModel.SelectedBuilding.Value == null)
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

        coordinates.x = (column * _scaledCell.x) - _scaledOffset.x + (_scaledCell.x * 0.5f);
        coordinates.z = (-row * _scaledCell.z) + _scaledOffset.z - (_scaledCell.z * 0.5f);

        return coordinates;
    }

    private void SnapSelectedCellOnGrid(Vector3 worldPosition)
    {
        Vector2 coordinates = GetWorldToGridPosition(worldPosition);
        _selectedCell.transform.position = GetGridToWorldPosition((int)coordinates.x, (int)coordinates.y);
    }
    #endregion

    #region Events

    public void OnCLick(BaseEventData data)
    {
        _results.Clear();
        _raycaster.Raycast(data as PointerEventData, _results);
        foreach (var hit in _results)
        {
            Vector2 coordinates = GetWorldToGridPosition(hit.worldPosition);
            Debug.LogFormat("{0}, {1}", (int)coordinates.x, (int)coordinates.y);
            _sharedModel.SelectedBuilding.Value = null;
        }
    }

    private void OnSelectedBuildingChange(BuildingModel model)
    {
        Debug.Log("SelectedBuilding has changed: " + _sharedModel.SelectedBuilding.Value);
        _selectedCell.Model = model;
    }
    #endregion

    #region Editor

    private void OnDrawGizmos()
    {
        int rows = 20; int columns = 20;
        Gizmos.color = Color.red;
        Vector3 scaledSize = new Vector3(
            (_cellSize.x * transform.localScale.x) / rows,
            0f,
            (_cellSize.y * transform.localScale.z) / columns);

        Vector3 offset = new Vector3(
            (_cellSize.x / 2f) * transform.localScale.x,
            0f,
            (_cellSize.y / 2f) * transform.localScale.z);

        for (int i = 0; i < rows + 1; i++) //rows
        {
            float zRow = i * scaledSize.z - offset.z;
            Vector3 startRow = new Vector3(-offset.x, 0f, zRow);
            Vector3 endRow = new Vector3(offset.x, 0f, zRow);
            Gizmos.DrawLine(startRow, endRow);

            for (int j = 0; j < columns + 1; j++) //columns
            {
                float xCol = j * scaledSize.x - offset.x;
                Vector3 startCol = new Vector3(xCol, 0f, -offset.z);
                Vector3 endCol = new Vector3(xCol, 0f, offset.z);
                Gizmos.DrawLine(startCol, endCol);
            }
        }
    }

    #endregion Editor
}