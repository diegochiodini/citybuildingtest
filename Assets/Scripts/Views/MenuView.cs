using DG.Tweening;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    bool _isOpen = false;
    public void ToggleMenu()
    {
        transform.DOLocalMoveY(_isOpen ? 0f : 100f, 0.15f);
        _isOpen = !_isOpen;
    }
}