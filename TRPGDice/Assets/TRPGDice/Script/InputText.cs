using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using UniRx;
public class InputText : MonoBehaviour {
    [SerializeField]
    ButtonPush _Button;

    InputField _Input;
    int temp;

    ReactiveProperty<int> InputProperty = new ReactiveProperty<int>(2);
    public ReadOnlyReactiveProperty<int> ReadOnlyInputProperty{ get { return InputProperty.ToReadOnlyReactiveProperty(); } }

	// Use this for initialization
	void Start () {
        _Input = GetComponent<InputField>();
        _Input.OnValueChangeAsObservable()
            .Where(x => !string.IsNullOrEmpty(x) && int.TryParse(x, out temp))
            .Select(x => int.Parse(x))
            .Where(x => x != 0)
            .Subscribe(x => InputProperty.Value = x);
        _Button.ReadOnlyButtonProperty
            .Where(x => x)
            .Subscribe(x=>_Input.text= InputProperty.Value.ToString());
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
