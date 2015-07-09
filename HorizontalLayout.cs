using RSG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityEditorUI
{
    /// <summary>
    /// Lays widgets out horizontally.
    /// </summary>
    internal class HorizontalLayout : AbstractLayout
    {
        public HorizontalLayout(ILayout parent) :
            base(parent)
        {
            Argument.NotNull(() => parent);
        }

        public override void OnGUI()
        {
            if (!enabled)
            {
                return;
            }

            GUILayout.BeginHorizontal();
            base.OnGUI();
            GUILayout.EndHorizontal();
        }
    }
}
