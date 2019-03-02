using System.Collections.Generic;
using UnityEngine;
using Event = Managers.Event;

namespace Utility
{
    public class Subject : MonoBehaviour
    {
        public List<Observer> observers = new List<Observer>();
        int _numOfObservers;

        public void AddObserver(Observer observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(Observer obj)
        {
            observers.Remove(obj);
        }

        public void Notify(Event stuff)
        {
            foreach (var thing in observers)
            {
                thing.OnNotify(stuff);
            }
        }
    }
}