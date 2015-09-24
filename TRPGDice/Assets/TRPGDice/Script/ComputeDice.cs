using UnityEngine;
using System.Collections;

public class ComputeDice  {
    System.Random _Random = new System.Random();

    /// <summary>
    /// countの回数num面ダイスを振る
    /// 表示形式は"ダイスの合計(1回ごとのダイスの結果)"
    /// </summary>
    /// <param name="count"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public string RandData(int count, int num)
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
}
