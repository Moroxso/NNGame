using UnityEngine;

public class ObjestStatus : MonoBehaviour
{
    [SerializeField] GameObject targetObject;

    public void OnStatus()
    {
        targetObject.SetActive(true);
    }

    public void OffStatus()
    {
        targetObject.SetActive(false);
    }



}
