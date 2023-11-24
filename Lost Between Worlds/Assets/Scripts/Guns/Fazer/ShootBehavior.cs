using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehavior : MonoBehaviour
{
    /**
   Content Imported from https://www.firemind-academy.com/p/how-to-shoot-lasers-in-unity
   **/
    public Vector3 m_target;
    public GameObject collisionExplosion;
    public float speed;


    void Update()
    {
        float step = speed * Time.deltaTime;

        if (m_target != null)
        {
            if (transform.position == m_target)
            {
                explode();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, m_target, step);
        }

    }

    public void setTarget(Vector3 target)
    {
        m_target = target;
    }

    void explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = (GameObject)Instantiate(
                collisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }
}
