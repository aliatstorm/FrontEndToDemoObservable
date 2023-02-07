using FrontEndToDemoObservable.ViewModels;

namespace FrontEndToDemoObservable.Services
{
    public class StateContainer
    {
        private AtcExamSessions _atcExamSessions;

        public AtcExamSessions AtcExamSessions
        {
            get => _atcExamSessions ?? new AtcExamSessions();
            set
            {
                _atcExamSessions = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
