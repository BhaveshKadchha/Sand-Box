using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PopUpManager : Singleton<PopUpManager>
    {
        public PopUpData simplePopUpData;
        public PopUpData globalMessagePopupData;
        public Text globalMessageText;

        public PopUpData globalWarningPopupData;
        public Text globalWarningText;
        public Button acceptButton;


        public SimplePopUp simplePopUp;
        public GlobalMessagePopUp globalMessagePopUp;
        public GlobalWarningPopUp globalWarningPopUp;


        private void Start()
        {
            simplePopUp = new SimplePopUp(simplePopUpData.mainCanvs, simplePopUpData.rect, simplePopUpData.showAnimation, simplePopUpData.hideAnimation, simplePopUpData.closeButton, this);
            globalMessagePopUp = new GlobalMessagePopUp(globalMessagePopupData.mainCanvs, globalMessagePopupData.rect, globalMessagePopupData.showAnimation, globalMessagePopupData.hideAnimation, globalMessagePopupData.closeButton, this, globalMessageText);
            globalWarningPopUp = new GlobalWarningPopUp(globalWarningPopupData.mainCanvs, globalWarningPopupData.rect, globalWarningPopupData.showAnimation, globalWarningPopupData.hideAnimation, globalWarningPopupData.closeButton, this, globalWarningText, acceptButton);
        }

        public void ShowSimplePopUP()
        {
            simplePopUp.Show(this);
        }

        public void ShowGlobalMessagePopUp(string text)
        {
            globalMessagePopUp.Show(this, text);
        }

        public void ShowGlobalWarningPopUp(string text, System.Action action)
        {
            globalWarningPopUp.Show(this, text, action, true);
        }
    }

    [System.Serializable]
    public class PopUpData
    {
        public Canvas mainCanvs;
        public ShowAnimation showAnimation;
        public HideAnimation hideAnimation;
        public RectTransform rect;
        public Button closeButton;
    }
}