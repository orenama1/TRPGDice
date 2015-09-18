using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using System.Linq;
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
        //Observable.CombineLatest(_Count.OnValueChangeAsObservable(),
        //    _Num.OnValueChangeAsObservable())
        //    .Select(x=>x.Select(y=>int.Parse(y)))
        //    .Select(x=>RandData(x.First(), x.ElementAt(1)))
        //    .SubscribeToText(_Result);
            
        //0以外の数値のみ通す
        var count = _Count.OnValueChangeAsObservable()
            .Where(x=>!string.IsNullOrEmpty(x)&&int.TryParse(x, out temp))
            .Select(x => int.Parse(x))
            .Where(x=>x!=0);

        //0以外の数値のみ通す
        var num = _Num.OnValueChangeAsObservable()
            .Where(x => !string.IsNullOrEmpty(x) && int.TryParse(x, out temp))
            .Select(x => int.Parse(x))
            .Where(x => x != 0);

        //片方でも変化したら計算
        var anser = num.CombineLatest(count, (n, c) =>
          {
              return RandData(c, n);
          });
        //ダイス結果の描画
        anser.SubscribeToText(_Result);

        //クリックしたらダイス結果の再計算と描画
        _Action.OnClickAsObservable()
            .Subscribe(_ => anser.SubscribeToText(_Result));
    }
    /// <summary>
    /// countの回数num面ダイスを振る
    /// 表示形式は"ダイスの合計(1回ごとのダイスの結果)"
    /// </summary>
    /// <param name="count"></param>
    /// <param name="num"></param>
    /// <returns></returns>
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
