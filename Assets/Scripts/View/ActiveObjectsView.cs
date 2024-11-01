public class ActiveObjectsView : TextView
{
    private void OnEnable()
    {
        Counters.ActiveObjectsCounterChanged += ShowCounter;
    }

    private void OnDisable()
    {
        Counters.ActiveObjectsCounterChanged -= ShowCounter;
    }
}
