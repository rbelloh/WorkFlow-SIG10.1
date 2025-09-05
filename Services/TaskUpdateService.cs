namespace WorkFlow_SIG10._1.Services
{
    public class TaskUpdateService
    {
        public event Action OnTaskDataChanged;

        public void NotifyTaskDataChanged()
        {
            OnTaskDataChanged?.Invoke();
        }
    }
}
