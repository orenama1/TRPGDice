using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UniRx;
using UniRx.Triggers;
using System;

public class Test : MonoBehaviour {
    bool test1 = true;        

    plugins p;
	// Use this for initialization
    void Start()
    {
        
        p=new plugins();
        this.UpdateAsObservable()
            .Where(_ => !test1)
            .Subscribe(_ => Application.Quit());
        this.UpdateAsObservable()
            .Select(_ =>
            {
                test1 = p.ISss();
                return test1;
            })
            .Subscribe(x => {  });
	}
	
	// Update is called once per frame
	void Update () {
	}
}
