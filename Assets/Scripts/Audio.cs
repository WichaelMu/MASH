using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MW.Audio;

public class Audio : MAudio
{
	void Awake()
	{
		Initialise(SSounds);
	}
}
