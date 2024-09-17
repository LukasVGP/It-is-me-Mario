using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger1 : MonoBehaviour
{
    public GruzMother gruzMother;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivateBoss();
        }
    }

    private void ActivateBoss()
    {
        if (gruzMother != null)
        {
            gruzMother.ActivateBoss();
            
        }
        gameObject.SetActive(false); // Disable the trigger after activation
    }
}
