namespace Game.UISystem
{
    class Screens : BaseUI
    {
        protected override void Awake()
        {
            base.Awake();

            if (!content)
                content = transform.GetChild(0).gameObject;
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