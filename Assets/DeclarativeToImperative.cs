using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Async;
using UnityEngine;
using static UnitObservable;
using static SyncAsync;
using static UniRx.Observable;
using System;
using System.Linq;

public class DeclarativeToImperative : MonoBehaviour {

	// Use this for initialization
	void Start () {

		var periodFrameCount = 60;
		var maxCount = 6;
		IObservable<Unit> DeclarativeDo () {
			return
			TimerFrame (0, periodFrameCount)
				.TakeWhile (t => t <= maxCount)
				.SelectMany (count => PrintUnit ("test"));
		}
		IObservable<Unit> ImperativeDo () {
			return SyncLoop (Enumerable.Range (0, maxCount), t =>
				Sync (
					TimerFrame (periodFrameCount).AsUnitObservable (),
					Observable.Start (() => Debug.Log ("test"))
				));
		}
		// DeclarativeDo ().Subscribe ();
		ImperativeDo ().Subscribe ();
	}
}