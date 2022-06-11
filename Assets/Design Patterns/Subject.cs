using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGP141
{
    public abstract class Subject : MonoBehaviour
    {
        private List<IObserver> _observers;

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        protected virtual void Awake()
        {
            _observers = new List<IObserver>();
        }

        protected void Notify(string eventId)
        {
            foreach (IObserver observer in _observers)
            {
                observer.OnNotify(eventId);
            }
        }
    }
}