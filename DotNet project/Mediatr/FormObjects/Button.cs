using System;

namespace Mediatr
{
    public class Button : FormObject
    {
        // События
        public override void Click()
        {
            var message = "Button submit data from form";
            Console.WriteLine(message);
            mediatr.EventHandler(this, message);
        }

        // Изменение состояния
        public void MakeActive()
        {
            var message = "Button is active";
            Console.WriteLine(message);
        }

        public void MakeDisable()
        {
            var message = "Button is disabled";
            Console.WriteLine(message);
        }
    }
}
