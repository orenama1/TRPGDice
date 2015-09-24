using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

public class ButtonPush : MonoBehaviour {
    Button _Button;
    ReactiveProperty<bool> ButtonProperty = new ReactiveProperty<bool>(false);
    public ReadOnlyReactiveProperty<bool> ReadOnlyButtonProperty { get { return ButtonProperty.ToReadOnlyReactiveProperty(); } }
	// Use this for initialization
	void Start () {
        _Button = GetComponent<Button>();
        var button =_Button.OnClickAsObservable();
        button.Subscribe(_ => ButtonProperty.Value = true);
        button.Skip(1)
            .Subscribe(_ => ButtonProperty.Value = false);

    }

    // Update is called once per frame
    void Update () {
	
	}
}
