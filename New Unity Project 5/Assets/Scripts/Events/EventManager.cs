using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


[System.Serializable]
public class EventsArgs : UnityEvent<object>
{

}


public class EventManager : MonoBehaviour
{

	private Dictionary<string, EventsArgs> eventDictionary;
	private static EventManager eventManagerObject;

	public static EventManager instance
	{
		get
		{
			if (!eventManagerObject)
			{
				eventManagerObject = FindObjectOfType(typeof(EventManager)) as EventManager;

				if (!eventManagerObject)
				{
					Debug.LogError("Put an active Event Manager into the scene");
				}
				else
				{
					eventManagerObject.Init();
				}
			}
			return eventManagerObject;
		}
	}

	void Init()
	{
		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<string, EventsArgs>();
		}
	}

	public static void StartListening(string eventName, UnityAction<object> listener)
	{
		EventsArgs thisEvent = null;
		if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.AddListener(listener);
		}
		else
		{
			thisEvent = new EventsArgs();
			thisEvent.AddListener(listener);
			instance.eventDictionary.Add(eventName, thisEvent);
		}
	}

	public static void StopListening(string eventName, UnityAction<object> listener)
	{
		if (eventManagerObject == null) return;

		EventsArgs thisEvent = null;
		if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.RemoveListener(listener);
		}
	}

	public static void RemoveAllListerners()
	{
		instance.eventDictionary.Clear();
	}

	public static void TriggerEvent(string eventName, object e)
	{
		EventsArgs thisEvent = null;
		if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.Invoke(e);
		}
	}
}