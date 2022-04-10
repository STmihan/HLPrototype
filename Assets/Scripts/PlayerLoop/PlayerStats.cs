using System;
using UnityEngine;

namespace PlayerLoop
{
    [Serializable]
    public class PlayerStats
    {
        [SerializeField] private float _speed;
        public float Speed => _speed;
    }
}