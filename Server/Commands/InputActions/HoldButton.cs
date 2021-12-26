﻿using UnityEngine;

namespace JustUnityTester.Server.Commands {
    class HoldButton : AltUnityCommand {
        KeyCode keyCode;
        float power;
        float duration;

        public HoldButton(KeyCode keyCode, float power, float duration) {
            this.keyCode = keyCode;
            this.power = power;
            this.duration = duration;
        }

        public override string Execute() {
#if ALTUNITYTESTER
            AltUnityRunner._altUnityRunner.LogMessage("pressKeyboardKey: " + keyCode);
            var powerClamped = UnityEngine.Mathf.Clamp01(power);
            Input.SetKeyDown(keyCode, power, duration);
#endif      
            return "Ok";
        }
    }
}
