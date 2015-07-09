using RSG;
using RSG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityEditorUI
{
    /// <summary>
    /// Root layout - same as VerticalLayout except that it does not contain a parent.
    /// </summary>
    internal class RootLayout : AbstractLayout
    {
        internal RootLayout() : 
            base(null) 
        {
        }

        public override void OnGUI()
        {
            GUILayout.BeginVertical();
            base.OnGUI();
            GUILayout.EndVertical();
        }
    }
}
