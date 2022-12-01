using System;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace GZKL.Client.UI.Common
{
    /// <summary>
    /// Provides additional properties to use against PasswordBox controls.
    /// </summary>
    /// <remarks>
    /// Sometimes controls have properties that don't support binding. PasswordBox is one of these, where Microsoft's implementation does not support binding against the entered
    /// password (for 'security purposes').
    ///
    /// This class provides an 'attached property' which allows us to bridge the gap between the view and view model.
    /// </remarks>
    public static class Password
    {
        #region Dependency properties

        /// <summary>
        /// SecurePassword property.
        /// </summary>
        public static readonly DependencyProperty SecurePasswordProperty = DependencyProperty.RegisterAttached("SecurePassword", typeof(SecureString), typeof(Password), new FrameworkPropertyMetadata(OnSecurePasswordChanged));

        /// <summary>
        /// HasSecurePassword property.
        /// </summary>
        private static readonly DependencyProperty HasSecurePasswordProperty = DependencyProperty.RegisterAttached("HasSecurePassword", typeof(bool), typeof(Password));

        #endregion

        #region Methods

        /// <summary>
        /// Sets the value of the SecurePassword property.
        /// </summary>
        /// <param name="dependencyObject">The object to set the property for.</param>
        /// <param name="value">The value to set.</param>
        public static void SetSecurePassword(DependencyObject dependencyObject, SecureString value)
        {
            if (dependencyObject == null)
                throw new ArgumentNullException(nameof(dependencyObject));

            dependencyObject.SetValue(Password.SecurePasswordProperty, value);
        }

        /// <summary>
        /// Gets the value of the SecurePassword property.
        /// </summary>
        /// <param name="dependencyObject">The object to get the property for.</param>
        /// <returns>The current value.</returns>
        public static SecureString GetSecurePassword(DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
                throw new ArgumentNullException(nameof(dependencyObject));

            return (SecureString)dependencyObject.GetValue(Password.SecurePasswordProperty);
        }

        #endregion

        /// <summary>
        /// Handles the SecurePassword value changing.
        /// </summary>
        private static void OnSecurePasswordChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox password = dependencyObject as PasswordBox;

            bool? isRegistered = (bool?)password?.GetValue(Password.HasSecurePasswordProperty);
            if (isRegistered == false)
            {
                // register with the PasswordBox's PasswordChanged event so that we can keep updated with the latest value entered by the user
                password.PasswordChanged += (s, ee) => SetSecurePassword(password, password.SecurePassword);
                password.SetValue(Password.HasSecurePasswordProperty, true);
            }
        }
    }
}
