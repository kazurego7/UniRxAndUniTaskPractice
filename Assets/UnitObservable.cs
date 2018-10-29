using System;
using UniRx;
using UniRx.Async;
using UnityEngine;
using static UniRx.Observable;

public static class UnitObservable {
	public static IObservable<Unit> TimerUnit (TimeSpan dueTime) {
		return Timer (dueTime).Select (_ => Unit.Default);
	}

	public static IObservable<Unit> PrintUnit<T> (T output) {
		Unit Log (object _output) {
			Debug.Log (_output);
			return Unit.Default;
		}
		return Empty (Log (output));
	}
}