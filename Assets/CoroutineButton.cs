using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

public class CoroutineButton : MonoBehaviour {
	[SerializeField] Button _cor1Button;
	[SerializeField] Button _cor2Button;

	IEnumerator Cor1 () {
		yield return new WaitForSeconds (3);
		Debug.Log ("Cor1: waited 3 sec");
		yield return null;
	}

	IEnumerator Cor2 () {
		yield return new WaitForSeconds (1);
		Debug.Log ("Cor2: waited 1 sec");
		yield return new WaitForSeconds (4);
		Debug.Log ("Cor2: waited 4 sec");
		yield return null;
	}

	void Start () {
		var a = _cor1Button.onClick.AsObservable ();
		var b = _cor2Button.onClick.AsObservable ();
		var obs1 = a.Select (_ => Observable.FromCoroutine (Cor1));
		var obs2 = b.Select (_ => Observable.FromCoroutine (Cor2));
		Observable.Merge (obs1, obs2).Concat ().Subscribe ();
	}

}