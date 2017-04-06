using Game.Models;
using UnityEngine;

public class SharedDataModel : MonoBehaviour
{
    public Observable<BuildingModel> SelectedBuilding = new Observable<BuildingModel>(null);
}