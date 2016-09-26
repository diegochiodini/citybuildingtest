using Game.Models;
using UnityEngine;

public class SharedModel : MonoBehaviour
{
    public Observable<BuildingModel> SelectedBuilding = new Observable<BuildingModel>(null);
}