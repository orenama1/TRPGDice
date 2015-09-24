using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

public class ResultText : MonoBehaviour {
    [SerializeField]
    InputText _Num, _Count;

    [SerializeField]
    ButtonPush _Button;

    Text _Result;

    ComputeDice _Dice=new ComputeDice();

    int count, num;
	// Use this for initialization
	void Start () {
        _Count.ReadOnlyInputProperty
            .DistinctUntilChanged()
            .Subscribe(x=> { count = x; });

        _Num.ReadOnlyInputProperty
            .DistinctUntilChanged()
            .Subscribe( x => { num = x; });

        _Result = GetComponent<Text>();

        _Button.ReadOnlyButtonProperty
            .Where(x=>x)
            .Select(_ => _Dice.RandData(count, num))
            .SubscribeToText(_Result);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
