using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Game.UI
{
    public class ButtonClick : MonoBehaviour
    {
        [SerializeField] Button mainmenuButton;
        [SerializeField] Button gameplayButton;
        [SerializeField] Button gameoverButton;
        [Space(10)]
        [SerializeField] Button simplePopup;
        [SerializeField] Button globalMessagePopup;
        [SerializeField] Button globalWarningPopup;


        private void Start()
        {
            mainmenuButton.onClick.AddListener(() => ScreenManager.Instance.Show(ScreenType.Gameplay));
            gameplayButton.onClick.AddListener(() => ScreenManager.Instance.Show(ScreenType.Gameover));
            gameoverButton.onClick.AddListener(() => ScreenManager.Instance.Show(ScreenType.Mainmenu));

            simplePopup.onClick.AddListener(() => PopUpManager.Instance.ShowSimplePopUP());
            globalMessagePopup.onClick.AddListener(() => PopUpManager.Instance.ShowGlobalMessagePopUp("Display this Bro"));
            globalWarningPopup.onClick.AddListener(() => PopUpManager.Instance.ShowGlobalWarningPopUp("AND THEN THIS", () => Debug.Log("Accept Clicked")));

        }
    }
}
