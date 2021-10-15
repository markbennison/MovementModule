using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	Transform actorCamera;
	LayerMask layerMask;

	[SerializeField]
	private float maxDistanceFromCameraToObjects = 100f;

	[SerializeField]
	private float maxInteractableDistance = 3f;
	private float distanceFromActor;

	void Start()
	{
		layerMask = gameObject.layer;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			Interact();
		}
	}

	void Interact()
	{
		actorCamera = Camera.main.transform;
		Debug.Log("Live Camera: " + actorCamera.name);
		Ray ray = new Ray(actorCamera.position, actorCamera.forward);
		RaycastHit raycastHit;

		//Debug.DrawRay(ray.origin, ray.direction*10f, Color.magenta, 2f);

		//if (Physics.Raycast(ray, out raycastHit, maxDistanceFromCameraToObjects, layerMask))
		if (Physics.Raycast(ray, out raycastHit, maxDistanceFromCameraToObjects, layerMask))
		{
			Debug.Log("layer mask " + layerMask.value.ToString());
			if (raycastHit.transform != null)
			{
				distanceFromActor = Vector3.Distance(transform.position, raycastHit.transform.position);
				if (distanceFromActor <= maxInteractableDistance)
				{
					Debug.Log("In range " + raycastHit.transform.name + " (" + distanceFromActor.ToString("0,00"));
					Item item = raycastHit.transform.GetComponent<Item>();
					if (item != null)
					{
						item.Interact();
					}
				}
			}
		}
		else
		{
			Debug.Log("NO RAYCAST");
		}
	}
}