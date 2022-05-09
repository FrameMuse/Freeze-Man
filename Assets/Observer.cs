using System;
using System.Collections.Generic;

public static class Observer
{
  private static List<ObserverListener<object>> listeners = new List<ObserverListener<object>>();
  public static Action Subscribe(object eventName, Action<object> action)
  {
    ObserverListener<object> listener = new ObserverListener<object>(eventName, action);
    listeners.Add(listener);
    return () =>
    {
      listeners.Remove(listener);
    };
  }

  public static void Dispatch(object eventName, object payload)
  {
    listeners.ForEach(listener =>
    {
      if (!listener.eventName.Equals(eventName)) return;
      listener.action(payload);
    });
  }
}

public class ObserverListener<T>
{
  public T eventName;
  public Action<object> action;

  public ObserverListener(T eventName, Action<object> action)
  {
    this.eventName = eventName;
    this.action = action;
  }
}
