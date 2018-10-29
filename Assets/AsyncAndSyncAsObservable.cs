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
			TimerUnit (TimeSpan.FromSeconds (1)),
			PrintUnit ("1"),
			TimerUnit (TimeSpan.FromSeconds (2)),
			PrintUnit ("2")
		).Async (
			TimerUnit (TimeSpan.FromSeconds (3)),
			PrintUnit ("3"),
			TimerUnit (TimeSpan.FromSeconds (3)),
			PrintUnit ("3")
		).Subscribe ();
	}
}