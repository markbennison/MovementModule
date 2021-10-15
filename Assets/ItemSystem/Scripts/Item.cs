using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject itemPrefab;
    public Sprite icon;

    public string itemName;
    [TextArea(4,16)]
    public string description;

    public float weight = 0;
    public int maxStackableQuantity = 1; // for bundles of items, such as arrows or coins.
    public int quantity = 1;

    public bool isStorable = false; // if false, item will be used on pickup
    public bool isConsumable = true; // if true, item will be destroyed (or quantity decremented if >1) when used
    public bool isCollideToPickup = false; // if true, collider.isTrigger == true

	private void Start()
	{
        InitialiseCollideToPickup();

    }

    void InitialiseCollideToPickup()
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            if (isCollideToPickup)
            {
                collider.isTrigger = true;
            }
            else
            {
                collider.isTrigger = false;
            }
        }
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
            Interact();
        }
	}

	public void Interact()
    {
        Debug.Log("Picked up " + transform.name);

        if (isStorable)
        {
            Store();
        }
        else
        {
            Use();
        }
    }

    void Store()
    {
        Debug.Log("Storing " + transform.name);
        
        // ToDo
        
        Destroy(gameObject);
    }

    void Use()
    {
        if (isConsumable)
        {
            quantity--;
            if (quantity <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
