using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEditorUI
{
    /// <summary>
    /// Basic interface for all widgets.
    /// </summary>
    public interface IWidget
    {
        /// <summary>
        /// Returns ths widget's parent layout.
        /// </summary>
        ILayout End();
    }
}
