using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour {

	public GameObject xrRig = null;
	//move to a object looking at it
	public bool gazeBasedMove = false;
	public float stareLength = 2.0f;
	public List<GameObject> moveTo = new List<GameObject>();
	private float currentTimer = 0.0f;
	private float distanceX = 0.0f;
	private float distanceZ = 0.0f;
	private float distanceMoveError = 0.5f;
	private int currentIndex = 0;
	private bool startMovement = false;

	public float moveSpeed = 0.6f;

	private float lastDistance = 0.0f;
	private float lastHeadPosition = 0.0f;
	private float distanceError = 0.001f;

	private Rigidbody rb;
	private SimpleGaze sg;
	// private IncreaseVignette iv;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		sg = GetComponent<SimpleGaze>();
		// iv = GetComponent<IncreaseVignette>();
		SetLocation(currentIndex);
	}
	
	// Update is called once per frame
	void Update () {
		if (gazeBasedMove)
		{
			CheckLookAtLength();
		}
		if(startMovement)
		{
			TriggerMove();
			// iv.IncreaseVignetteValue();
		}
		else
		{
			// iv.DecreaseVignetteValue();
		}
	}

	private void SetLocation(int index)
	{
        
		if (moveTo[index] != null)
		{
			moveTo[index].SetActive(true);
			distanceX = transform.position.x - moveTo[index].transform.position.x;
			distanceZ = transform.position.z - moveTo[index].transform.position.z;
		}
	}


	private void CheckLookAtLength()
	{

		if(GameObject.ReferenceEquals(sg.objectCurrentlyLookingAt, moveTo[currentIndex]) && moveTo != null)
		{
			currentTimer += Time.deltaTime;
			if (currentTimer > stareLength)
			{
				startMovement = true;
			}
		}
		else
		{
			currentTimer = 0.0f;
		}
	}

	private void TriggerMove()
	{
		
		if (Vector3.Distance(xrRig.transform.position, new Vector3(moveTo[currentIndex].transform.position.x,xrRig.transform.position.y, moveTo[currentIndex].transform.position.z)) > distanceMoveError)
		{
			xrRig.transform.position -= new Vector3(
				distanceX * Time.deltaTime * moveSpeed, 
				xrRig.transform.position.y,
				distanceZ * Time.deltaTime * moveSpeed);

		}
		else
		{

			startMovement = false;
			moveTo[currentIndex].SetActive(false);

			currentIndex +=1;

			if (currentIndex == moveTo.Count)
				currentIndex =0;

			SetLocation(currentIndex);
		}
	}
}
