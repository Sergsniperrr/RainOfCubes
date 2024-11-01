public class SpawnedObjectsView : TextView
{
    private void OnEnable()
    {
        Counters.SpawnedObjectsCounterChanged += ShowCounter;
    }

    private void OnDisable()
    {
        Counters.SpawnedObjectsCounterChanged -= ShowCounter;
    }
}
