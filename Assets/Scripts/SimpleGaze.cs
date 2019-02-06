using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGaze : MonoBehaviour {

 	public Camera viewCamera;

    public GameObject objectCurrentlyLookingAt = null;

    public float maxRaycastDistance = 40;

    [SerializeField] LayerMask choosenLayers;


    // Update is called once per frame
    void Update () {

        CheckLookAt();
	}


    private void CheckLookAt()
    {
    // Create a gaze ray pointing forward from the camera
    int layerMask = choosenLayers.value;
    Ray ray = new Ray(viewCamera.transform.position, viewCamera.transform.TransformDirection(Vector3.forward) *maxRaycastDistance);
        
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
    {

    	Debug.DrawRay(viewCamera.transform.position, viewCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow,1.0f,true);
    	var interactable = hit.collider.gameObject.GetComponent<SimpleInteractable>();
        var salmonInteractable = hit.collider.gameObject.GetComponent<SalmonInteractable>();
        if (interactable!=null)
        {
            interactable.OnSelectEnter();
        }
        if (salmonInteractable != null)
        {
            salmonInteractable.OnSelectEnter();
        }
        objectCurrentlyLookingAt = hit.collider.gameObject; 
        
    }
    else
    {
        Debug.DrawRay(viewCamera.transform.position, viewCamera.transform.TransformDirection(Vector3.forward) *maxRaycastDistance, Color.white,1.0f,true);
        if (objectCurrentlyLookingAt!= null)
        {
            var interactable = objectCurrentlyLookingAt.GetComponent<SimpleInteractable>();
            var salmonInteractable = objectCurrentlyLookingAt.GetComponent<SalmonInteractable>();
            if (interactable != null)
            {
                interactable.OnSelectExit();
            }
            if (salmonInteractable != null)
            {
                salmonInteractable.OnSelectExit();
            }
            objectCurrentlyLookingAt = null;
        }
        
    }

}

}
