using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using static UniRx.Observable;
using static SyncAsync;

public static class SyncAsync {
	public static IObservable<T> Sync<T> (params IObservable<T>[] args) {
		return Concat (args);
	}
	public static IObservable<T> Sync<T> (this IObservable<T> stream, params IObservable<T>[] args) {
		return stream.Concat (args);
	}
	public static IObservable<T> Async<T> (params IObservable<T>[] args) {
		return Merge (args);
	}
	public static IObservable<T> Async<T> (this IObservable<T> stream, params IObservable<T>[] args) {
		return stream.Concat (Merge (args));
	}
	public static IObservable<TR> SyncLoop<T, TR> (IEnumerable<T> items, Func<T, IObservable<TR>> proc) {
		return items.Select (item => proc (item)).Concat ();
	}
	public static IObservable<TR> AsyncLoop<T, TR> (IEnumerable<T> items, Func<T, IObservable<TR>> proc) {
		return items.Select (item => proc (item)).Merge ();
	}
}