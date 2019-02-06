using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SalmonInteractable : MonoBehaviour
{
    public GameObject xrRig = null;
    public float lookAtTime = 2.0f;  // track for the lookat time
    private float lookAtCounter = 0.0f;

    private Vector3 salmonLocation;

    private float distanceX;
    private float distanceZ;
    private float distanceY;

    public float moveSpeed = 0.3f;

    private bool GazeSuccess = false;
    private bool moving = false;

    public PostProcessVolume iv = null;
    Vignette vigChange;

    protected float startValueofVig;

    // Start is called before the first frame update
    void Start()
    {
        salmonLocation = this.transform.position;
        iv = GetComponent<PostProcessVolume>();
        iv.profile.TryGetSettings(out vigChange);
        
    }

    // Update is called once per frame
    void Update()
    {
        vigChange.intensity.value = 1.0f;
        Debug.Log(vigChange.intensity.value);
        if (GazeSuccess)
        {

            if (Vector3.Distance(xrRig.transform.position, salmonLocation) > 0.5f)
            {
                moving = true;
                TriggerMove();
                
            } else
            {
                moving = false;
                vigChange.intensity.value = 0f;
            }

        }
        if (!moving)
        {
            calculateDistance();
        }

    }

    public virtual void OnSelectEnter()
    {
        lookAtCounter += Time.deltaTime;
        // should add the position comparison to make the player not moving again
        if (lookAtCounter > lookAtTime)
        {
            GazeSuccess = true;
            lookAtCounter = 0.0f;
        }
    }

    public virtual void OnSelectExit()
    {
            GazeSuccess = false;
            lookAtCounter = 0.0f;
    }

    private void calculateDistance()
    {
        distanceX = xrRig.transform.position.x - this.transform.position.x;
        distanceY = xrRig.transform.position.y - this.transform.position.y;
        distanceZ = xrRig.transform.position.z - this.transform.position.z;
    }


    private void TriggerMove()
    {
        xrRig.transform.position -= new Vector3(
            distanceX * Time.deltaTime * moveSpeed,
            distanceY * Time.deltaTime * moveSpeed,
            distanceZ * Time.deltaTime * moveSpeed);
    }

}
