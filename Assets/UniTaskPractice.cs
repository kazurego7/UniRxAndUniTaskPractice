using System.Collections;
using System.Collections.Generic;
using UniRx;
using static UniRx.Observable;
using UniRx.Async;
using UnityEngine;

public class UniTaskPractice : MonoBehaviour {

	void Start () {
		async UniTask<Unit> Print (object output) {
			await UniTask.Run (() => Debug.Log (output));
			return await UniTask.Run (() => Unit.Default);
		}
		Concat ( // うまくいかない
			TimerFrame (10).AsUnitObservable (),
			Print ("test").ToObservable ()
		).Subscribe ();
	}

}