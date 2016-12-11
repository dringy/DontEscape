using UnityEngine;
using System.Collections;

//Used to interact with things
public class Interactor : MonoBehaviour {
    private InteractableEntity currentEntity;
    private Collider2D currentEntityCollider;

    private Collider2D interactionCollider;

    private void Start()
    {
        interactionCollider = GetComponent<Collider2D>();
    }

    //Register Interaction Entity
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Interactable" && !col.Equals(currentEntityCollider))
        {
            currentEntity = col.gameObject.GetComponent<InteractableEntity>();
            currentEntityCollider = col;
        } 
    }

    //Interact with an entity if it is still valid
    public void Interact()
    {
        if (currentEntity != null && currentEntityCollider != null)
        {
            if (!currentEntityCollider.bounds.Intersects(interactionCollider.bounds))
            {
                currentEntity = null;
                currentEntityCollider = null;
            }
        }

        if (currentEntity != null)
        {
            currentEntity.Interact();
        }
    }
}
