using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject missile;
    public GameObject target;

    private bool isMissileActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMissileDemo()
    {
        target.GetComponent<Target>().StartTarget();
    }

    public void ResetMissileDemo()
    {
        target.GetComponent<Target>().ResetTarget();
    }

    public void Fire()
    {
        if (!isMissileActive)
        {
            isMissileActive = true;
            Instantiate(missile, transform.position + new Vector3(0, 2.5f, 2.5f), Quaternion.Euler(-45f, 0, 0));
        }
    }

    public void MissileHit()
    {
        isMissileActive = false;
    }
}
