using System;
using UniRx;
using UnityEngine;

public static class UnitObservable {
	public static IObservable<Unit> TimerDefault (TimeSpan dueTime) {
		return Observable.Timer (dueTime).Select (_ => Unit.Default);
	}
	public static IObservable<Unit> PrintLog (object output) {
		return Observable.ReturnUnit ().Select (_ => {
			Debug.Log (output);
			return Unit.Default;
		});
	}
}