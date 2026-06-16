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
        if (collision.gameObject.CompareTag("Gnome"))
        {
            StatesData otherData = collision.gameObject.GetComponent<StatesData>();
            if (otherData != null && otherData.wasThrown)
            {
                data.isStunned = true;
            }
        }
    }
}
