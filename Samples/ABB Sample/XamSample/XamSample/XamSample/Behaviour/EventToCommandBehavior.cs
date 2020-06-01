using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace EventToCommandBehavior
{
    /// <summary>
    /// Defines the <see cref="EventToCommandBehavior" />.
    /// </summary>
    public class EventToCommandBehavior : BehaviorBase<VisualElement>
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the CommandParameterProperty.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(EventToCommandBehavior), null);

        /// <summary>
        /// Defines the CommandProperty.
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(EventToCommandBehavior), null);

        /// <summary>
        /// Defines the EventNameProperty.
        /// </summary>
        public static readonly BindableProperty EventNameProperty = BindableProperty.Create("EventName", typeof(string), typeof(EventToCommandBehavior), null, propertyChanged: OnEventNameChanged);

        /// <summary>
        /// Defines the InputConverterProperty.
        /// </summary>
        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create("Converter", typeof(IValueConverter), typeof(EventToCommandBehavior), null);

        /// <summary>
        /// Defines the eventHandler.
        /// </summary>
        internal Delegate eventHandler;

        #endregion

        #region PUBLIC_PPTY

        /// <summary>
        /// Gets or sets the Command.
        /// </summary>
        public ICommand Command { get => (ICommand)GetValue(CommandProperty); set => SetValue(CommandProperty, value); }

        /// <summary>
        /// Gets or sets the CommandParameter.
        /// </summary>
        public object CommandParameter { get => GetValue(CommandParameterProperty); set => SetValue(CommandParameterProperty, value); }

        /// <summary>
        /// Gets or sets the Converter.
        /// </summary>
        public IValueConverter Converter { get => (IValueConverter)GetValue(InputConverterProperty); set => SetValue(InputConverterProperty, value); }

        /// <summary>
        /// Gets or sets the EventName.
        /// </summary>
        public string EventName { get => (string)GetValue(EventNameProperty); set => SetValue(EventNameProperty, value); }

        #endregion

        /// <summary>
        /// The OnAttachedTo.
        /// </summary>
        /// <param name="bindable">The bindable<see cref="VisualElement"/>.</param>
        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            RegisterEvent(EventName);
        }

        /// <summary>
        /// The OnDetachingFrom.
        /// </summary>
        /// <param name="bindable">The bindable<see cref="VisualElement"/>.</param>
        protected override void OnDetachingFrom(VisualElement bindable)
        {
            DeregisterEvent(EventName);
            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// The RegisterEvent.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        internal void RegisterEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            EventInfo eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);
            if (eventInfo == null)
            {
                throw new ArgumentException(string.Format("EventToCommandBehavior: Can't register the '{0}' event.", EventName));
            }
            MethodInfo methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");
            eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(AssociatedObject, eventHandler);
        }

        /// <summary>
        /// The DeregisterEvent.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        internal void DeregisterEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            if (eventHandler == null)
            {
                return;
            }
            EventInfo eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);
            if (eventInfo == null)
            {
                throw new ArgumentException(string.Format("EventToCommandBehavior: Can't de-register the '{0}' event.", EventName));
            }
            eventInfo.RemoveEventHandler(AssociatedObject, eventHandler);
            eventHandler = null;
        }

        /// <summary>
        /// The OnEvent.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="eventArgs">The eventArgs<see cref="object"/>.</param>
        internal void OnEvent(object sender, object eventArgs)
        {
            if (Command == null)
            {
                return;
            }

            object resolvedParameter;
            if (CommandParameter != null)
            {
                resolvedParameter = CommandParameter;
            }
            else if (Converter != null)
            {
                resolvedParameter = Converter.Convert(eventArgs, typeof(object), null, null);
            }
            else
            {
                resolvedParameter = eventArgs;
            }

            if (Command.CanExecute(resolvedParameter))
            {
                Command.Execute(resolvedParameter);
            }
        }

        /// <summary>
        /// The OnEventNameChanged.
        /// </summary>
        /// <param name="bindable">The bindable<see cref="BindableObject"/>.</param>
        /// <param name="oldValue">The oldValue<see cref="object"/>.</param>
        /// <param name="newValue">The newValue<see cref="object"/>.</param>
        internal static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (EventToCommandBehavior)bindable;
            if (behavior.AssociatedObject == null)
            {
                return;
            }

            string oldEventName = (string)oldValue;
            string newEventName = (string)newValue;

            behavior.DeregisterEvent(oldEventName);
            behavior.RegisterEvent(newEventName);
        }
    }
}
