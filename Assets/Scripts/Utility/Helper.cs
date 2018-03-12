﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour 
{
	public static IEnumerator waitThenCallback(float time, Action callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}
}
