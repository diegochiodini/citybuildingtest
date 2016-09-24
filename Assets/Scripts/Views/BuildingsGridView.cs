using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class BuildingsGridView : MonoBehaviour
{
    [SerializeField]
    private PhysicsRaycaster _raycaster;

    private List<RaycastResult> _results = new List<RaycastResult>();

    private void Awake()
    {
        Assert.IsNotNull(_raycaster, "You must specify a raycaster");
    }

    public void OnCLick(BaseEventData data)
    {
        _results.Clear();
        _raycaster.Raycast(data as PointerEventData, _results);
        foreach (var hit in _results)
        {
            Debug.Log(hit.worldPosition);
        }
    }
}