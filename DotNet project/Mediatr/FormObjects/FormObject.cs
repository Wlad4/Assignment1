namespace Mediatr
{
    public abstract class FormObject
    {
        protected IMediatr mediatr;

        public void SetMediatr(IMediatr mediatr)
        {
            this.mediatr = mediatr;
        }

        public abstract void Click();
    }
}
