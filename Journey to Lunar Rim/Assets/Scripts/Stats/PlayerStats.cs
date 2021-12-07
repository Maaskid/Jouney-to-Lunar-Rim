using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "New Player Stats")]
    public class PlayerStats : ScriptableObject, ISerializationCallbackReceiver
    {
        public float tankInit;
        [NonSerialized] public float TankRuntime;
        
        public float schadenInit;
        public float schadenMax;
        [NonSerialized] public float SchadenRuntime;
        

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            TankRuntime = tankInit;
            SchadenRuntime = schadenInit;
        }
    }
}