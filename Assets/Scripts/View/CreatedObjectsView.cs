public class CreatedObjectsView : TextView
{
    private void OnEnable()
    {
        Counters.CreatedObjectsCounterChanged += ShowCounter;
    }

    private void OnDisable()
    {
        Counters.CreatedObjectsCounterChanged -= ShowCounter;
    }
}
