using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public static class SyncAsync {
	public static IObservable<T> Sync<T> (params IObservable<T>[] args) {
		return Observable.Concat (args);
	}
	public static IObservable<T> Sync<T> (this IObservable<T> stream, params IObservable<T>[] args) {
		return stream.Concat (args);
	}
	public static IObservable<T> Async<T> (params IObservable<T>[] args) {
		return Observable.Merge (args);
	}
	public static IObservable<T> Async<T> (this IObservable<T> stream, params IObservable<T>[] args) {
		return stream.Concat (Observable.Merge (args));
	}
}