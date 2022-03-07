using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Eventler
{
    // Delegate tanýmlamasý
    public delegate void objeAc();

    // Delegate objesi oluþturmak
    public  static objeAc resetgame; 



    public static void resettgame()
    {
        resetgame?.Invoke();
      
    }
}
