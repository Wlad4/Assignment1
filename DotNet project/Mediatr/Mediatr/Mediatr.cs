namespace Mediatr
{
    public class Mediatr : IMediatr
    {
        private readonly Button button;
        private readonly RadioButton radioButton;
        private readonly TextBox textBox;

        public Mediatr(Button button, RadioButton radioButton, TextBox textBox)
        {
            this.button = button;
            this.radioButton = radioButton;
            this.textBox = textBox;
        }

        public void EventHandler(object sender, string message)
        {
            if (sender is Button)
            {
                textBox.RemoveCursor();
                button.MakeDisable();
            }
            if (sender is RadioButton)
            {
                textBox.RemoveCursor();
            }
            if (sender is TextBox)
            {
                button.MakeActive();
            }
        }
    }
}
