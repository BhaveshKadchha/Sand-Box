using UnityEngine;
using UnityEngine.UI;

namespace Game.UISystem
{
    class GlobalPopUpWarning : Popup
    {
        [SerializeField] Text messageText;              // Text field for message to display in popup.
        [SerializeField] Button _acceptButton;             // Buy button reference of this popup.

        public override void Hide()
        {
            base.Hide();
        }

        public override void Show()
        {
            base.Show();
        }

        /// <summary>
        /// Method to set Data in Popup callback.
        /// </summary>
        public void Show(string message, System.Action actionOne)
        {
            messageText.text = message;

            _acceptButton.onClick.RemoveAllListeners();
            _acceptButton.onClick.AddListener(() =>
            {
                actionOne();
                Hide();
            });

            base.Show();
        }
    }
}