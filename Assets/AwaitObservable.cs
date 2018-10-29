using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

public class AwaitObservable : MonoBehaviour {
	public Button button;
	// Use this for initialization
	void Start () {
		IObservable<Unit> Event () {
			return Enumerable
				.Select (Enumerable.Range (0, 4), (i, msg) =>
					Observable
					.ReturnUnit ()
					.DelayFrame (60)
					.Do ((_) => Debug.Log ($"ok{i}"))
				).Concat ();
		}
		// Observable.Concat (Event (), Event ()).Subscribe ();
		async UniTask<Unit> Event2 () {
			foreach (var i in Enumerable.Range (0, 4)) {
				await UniTask.DelayFrame (60);
				Debug.Log ($"ok{i}");
			}
			return Unit.Default;
		}
	}

}