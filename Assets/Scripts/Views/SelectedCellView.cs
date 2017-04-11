using UnityEngine;
using Game.Models;

public class SelectedCellView : MonoBehaviour
{
    [SerializeField]
    private float _cellSize;

    protected BuildingModel ModelCache;

    public BuildingModel Model
    {
        get
        {
            return ModelCache;
        }

        set
        {
            if (ModelCache != value)
            {
                ModelCache = value;
                if (ModelCache == null)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                    AdjustGraphics();
                }
            }
        }
    }

    private void Awake()
    {
        AdjustGraphics();
    }

    private void AdjustGraphics()
    {
        float xUnit = 1f;
        float zUnit = 1f;

        if (Model != null)
        {
            xUnit = Model.Width;
            zUnit = Model.Height;
        }
        //Bear in mind that the quad is rotated along the X
        Vector3 scale = new Vector3(_cellSize * xUnit, _cellSize * zUnit, 1f);
        transform.localScale = scale;
    }
}