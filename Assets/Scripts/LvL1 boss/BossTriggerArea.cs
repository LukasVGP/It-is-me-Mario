using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerArea : MonoBehaviour
{
    public GruzMother gruzMother;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gruzMother != null)
            {
                gruzMother.ActivateBoss();
            }
            // Optionally, disable this trigger to prevent multiple activations
            gameObject.SetActive(false);
        }
    }
}
