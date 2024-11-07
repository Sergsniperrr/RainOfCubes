using UnityEngine;

public class Explosion : MonoBehaviour
{
    public void Explode()
    {
        float force = 400f;
        float radius = 12f;
        Rigidbody rigidbody;

        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, radius);

        for (int j = 0; j < overlappedColliders.Length; j++)
        {
            rigidbody = overlappedColliders[j].attachedRigidbody;

            if (rigidbody)
                rigidbody.AddExplosionForce(force, transform.position, radius);
        }
    }
}
