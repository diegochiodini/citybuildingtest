using UnityEngine;

public class SharedModel : MonoBehaviour
{
    public Observable<int> SelectedBuilding = new Observable<int>(int.MinValue);
}