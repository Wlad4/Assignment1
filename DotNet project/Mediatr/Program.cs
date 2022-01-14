using System;

namespace Mediatr
{
    class Program
    {
        static void Main(string[] args)
        {
            // инициализация
            var button = new Button();
            var radioButton = new RadioButton();
            var textBox = new TextBox();

            var mediatr = new Mediatr(button, radioButton, textBox);

            button.SetMediatr(mediatr);
            radioButton.SetMediatr(mediatr);
            textBox.SetMediatr(mediatr);

            // имитация действий пользователя
            textBox.Click();
            Console.WriteLine("\n\n");

            radioButton.Click();
            Console.WriteLine("\n\n");

            button.Click();
            Console.WriteLine("\n\n");
        }
    }
}
