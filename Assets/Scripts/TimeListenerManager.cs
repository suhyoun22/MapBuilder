using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class TimeListenerManager : MonoBehaviour
{
    private async void Start()
    {
        await WaitForTimeManagerAsync();

        var listeners = FindObjectsOfType<MonoBehaviour>().OfType<ITimeListener>();
        foreach (var listener in listeners)
        {
            listener.Register(TimeManager.Instance);
        }
    }

    private async Task WaitForTimeManagerAsync()
    {
        while (TimeManager.Instance == null)
        {
            await Task.Yield();
        }
    }

    private void OnDisable()
    {
        if (TimeManager.Instance == null) return;

        var listeners = FindObjectsOfType<MonoBehaviour>().OfType<ITimeListener>();
        foreach (var listener in listeners)
        {
            listener.Unregister(TimeManager.Instance);
        }
    }
}
