using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Save_System
{    public interface ISaveable
    {
        object CaptureSate();
        void RestoreState(object state);
    }
}

