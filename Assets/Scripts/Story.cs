using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
	[SerializeField] Text m_text = default;
	IEnumerator Start()
	{
		m_text.text = "んん・・・。";
		yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
		yield return null;

		m_text.text = "ここはどこだ？";
		yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
		yield return null;

		m_text.text = "どうしてこんなところに？";
	}
}
