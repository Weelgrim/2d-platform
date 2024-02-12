using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public GameObject boom;
    public GameObject barrel;
    private float t = 0;
    private bool _boom = false;
    
    private void OnTriggerEnter2D(Collider2D ActiveGameObject)
    {
        if(ActiveGameObject.CompareTag("active game objects"))
        {
            boom.SetActive(true);
            barrel.SetActive(false);
            t = 0.5f;
            _boom = true;
        }
    }

    private void Update()
    {
        if (_boom == true)
        {
            t -= Time.deltaTime;
            if (t <= 0)
            {
                Destroy(boom);
            }
        }
        
    }
}
