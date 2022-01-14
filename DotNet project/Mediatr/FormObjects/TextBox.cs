using System;

namespace Mediatr
{
    public class TextBox : FormObject
    {
        public override void Click()
        {
            var message = "Сursor is placed in the TextBox field";
            Console.WriteLine(message);
            mediatr.EventHandler(this, message);
        }

        public void RemoveCursor()
        {
            var message = "Сursor is removed from the TextBox field";
            Console.WriteLine(message);
        }
    }
}
