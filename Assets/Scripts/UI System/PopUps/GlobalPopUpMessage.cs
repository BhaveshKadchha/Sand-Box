using UnityEngine.UI;
using UnityEngine;

namespace Game.UISystem
{
    class GlobalPopUpMessage : Popup
    {
        [SerializeField] Text _displayMessage;

        public override void Hide()
        {
            base.Hide();
        }

        public override void Show()
        {
            base.Show();
        }

        public void Show(string message)
        {
            _displayMessage.text = message;
            base.Show();
        }
    }
}