using UnityEngine;
using Zenject;

namespace DrebotGS
{
    public class DemoScript 
    {
        public int Value { get; private set; }

        public void IncreaseValue()
        {
            Value++;
        }
    }
}