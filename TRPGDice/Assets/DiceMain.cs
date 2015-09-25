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
    int num, count;
    bool _Click=false;
    System.Random _Random = new System.Random();

    // Use this for initialization
    void Start () {
        this.UpdateAsObservable()
            .Where(_ => Input.GetKey(KeyCode.Escape))
            .Subscribe(_ => Application.Quit());
            
        //0以外の数値のみ通す
        _Count.OnValueChangeAsObservable()
            .Where(x=>!string.IsNullOrEmpty(x)&&int.TryParse(x, out temp))
            .Select(x => int.Parse(x))
            .Where(x=>x!=0)
            .Subscribe(x=>this.count=x);


        //0以外の数値のみ通す
        _Num.OnValueChangeAsObservable()
            .Where(x => !string.IsNullOrEmpty(x) && int.TryParse(x, out temp))
            .Select(x => int.Parse(x))
            .Where(x => x != 0)
            .Subscribe(x=>this.num=x);

        _Count.OnValueChangeAsObservable()
            .Select(x => "")
            .SubscribeToText(_Result);
        _Num.OnValueChangeAsObservable()
            .Select(x=>"")
            .SubscribeToText(_Result);

        //計算
        _Action.OnClickAsObservable()
            .Select(_=> RandData(this.count, this.num+1))
            .Do(x=> {
                _Count.text = count.ToString();
                _Num.text = num.ToString();
            })
            .SubscribeToText(_Result);
        
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
