using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEditorUI
{
    /// <summary>
    /// Base GUI class. Creates and keeps track of the root of the widget stack, which can then be used to add new widgets.
    /// </summary>
    public class GUI
    {
        private RootLayout root;

        public GUI()
        {
            root = new RootLayout();
        }

        /// <summary>
        /// Updates the UI and processes events. Should be called in the unity OnGUI function.
        /// </summary>
        public void OnGUI()
        {
            root.OnGUI();
        }

        /// <summary>
        /// Returns the root layout of the UI.
        /// </summary>
        public ILayout Root()
        {
            return root;
        }

        /// <summary>
        /// Binds the GUI to a view model by using reflection to find properties and methods that match 
        /// the strings set in PropertyBindings in widgets.
        /// </summary>
        public void BindViewModel(object viewModel)
        {
            root.BindViewModel(viewModel);
        }
    }
}
