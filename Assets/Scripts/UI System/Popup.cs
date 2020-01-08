namespace Game.UISystem
{
    class Popup : BaseUI
    {
        protected override void Awake()
        {
            base.Awake();

            if (!content)
                content = transform.GetChild(1).gameObject;
        }

        public override void Show()
        {
            base.Show();
        }
        public override void Hide()
        {
            base.Hide();
        }
    }
}