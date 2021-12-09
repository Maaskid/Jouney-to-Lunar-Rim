using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvent
{
    [CreateAssetMenu(fileName = "New GameEvent", menuName = "Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly IList<GameEventListener> _events = new List<GameEventListener>();

        public void Raise()
        {
            for (int i = _events.Count - 1; i >= 0; i--)
            {
                _events[i].OnEventRaised();
            }
        }

        public void Register(GameEventListener gameEventListener)
        {
            _events.Add(gameEventListener);
        }

        public void Unregister(GameEventListener gameEventListener)
        {
            _events.Remove(gameEventListener);
        }
    }
}
