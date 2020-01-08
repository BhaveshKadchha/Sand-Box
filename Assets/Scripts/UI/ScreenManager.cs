using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.UI
{
    public class ScreenManager : Singleton<ScreenManager>
    {
        [SerializeField] List<ScreenData> screenData;
        [SerializeField, Space(10)] Canvas defaultOnCanvas;

        List<ScreenTypePair> screens = new List<ScreenTypePair>();
        Screen _currentScreen;
        Screen _previousScreen;

        private void Start()
        {
            Init();
        }

        public void Show(ScreenType type)
        {
            if (_currentScreen == null)
            {
                _currentScreen = GetScreen(type);
                _currentScreen.Show(this);
            }
            else
            {
                _previousScreen = _currentScreen;
                _currentScreen = GetScreen(type);
                _previousScreen.Hide(this, () => { _currentScreen.Show(this); });
            }
        }

        Screen GetScreen(ScreenType screenType)
        {
            return screens.Find(x => x.screenType.Equals(screenType)).screen;
        }

        void Init()
        {
            foreach (ScreenData data in screenData)
            {
                if (!data.rect)
                    data.rect = data.screenCanvas.transform.GetChild(0).GetComponent<RectTransform>();

                Screen screen = new Screen(data.screenCanvas, data.rect, data.showAnimation, data.hideAnimation);
                ScreenTypePair temp = new ScreenTypePair(screen, data.screenType);
                screens.Add(temp);
            }


            if (!defaultOnCanvas)
            {
                defaultOnCanvas = screenData[0].screenCanvas;
                _currentScreen = screens[0].screen;
            }
            else
            {
                _currentScreen = GetScreen(screenData.Find(x => x.screenCanvas.Equals(defaultOnCanvas)).screenType);
            }
        }
    }

    public enum ScreenType
    {
        Mainmenu, Gameplay, Gameover
    }






    [System.Serializable]
    public class ScreenTypePair
    {
        public ScreenTypePair(Screen screen, ScreenType screenType)
        {
            this.screen = screen;
            this.screenType = screenType;
        }

        public Screen screen;
        public ScreenType screenType;
    }

    [System.Serializable]
    public class ScreenData
    {
        public ScreenType screenType;
        public Canvas screenCanvas;

        [Header("Optional"), Space(5)]
        public RectTransform rect;
        public ShowAnimation showAnimation;
        public HideAnimation hideAnimation;
    }
}