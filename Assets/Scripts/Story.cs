using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
	[SerializeField] string[] m_serihu = null;
	IEnumerator Start()
	{
		Debug.Log("衝撃のファーストブリット");
		yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
		yield return null;

		Debug.Log("撃滅のセカンドブリット");
		yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
		yield return null;

		Debug.Log("抹殺のラストブリット");
		List<string> serihu = new List<string>();
		//serihu.ForEach(s => s.);
	}
}
