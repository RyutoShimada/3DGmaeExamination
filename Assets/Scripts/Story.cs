﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
	[SerializeField] Text m_textObj = default;
	[SerializeField] string[] m_text;
	[SerializeField] StartLoad m_startLoad = default;
	IEnumerator Start()
	{
        foreach (var s in m_text)
        {
			m_textObj.text = s;
			yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
			yield return null;
		}

		m_startLoad.StartToLoad();
	}
}
