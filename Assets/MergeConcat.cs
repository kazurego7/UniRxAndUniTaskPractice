using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MergeConcat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Observable.Concat (
			Observable.Merge (
				Observable.TimerFrame (70).ForEachAsync (_ => Debug.Log ("1")),
				Observable.TimerFrame (60).ForEachAsync (_ => Debug.Log ("2"))
			),
			Observable.TimerFrame (60).ForEachAsync (_ => Debug.Log ("3"))
		).Subscribe ();
	}
}