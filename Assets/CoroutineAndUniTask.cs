using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Async;
using UnityEngine;

public class CoroutineAndUniTask : MonoBehaviour {
	IEnumerator Cor1 () {
		yield return new WaitForSeconds (3);
		Debug.Log ("Cor1: waited 3 sec");
		yield return null;
	}

	IEnumerator Cor2 () {
		yield return new WaitForSeconds (1);
		Debug.Log ("Cor2: waited 1 sec");
		yield return new WaitForSeconds (9);
		Debug.Log ("Cor2: waited 9 sec");
		yield return null;
	}

	void DoCoroutineOnObservable () {
		var a = new Subject<Unit> ();
		var b = new Subject<Unit> ();
		var obs1 = a.Select (_ => Observable.FromCoroutine (Cor1));
		var obs2 = b.Select (_ => Observable.FromCoroutine (Cor2));
		Observable.Merge (obs1, obs2).Concat ().Subscribe ();

		// 発行
		a.OnNext (Unit.Default);
		b.OnNext (Unit.Default);
		a.OnNext (Unit.Default);
	}

	async UniTask<Unit> Task1 () {
		await UniTask.Delay (3000); // note! Delayはミリ秒指定
		Debug.Log ("Task1: waited 3 sec");
		return Unit.Default;
	}

	async UniTask<Unit> Task2 () {
		await UniTask.Delay (1000);
		Debug.Log ("Task2: waited 1 sec");
		await UniTask.Delay (9000);
		Debug.Log ("Task2: waited 9 sec");
		return Unit.Default;
	}
	void DoUniTaskOnObservable () {
		// ストリームの構築
		var a = new Subject<Unit> ();
		var b = new Subject<Unit> ();
		var obs1 = a.Select (_ => Task1 ().ToObservable ());
		var obs2 = b.Select (_ => Task2 ().ToObservable ());
		Observable.Merge (obs1, obs2).Concat ().Subscribe ();

		// 発行
		a.OnNext (Unit.Default);
		b.OnNext (Unit.Default);
		a.OnNext (Unit.Default);
	}

	void DoObservableFromCoroutine () {
		var Obs1 = Observable.FromCoroutine (Cor1);
		var Obs2 = Observable.FromCoroutine (Cor2);

		// 並列処理
		Obs1.Merge (Obs2).Subscribe ();

		// 直列処理
		// Obs1.Concat (Obs2).Subscribe ();
	}
	void DoObservableFromUniTask () {
		var Obs1 = Task1 ().ToObservable ();
		var Obs2 = Task2 ().ToObservable ();

		// 並列処理
		Obs1.Merge (Obs2).Subscribe ();

		// 直列処理
		// Obs1.Concat (Obs2).Subscribe ();
	}
	void DoUniTask () {
		// 並列処理
		Task1 ().Forget ();
		Task2 ().Forget ();

		// 直列処理
		//await Task1 ();
		//await Task2 ();
	}

	void Start () {
		// DoObservableFromCoroutine ();
		// DoObservableFromUniTask ();
		// DoUniTask ();
		// DoCoroutineOnObservable ();
		DoUniTaskOnObservable ();
	}
}