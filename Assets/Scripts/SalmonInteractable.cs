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

    public Texture2D texture;
    private Texture2D TransparentTexture;
    private bool GazeSuccess = false;
    private bool moving = false;

    private PostProcessVolume iv;
    private Vignette vigChange;

    protected float startValueofVig;

    public float endValueofVig;
    // Start is called before the first frame update
    void Start()
    {
        salmonLocation = this.transform.position;
        iv = GetComponent<PostProcessVolume>();
        iv.profile.TryGetSettings(out vigChange);
        startValueofVig = vigChange.intensity.value;
        // here is the code to draw the transparent texture.
        //int size = Screen.width * Screen.height;
        //TransparentTexture = new Texture2D(Screen.width, Screen.height);
        //Color[] colors = new Color[size];
        //for(int i = 0; i < size; i++)
        //{
        //    colors[i] = Color.clear;
        //}
        //TransparentTexture.SetPixels(colors);
        //TransparentTexture.Apply();
        vigChange.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(xrRig.transform.position);
        if (GazeSuccess)
        {

            if (Vector3.Distance(xrRig.transform.position, salmonLocation) > 0.5f)
            {
                moving = true;
                TriggerMove();
                vigChange.intensity.value = Mathf.Lerp(0, 5, 2);
                Debug.Log(vigChange.intensity.value);
            } else
            {
                moving = false;
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

    //private void OnGUI()
    //{
       
    //    if (moving)
    //    {
    //        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
    //    }
    //    else
    //    {
    //        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TransparentTexture);
    //    }
        
    //}
}
