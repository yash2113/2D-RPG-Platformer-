using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;
    public string id;
    public bool activationStatus;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogWarning("Animator component not found on checkpoint object: " + gameObject.name);
        }
    }

    [ContextMenu("Generate Checkpoint Id")]
    private void GenerateId()
    {
        id = System.Guid.NewGuid().ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            ActivateCheckpoint();
        }
    }

    public void ActivateCheckpoint()
    {
        /*anim.SetBool("active", true);
        activationStatus = true;*/

        if(!activationStatus)
        {
            AudioManager.instance.PlaySFX(4, transform);
        }

        if (anim != null)
        {
            anim.SetBool("active", true);
            activationStatus = true;
        }
        else
        {
            Debug.LogError("Animator component is null. Check if it's properly attached to the checkpoint object.");
        }
    }
}
