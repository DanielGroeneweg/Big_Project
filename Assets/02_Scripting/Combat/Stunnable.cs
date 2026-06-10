using UnityEngine;

public class Stunnable : MonoBehaviour
{
    private StatesData data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        data = GetComponent<StatesData>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Gnome"))
        {
            data.isStunned = true;
        }
    }
}
