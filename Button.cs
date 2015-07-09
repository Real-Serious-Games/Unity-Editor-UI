using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityEditorUI
{
    /// <summary>
    /// Clickable push button widget.
    /// </summary>
    public interface IButton : IWidget
    {
        /// <summary>
        /// Text to be displayed on the button.
        /// </summary>
        IPropertyBinding<string, IButton> Text { get; }

        /// <summary>
        /// Tooltip displayed on mouse hover.
        /// </summary>
        IPropertyBinding<string, IButton> Tooltip { get; }

        /// <summary>
        /// Width of the widget in pixels. Default uses auto-layout.
        /// </summary>
        IPropertyBinding<int, IButton> Width { get; }

        /// <summary>
        /// Height of the widget in pixels. Default uses auto-layout.
        /// </summary>
        IPropertyBinding<int, IButton> Height { get; }

        /// <summary>
        /// Event invoked when the button is clicked.
        /// </summary>
        IEventBinding<IButton> Click { get; }
    }

    /// <summary>
    /// Clickable push button widget.
    /// </summary>
    internal class Button : AbstractWidget, IButton
    {
        // Private members
        private string text = String.Empty;
        private string tooltip = String.Empty;
        private int width = -1;
        private int height = -1;

        // Concrete property bindings
        private PropertyBinding<string, IButton> textProperty;
        private PropertyBinding<string, IButton> tooltipProperty;
        private PropertyBinding<int, IButton> widthProperty;
        private PropertyBinding<int, IButton> heightProperty;
        private EventBinding<IButton> clickEvent;

        // Public interfaces for getting PropertyBindings
        public IPropertyBinding<string, IButton> Text { get { return textProperty; } }
        public IPropertyBinding<string, IButton> Tooltip { get { return tooltipProperty; } }
        public IPropertyBinding<int, IButton> Width { get { return widthProperty; } }
        public IPropertyBinding<int, IButton> Height { get { return heightProperty; } }
        public IEventBinding<IButton> Click { get { return clickEvent; } }

        internal Button(ILayout parent) : base(parent) 
        {
            textProperty = new PropertyBinding<string, IButton>(
                this,
                value => this.text = value
            );

            tooltipProperty = new PropertyBinding<string, IButton>(
                this,
                value => this.tooltip = value
            );

            widthProperty = new PropertyBinding<int, IButton>(
                this,
                value => this.width = value
            );

            heightProperty = new PropertyBinding<int,IButton>(
                this,
                value => this.height = value
            );

            clickEvent = new EventBinding<IButton>(this);
        }

        public override void OnGUI()
        {
            var layoutOptions = new List<GUILayoutOption>();
            if(width >= 0)
            {
                layoutOptions.Add(GUILayout.Width(width));
            }
            if(height >= 0)
            {
                layoutOptions.Add(GUILayout.Height(height));
            }

            if (GUILayout.Button(new GUIContent(text, tooltip), layoutOptions.ToArray()))
            {
                clickEvent.Invoke();
            }
        }

        public override void BindViewModel(object viewModel)
        {
            textProperty.BindViewModel(viewModel);
            tooltipProperty.BindViewModel(viewModel);
            widthProperty.BindViewModel(viewModel);
            heightProperty.BindViewModel(viewModel);
            clickEvent.BindViewModel(viewModel);
        }
    }
}
