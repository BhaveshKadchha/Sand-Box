using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIHover : MonoBehaviour
{
	public static bool isHovering;
	public static System.Action StopEventSystem;
	bool isEventSystemOff;
	[SerializeField] GameObject _eventSystem;

	private void Start()
	{
		StopEventSystem += (() => StartCoroutine(StopEvent()));
	}

	void Update()
	{
		if (!isEventSystemOff)
			isHovering = EventSystem.current.IsPointerOverGameObject();
	}

	IEnumerator StopEvent()
	{
		isEventSystemOff = true;
		_eventSystem.SetActive(false);
		yield return GlobalVariable.PointFiveSecond;
		yield return GlobalVariable.PointFiveSecond;
		_eventSystem.SetActive(true);
		isEventSystemOff = false;
	}
}