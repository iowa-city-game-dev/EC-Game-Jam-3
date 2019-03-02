using UnityEngine;
using Event = Managers.Event;

namespace Utility
{
    public class Observer : MonoBehaviour
    {
        public virtual void OnNotify(Event @event)
        {
        }
    }
}