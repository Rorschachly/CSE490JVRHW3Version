using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class IncreaseVignette : MonoBehaviour {

	public PostProcessVolume volume;
	public float changeSpeed = 0.05f;
	public float vignetteStart = 0.0f;
	public float vignetteEnd = 0.0f;

	Vignette vignetteLayer = null;

	public void Start()
	{
		volume.profile.TryGetSettings(out vignetteLayer);
	}

	public void IncreaseVignetteValue () 
	{
		if (vignetteLayer.intensity.value < vignetteEnd)
			vignetteLayer.intensity.value += changeSpeed;
	}

	public void DecreaseVignetteValue ()
	{
		if (vignetteLayer.intensity.value > vignetteStart)
			vignetteLayer.intensity.value -= changeSpeed;
	}
}
