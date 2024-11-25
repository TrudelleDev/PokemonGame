using UnityEngine;
using System;

// Source : https://discussions.unity.com/t/650579
namespace PokemonGame.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class DrawIfAttribute : PropertyAttribute
    {
        public string ComparedPropertyName { get; private set; }
        public object ComparedValue { get; private set; }
        public DisablingType DisablingType { get; private set; }

        public DrawIfAttribute(string comparedPropertyName, object comparedValue, DisablingType disablingType = DisablingType.DontDraw)
        {
            ComparedPropertyName = comparedPropertyName;
            ComparedValue = comparedValue;
            DisablingType = disablingType;
        }
    }
}
