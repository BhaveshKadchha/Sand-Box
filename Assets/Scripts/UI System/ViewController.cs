using System.Collections.Generic;
using UnityEngine;

namespace Game.UISystem
{
	class ViewController : Singleton<ViewController>
	{
		public enum ScreenEnum
		{
			MainMenu, GamePlay, GameOver
		}

		Screens _currentView;
		Screens _previousView;
		[SerializeField] Canvas _autoOnCanvas;

		[Space(10)]
		[SerializeField] List<ScreenView> _screens = new List<ScreenView>();

		[Space(10), Header("Global PopUps")]
		public GlobalPopUpWarning globalWarningPopUp;
		public GlobalPopUpMessage globalMessagePopUp;

		[System.Serializable]
		public struct ScreenView
		{
			public Screens screen;
			public ScreenEnum screenName;
		}

		void Start()
		{
			Init();

			if (_autoOnCanvas)
				_autoOnCanvas.enabled = true;
		}

		public void Show(ScreenEnum screen)
		{
			if (_currentView != null)
			{
				if (_previousView)
					_previousView.EndHideAnimation();
				_previousView = _currentView;
				_previousView.Hide();
				_currentView = _screens[GetScreen(screen)].screen;
				_currentView.Show();
			}
			else
			{
				_currentView = _screens[GetScreen(screen)].screen;
				_currentView.Show();
			}
		}

		internal void ShowPreviousView()
		{
			Screens temp = _currentView;
			_currentView = _previousView;
			_previousView = temp;

			_currentView.Show();
		}

		int GetScreen(ScreenEnum screenName)
		{
			return _screens.FindIndex(
			delegate (ScreenView screenView)
			{
				return screenView.screenName.Equals(screenName);
			});
		}

		void Init()
		{
			for (int indexOfScreen = 0; indexOfScreen < _screens.Count; indexOfScreen++)
			{
				_screens[indexOfScreen].screen.canvas.enabled = false;
			}
		}
	}
}