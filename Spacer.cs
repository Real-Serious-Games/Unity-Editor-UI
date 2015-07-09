using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

namespace UnityEditorUI
{
    /// <summary>
    /// Inserts a space between other widgets.
    /// </summary>
    interface ISpacer : IWidget
    {

    }

    /// <summary>
    /// Inserts a space between other widgets.
    /// </summary>
    class Spacer : AbstractWidget, ISpacer
    {
        internal Spacer(ILayout parent) : base(parent)
        {

        }

        public override void OnGUI()
        {
            EditorGUILayout.Space();
        }

        public override void BindViewModel(object viewModel)
        {

        }
    }
}
