using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    Coroutine _loadingAnim;
    Canvas _canvas;
    [SerializeField] Text _loadingTxt;

    public static System.Action ShowLoading;
    public static System.Action HideLoading;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        ShowLoading += Show;
        HideLoading += Hide;
    }

    void Show()
    {
        _canvas.enabled = true;
        _loadingAnim = StartCoroutine(LoadingAnimaton());
    }

    void Hide()
    {
        StartCoroutine(WaitForHide());
    }

    IEnumerator WaitForHide()
    {
        yield return GlobalVariable.PointFiveSecond;
        _canvas.enabled = false;
        StopCoroutine(_loadingAnim);
    }

    IEnumerator LoadingAnimaton()
    {
        _loadingTxt.text = "Loading.";
        yield return GlobalVariable.PointFiveSecond;
        _loadingTxt.text = "Loading..";
        yield return GlobalVariable.PointFiveSecond;
        _loadingTxt.text = "Loading...";
        yield return GlobalVariable.PointFiveSecond;
        _loadingAnim = StartCoroutine(LoadingAnimaton());
    }
}
