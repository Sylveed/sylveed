namespace Sylveed.SampleApp.Sample.Application.Dialogs
{
    public interface IDialogPresenter
    {
        void Alert(string message);
        bool Confirm(string message);
        void Message(string message);
    }
}