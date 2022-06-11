using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGP141
{
    public interface IObserver
    {
        void OnNotify(string eventId);
    }
}