using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class PaintSplash : MonoBehaviour
{
    [SerializeField]
    private IActivatable[] _iActivatable;

    public void start()
    {
        foreach (IActivatable item in _iActivatable)
        {
            item.Activate();
        }
    }
}

