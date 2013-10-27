using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace UiConstruction
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void ExplanationToggle_Click(object sender, RoutedEventArgs e)
        {
            // Toggle explanations
            if (PublicExplanationBlock.Visibility == Visibility.Collapsed && PrivateExplanationBlock.Visibility == Visibility.Collapsed)
            {
                PublicExplanationBlock.Visibility = Visibility.Visible;
                PrivateExplanationBlock.Visibility = Visibility.Visible;
                ExplanationToggle.Content = L10N.ExplanationToggleHide;
            }
            else
            {
                PublicExplanationBlock.Visibility = Visibility.Collapsed;
                PrivateExplanationBlock.Visibility = Visibility.Collapsed;
                ExplanationToggle.Content = L10N.ExplanationToggleShow;
            }
        }

        // toggle warning
        private void PrivateRadio_Checked(object sender, RoutedEventArgs e)
        {
            PrivateWarningBlock.Visibility = Visibility.Visible;
        }
        private void PrivateRadio_Unchecked(object sender, RoutedEventArgs e)
        {
            PrivateWarningBlock.Visibility = Visibility.Collapsed;
        }
  
        // toggle note
        private void LightCheck_Checked(object sender, RoutedEventArgs e)
        {
            LightNoteBlock.Visibility = Visibility.Visible;
        }
        private void LightCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            LightNoteBlock.Visibility = Visibility.Collapsed;
        }

        // hadle 'login'
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameBox.Text.Length <= 0 || PasswordBox.Password.Length <= 0) 
            { 
                MessageBox.Show(L10N.LoginFailure);
            }
            else 
            {
                MessageBox.Show(L10N.LoginSuccess);
            }
        }


    }
}