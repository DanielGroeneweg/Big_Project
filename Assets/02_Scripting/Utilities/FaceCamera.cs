using UnityEngine;
public class FaceCamera : MonoBehaviour
{
    private void Update()
    {
        if (Camera.main != null)
        transform.LookAt(Camera.main.transform);
    }
}