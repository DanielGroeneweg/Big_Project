using UnityEngine;
public abstract class Presenter : MonoBehaviour
{
    public abstract void Present(float min, float max, float current);
}