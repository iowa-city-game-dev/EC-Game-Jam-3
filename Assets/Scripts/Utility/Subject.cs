using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    public List<Observer> observers = new List<Observer>();
    int numOfObservers;

  public void addObserver(Observer observer)
    {
        observers.Add(observer);
    }

    public void removeObserver(Observer obj)
    {
        observers.Remove(obj);
    }

    public void notify(Event stuff){
        foreach (var thing in observers)
        {
            thing.OnNotify(stuff);
        }
    }
}
