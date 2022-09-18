using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);

        if (contact.otherCollider)
        {
            Destroy(gameObject, 0.01f);
            Debug.Log("hit");
        }
    }
}
