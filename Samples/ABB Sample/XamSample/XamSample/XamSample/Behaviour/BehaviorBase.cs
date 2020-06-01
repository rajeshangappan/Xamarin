using System;
using Xamarin.Forms;

namespace EventToCommandBehavior
{
    /// <summary>
    /// Defines the <see cref="BehaviorBase{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class BehaviorBase<T> : Behavior<T> where T : BindableObject
    {
        #region PUBLIC_PPTY

        /// <summary>
        /// Gets the AssociatedObject.
        /// </summary>
        public T AssociatedObject { get; private set; }

        #endregion

        /// <summary>
        /// The OnAttachedTo.
        /// </summary>
        /// <param name="bindable">The bindable<see cref="T"/>.</param>
        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;

            if (bindable.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        /// <summary>
        /// The OnDetachingFrom.
        /// </summary>
        /// <param name="bindable">The bindable<see cref="T"/>.</param>
        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;
        }

        /// <summary>
        /// The OnBindingContextChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        internal void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        /// <summary>
        /// The OnBindingContextChanged.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}
