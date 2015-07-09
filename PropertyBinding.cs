using RSG.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace UnityEditorUI
{
    /// <summary>
    /// Binds a widget's property to a property in the external view model.
    /// </summary>
    public interface IPropertyBinding<ValueT, WidgetT>
    {
        /// <summary>
        /// Configure the property to bind to later.
        /// </summary>
        WidgetT Bind(string propertyName);

        /// <summary>
        /// Configure the property to bind to later.
        /// </summary>
        WidgetT Bind(Expression<Func<ValueT>> propertyExpression);
        
        /// <summary>
        /// Set the value of the property directly (only used in initial setup)
        /// </summary>
        WidgetT Value(ValueT propertyValue);
    }

    /// <summary>
    /// Binds a widget's property to a property in the external view model.
    /// </summary>
    internal class PropertyBinding<ValueT, WidgetT> : IPropertyBinding<ValueT, WidgetT>
    {
        // Used in fluent API so that Bind and Value methods can return the parent widget and thus be chained together
        WidgetT parentWidget;
        Action<ValueT> onViewModelUpdated;

        private string boundPropertyName;
        private object viewModel;
        private PropertyInfo boundProperty;

        public void BindViewModel(object newViewModel)
        {
            viewModel = newViewModel;
            if (!String.IsNullOrEmpty(boundPropertyName))
            {
                var viewModelType = newViewModel.GetType();
                boundProperty = viewModelType.GetProperty(boundPropertyName);
                if (boundProperty == null)
                {
                    throw new ApplicationException("Expected property " + boundPropertyName + " not found on type " + viewModelType.Name + ".");
                }

                // Update the widget with the initial value from the bound property.
                var widgetValue = GetValueFromViewModel();
                UpdateWidget(widgetValue);

                // Bind the property so that the widget gets updated when the view model changes
                var notifyPropertyChanged = viewModel as INotifyPropertyChanged;
                if (notifyPropertyChanged != null)
                {
                    notifyPropertyChanged.PropertyChanged += viewModel_PropertyChanged;
                }
            }
        }

        void viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == boundPropertyName)
            {
                var widgetValue = GetValueFromViewModel();
                UpdateWidget(widgetValue);
            }
        }

        /// <summary>
        /// Gets the value from the property in the bound view model.
        /// </summary>
        private ValueT GetValueFromViewModel()
        {
            if (boundProperty == null)
            {
                return default(ValueT);
            }

            var viewModelValue = boundProperty.GetValue(viewModel, null);
            try
            {
                return (ValueT)viewModelValue;
            }
            catch (InvalidCastException)
            {
                //Logger.LogError(ex, "todo")
                Debug.LogError("Failed to cast view model value of type " + viewModelValue.GetType().Name + " to " + typeof(ValueT).Name);
            }

            return default(ValueT);
        }

        /// <summary>
        /// Create the PropertyBinding with a reference to the widget using it and an action to be called when the external view model changes.
        /// </summary>
        internal PropertyBinding(WidgetT parentWidget, Action<ValueT> onViewModelUpdated)
        {
            this.parentWidget = parentWidget;
            this.onViewModelUpdated = onViewModelUpdated;
        }

        /// <summary>
        /// Update the parent widget when the value of the property is changed.
        /// </summary>
        internal void UpdateWidget(ValueT newValue)
        {
            onViewModelUpdated(newValue);
        }

        /// <summary>
        /// Updates the bound view model when the value is changed by the widget.
        /// </summary>
        internal void UpdateView(ValueT newValue)
        {
            if (viewModel != null && boundProperty != null)
            {
                boundProperty.SetValue(viewModel, newValue, null);
            }
        }

        /// <summary>
        /// Binds this PropertyBinding to an external property.
        /// </summary>
        public WidgetT Bind(string propertyName)
        {
            Argument.StringNotNullOrEmpty(() => propertyName);

            boundPropertyName = propertyName;

            return parentWidget;
        }

        /// <summary>
        /// Binds this PropertyBinding to an external property.
        /// </summary>
        public WidgetT Bind(Expression<Func<ValueT>> propertyExpression)
        {
            Argument.NotNull(() => propertyExpression);

            return Bind(GetPropertyName(propertyExpression));
        }

        /// <summary>
        /// Get the string name of a property.
        /// </summary>
        private static string GetPropertyName(Expression<Func<ValueT>> propertyExpression)
        {
            var expr = (MemberExpression)propertyExpression.Body;
            return expr.Member.Name;
        }

        /// <summary>
        /// Permanently set the value of this PropertyBinding
        /// </summary>
        public WidgetT Value(ValueT propertyValue)
        {
            UpdateWidget(propertyValue);

            return parentWidget;
        }
    }
}
