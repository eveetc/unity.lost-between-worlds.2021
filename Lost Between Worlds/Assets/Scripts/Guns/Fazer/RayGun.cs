using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    /**
    Content Imported from https://www.firemind-academy.com/p/how-to-shoot-lasers-in-unity
    **/
    public float shootRate;
    private float m_shootRateTimeStamp;

    public GameObject m_shotPrefab;

    RaycastHit hit;
    float range = 1000.0f;


    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (Time.time > m_shootRateTimeStamp)
            {
                shootRay();
                m_shootRateTimeStamp = Time.time + shootRate;
            }
        }

    }

    void shootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, range))
        {
            GameObject laser = GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<ShootBehavior>().setTarget(hit.point);
            GameObject.Destroy(laser, 2f);


            //activates portal to travel home

            if (hit.collider.gameObject.name == "Home" || hit.collider.gameObject.name == "HomePortalBody" || hit.collider.gameObject.name == "HomeCube")
            {
                GameObject.Find("Home").gameObject.GetComponent<BoxCollider>().enabled = true;
                GameObject.Find("homePortalEffect").gameObject.transform.localScale = new Vector3(5.0f, 5.0f, 2.0f);
            }

        }

    }

}
