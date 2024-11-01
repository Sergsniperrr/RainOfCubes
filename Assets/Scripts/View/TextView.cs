using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextView : MonoBehaviour
{
    [SerializeField] protected SpawnerStats Counters;

    private TMP_Text _textView;

    private void Awake()
    {
        _textView = GetComponent<TMP_Text>();
    }

    protected void ShowCounter(int counter)
    {
        _textView.text = counter.ToString();
    }
}
