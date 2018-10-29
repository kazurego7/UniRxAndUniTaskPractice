using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MyAll : MonoBehaviour {
	public Button _btn1;
	public Button _btn2;

	// Use this for initialization
	void Start () {
		// _btn1.OnClickAsObservable ().Subscribe (_ => {
		// 	sub1.OnNext (_);
		// });
		// var obs2 = Observable.TimerFrame (0, 60).Select (t => "2nd");
		// obs1.TakeUntil (obs2).Concat (obs2).Subscribe (x => Debug.Log (x));
		var asyncCommand = new ReactiveCommand ();
		var async = asyncCommand.Select (_ => Observable.TimerFrame (60, 60).Take (3).Select (t => {
			Debug.Log ($"async{t}");
			return Unit.Default;
		}));
		var syncCommand = new ReactiveCommand ();
		var sync = syncCommand.Select (_ => Observable.TimerFrame (60, 60).Take (3).Select (t => {
			Debug.Log ($"sync{t}");
			return Unit.Default;
		}));
		//sync.Concat ().Merge (async.Merge ()).Subscribe ();
		sync.Merge (async).Concat ().Subscribe ();

		syncCommand.BindTo (_btn1);
		asyncCommand.BindTo (_btn2);
	}

	public static class MyAlls {
		public static IObservable<T> MergeAll<T> (params IObservable<T>[] other) {
			return other.Aggregate ((accm, x) => accm.Merge (x));
		}
		public static IObservable<T> ConcatAll<T> (params IObservable<T>[] other) {
			return other.Aggregate ((accm, x) => accm.Concat (x));
		}
	}
}