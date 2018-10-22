using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using static UniRx.Observable;
using static UnitObservable;
using static SyncAsync;
using System;
using System.Linq;

public class DeclarativeToImperative : MonoBehaviour {

	// Use this for initialization
	void Start () {

		var periodFrameCount = 60;
		var maxCount = 6;
		void DeclarativeDo () {
			TimerFrame (0, periodFrameCount)
				.TakeWhile (t => t <= maxCount)
				.SelectMany (count => PrintLog ("test"))
				.Subscribe ();
		}
		void ImperativeDo () {
			SyncLoop (new int[maxCount],
					Sync (
						TimerFrame (periodFrameCount).AsUnitObservable (),
						PrintLog ("test2").AsUnitObservable ()
					)
				)
				.Subscribe ();
		}
		IEnumerator
		// DeclarativeDo ();
		ImperativeDo ();
	}
}