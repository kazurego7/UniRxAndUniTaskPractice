using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using static UniRx.Observable;

public class HotOrCold : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var sub = new Subject<Unit> ();
		var empty = Empty (Unit.Default);
		var subemp = sub.Concat (empty);
	}
}