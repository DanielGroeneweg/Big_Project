using UnityEngine;

public class Stunnable : MonoBehaviour
{
    private EnemyStatesData data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        data = GetComponent<EnemyStatesData>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gnome"))
        {
            EnemyStatesData otherData = collision.gameObject.GetComponent<EnemyStatesData>();
            if (otherData != null && otherData.wasThrown)
            {
                data.isStunned = true;
            }
        }
    }
}
