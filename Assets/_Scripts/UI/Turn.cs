using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    [SerializeField] private Text _text;
    
    void Start()
    {
    }

    void Update()
    {
    }

    public void UpdateTurn(string text)
    {
        _text.text = text;
    }
}