using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControllerFactory {
    private static IController instance;

    public static IController Instance {
        get {

            if (instance != null) {
                return instance;
            }

            if (SystemInfo.deviceType == DeviceType.Desktop) {
                instance = new ControllerPc();
            } else {
                instance = new ControllerMobile();
            }
            return instance;
        }
    }
}
