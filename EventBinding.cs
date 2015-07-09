using RSG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace UnityEditorUI
{
    /// <summary>
    /// Binds an event like a button click to a method in the view model.
    /// </summary>
    public interface IEventBinding<WidgetT>
    {
        /// <summary>
        /// Configure the event to bind to later.
        /// </summary>
        WidgetT Bind(string methodName);

        /// <summary>
        /// Configure the event to bind to later.
        /// </summary>
        WidgetT Bind(Expression<Action> methodExpression);
    }

    /// <summary>
    /// Binds an event like a button click to a method in the view model.
    /// </summary>
    internal class EventBinding<WidgetT> : IEventBinding<WidgetT>
    {
        WidgetT parentWidget;
        string boundMethodName;
        MethodInfo boundMethod;
        private object viewModel;

        public void BindViewModel(object newViewModel)
        {
            viewModel = newViewModel;
            if (!String.IsNullOrEmpty(boundMethodName))
            {
                Type viewModelType = newViewModel.GetType();
                boundMethod = viewModelType.GetMethod(boundMethodName);
                if (boundMethod == null)
                {
                    throw new ApplicationException("Expected method " + boundMethodName + " not found on type " + viewModelType.Name + ".");
                }
            }
        }

        /// <summary>
        /// Creates an EventBinding with a reference to the widget using it (used for API fluency)
        /// </summary>
        internal EventBinding(WidgetT parentWidget)
        {
            this.parentWidget = parentWidget;
        }

        /// <summary>
        /// Invokes the method bound to this EventBinding
        /// </summary>
        internal void Invoke()
        {
            if (boundMethod != null)
            {
                boundMethod.Invoke(viewModel, null);
            }
        }

        /// <summary>
        /// Configure the event to bind to later.
        /// </summary>
        public WidgetT Bind(string methodName)
        {
            Argument.StringNotNullOrEmpty(() => methodName);

            boundMethodName = methodName;

            return parentWidget;
        }

        /// <summary>
        /// Configure the event to bind to later.
        /// </summary>
        public WidgetT Bind(Expression<Action> methodExpression)
        {
            Argument.NotNull(() => methodExpression);

            return Bind(GetMethodName(methodExpression));
        }

        private static string GetMethodName(Expression<Action> methodExpression)
        {
            var expr = (MethodCallExpression)methodExpression.Body;
            return expr.Method.Name;
        }
    }
}
