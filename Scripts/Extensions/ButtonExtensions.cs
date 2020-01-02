using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEngine.UI {
    
    public static class ButtonExtensions {
    
        public static void UpdateText(this Button button, string text) {
            if (button != null) {
                Text textControl = button.transform.GetComponentInChildren<Text>();
                if (textControl != null)
                    textControl.text = text; 
            }
        }

        public static void UpdateTextColor(this Button button, Color color) {
            if (button != null) {
                Text textControl = button.transform.GetComponentInChildren<Text>();
                if (textControl != null)
                    textControl.color = color; 
            }
        }
    }
}
