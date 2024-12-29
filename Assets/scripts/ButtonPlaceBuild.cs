using UnityEngine;

public class ButtonPlaceBuild : MonoBehaviour
{
    public GameObject building;
    public void PlaceBuilding(){
        Instantiate(building, Vector3.zero, Quaternion.identity);
    }
}
