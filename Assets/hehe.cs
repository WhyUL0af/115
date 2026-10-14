using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class hehe : MonoBehaviour
{
    [SerializeField] private GameObject InputField;
    [SerializeField] private GameObject Text;
    [SerializeField] private GameObject Btn;

    [SerializeField] private GameObject Btn2;
    [SerializeField] private GameObject[] Images;

    private Text _textComponent;
    
    void Start()
    {
        Btn.GetComponent<Button>().onClick.AddListener(OnButtonClick);
        Btn2.GetComponent<Button>().onClick.AddListener(OnButton2Click);


        _textComponent = Text.GetComponent<Text>();
        _textComponent.text = "0";
    }

    private void OnInputFieldClick()
    {
    }

    private void OnButtonClick()
    {
        string inputText = InputField.GetComponent<InputField>().text;
        _textComponent.text = (int.Parse(_textComponent.text) + int.Parse(inputText)).ToString();
    }

    private void OnButton2Click()
    {
        foreach (var image in Images)
        {
            image.SetActive(!image.activeSelf);
        }
    }
}
