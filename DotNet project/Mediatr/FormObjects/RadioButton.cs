using System;

namespace Mediatr
{
    public class RadioButton : FormObject
    {
        public override void Click()
        {
            var message = "RadioButton has no reaction";
            Console.WriteLine(message);
            mediatr.EventHandler(this, message);
        }
    }
}
