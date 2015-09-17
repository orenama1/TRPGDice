using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using System;
public class DiceMain : MonoBehaviour {
    [SerializeField]
    InputField _Count;
    [SerializeField]
    InputField _Num;
    [SerializeField]
    Text _Result;
    [SerializeField]
    Button _Action;
    int temp;
    System.Random _Random = new System.Random();
    // Use this for initialization
    void Start () {
        var count = _Count.OnValueChangeAsObservable()
            .Where(x=>!string.IsNullOrEmpty(x))
            .Where(x => int.TryParse(x, out temp))
            .Select(x => int.Parse(x));

        var num = _Num.OnValueChangeAsObservable()
            .Where(x =>! string.IsNullOrEmpty(x))
            .Where(x => int.TryParse(x, out temp))
            .Select(x => int.Parse(x));

        var anser = num.CombineLatest(count, (n, c) =>
          {
              return RandData(c, n);
          });
        anser.SubscribeToText(_Result);
        _Action.OnClickAsObservable()
            .Subscribe(_ => anser.SubscribeToText(_Result));
    }
	string RandData(int count,int num)
    {
        int sum = _Random.Next(1, num), tmp;
        string s = "(" + sum.ToString();
        for (int i = 1; i < count; i++)
        {
            s += "+";
            tmp = _Random.Next(1, num);
            s += tmp.ToString();
            sum += tmp;
        }
        s += ")";
        return sum.ToString() + s;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
