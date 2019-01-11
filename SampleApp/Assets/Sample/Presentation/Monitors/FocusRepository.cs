namespace Sylveed.SampleApp.Sample.Presentation.Monitors
{
    public class FocusRepository
    {
        IFocusTarget current;

        public IFocusTarget CurrentFocus => current;

        public void Focus(IFocusTarget target)
        {
            current?.KillFocus();

            target.Focus();
            current = target;
        }

        public void KillFocus()
        {
            current?.KillFocus();
            current = null;
        }
    }
}