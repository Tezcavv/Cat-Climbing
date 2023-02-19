using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFactory : MonoBehaviour
{
    [SerializeField]
    private static IController instance;

   public static IController GetController() {

        if (instance != null) {
            return instance;
        }

        if (SystemInfo.deviceType == DeviceType.Desktop) {
            instance = new GameObject("Controller").AddComponent<ControllerPc>();            
        }
        else {
            instance = new GameObject("Controller").AddComponent<ControllerMobile>();

        }

        return instance;

    }


   
}
