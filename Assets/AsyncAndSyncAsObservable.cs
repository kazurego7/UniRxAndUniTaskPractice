using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SyncAsync;
using static UnitObservable;
using static UniRx.Observable;
using System;
using UniRx;

public class AsyncAndSyncAsObservable : MonoBehaviour {
	void Start () {
		/*
		Sync は、IObservable<T>をいくつか直列（同期的）に処理する (内部でただConcatしてるだけ)
		Async は、IObservable<T>をいくつか並行（非同期的）に処理する(内部でただMergeしてるだけ)
		ちなみに、途中のAsyncは、前後のObservableと直列につながっている(MergeしてConcat)。
		*/
		Sync (
			TimerDefault (TimeSpan.FromSeconds (1)),
			PrintLog ("1"),
			TimerDefault (TimeSpan.FromSeconds (2)),
			PrintLog ("2")
		).Async (
			TimerDefault (TimeSpan.FromSeconds (3)),
			PrintLog ("3"),
			TimerDefault (TimeSpan.FromSeconds (3)),
			PrintLog ("3")
		).Subscribe ();
	}
}