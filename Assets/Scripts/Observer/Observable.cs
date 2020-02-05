using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observable<T> : MonoBehaviour
{
    static readonly Observable<T> instance;
    readonly List<ObserverData<T>> observers = new List<ObserverData<T>>();
    private Dictionary<string, T> properties = new Dictionary<string, T>();

    public T GetValue(string key) {
		return properties[key];
	}

    public void SetValue(string key, T value = default, bool allowEqualValues = false) {
        //add allowEqualValues condition
        if (properties.ContainsKey(key))
        {
            properties[key] = value;
        }
        else
        {
            properties.Add(key, value);
        }

		CallObserversFrom(key, value);
    }

    public Action AddObserver(ObserverCallback<T> callback, string key, T value = default(T)) {
		return AddObserverTo(callback, key, value);
	}

    public void RemoveObservers(ObserverCallback<T> callback, string key, T value = default(T)) {
    	RemoveObserversFrom(callback, key, value);
    }
    
    Action AddObserverTo(ObserverCallback<T> callback, string key, T value) {
		if (callback != null) {
			observers.Add(new ObserverData<T>(callback, key, value));
		}

        return () => { RemoveObserversFrom(callback, key, value); };
    }

    void CallObserversFrom(string key, T value) {
		for (var i = observers.Count - 1; i >= 0; i--) {
			var observer = observers[i];
            if (observer.Key != null && observer.Key != key)
                continue;
            observer.Callback(value, key);
		}
	}

    void RemoveObserversFrom(ObserverCallback<T> callback, string key, T value) {
        for (var i = observers.Count - 1; i >= 0; i--)
        {
            var observer = observers[i];
            if (callback != observer.Callback)
            {
                continue;
            }
            else if (key != null && key != observer.Key)
            {
                continue;
            }
            else if (value != null && !EqualityComparer<T>.Default.Equals(value, observer.Value))/*value != observer.Value*/
            {
                continue;
            }
            observers.RemoveAt(i);
        }
    }
}
