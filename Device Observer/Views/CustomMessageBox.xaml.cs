using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Device_Observer.Views
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string message, bool withYesNoButtons = true)
        {
            InitializeComponent();
            TextBlock messageTextBlock = TextBlock;
            if (messageTextBlock != null)
            {
                messageTextBlock.Text = message;
            }
            if (!withYesNoButtons)
            {
                ButonPanel.Visibility = Visibility.Collapsed;
                OkButton.Visibility = Visibility.Visible;
            }
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public static bool Show(string message, bool withYesNoButtons = true)
        {
            CustomMessageBox customMessage = new CustomMessageBox(message, withYesNoButtons);
            customMessage.ShowDialog();
            return customMessage.DialogResult == true;
        }
    }
}
