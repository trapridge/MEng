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

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void explanationToggle_Click(object sender, RoutedEventArgs e)
        {
            // Toggle explanations
            if (publicExplanation.Visibility == Visibility.Collapsed && privateExplanation.Visibility == Visibility.Collapsed)
            {
                publicExplanation.Visibility = Visibility.Visible;
                privateExplanation.Visibility = Visibility.Visible;
                explanationToggle.Content = "Hide explanation";
            }
            else
            {
                publicExplanation.Visibility = Visibility.Collapsed;
                privateExplanation.Visibility = Visibility.Collapsed;
                explanationToggle.Content = "Show explanation";
            }
        }

        private void privateRadio_Checked(object sender, RoutedEventArgs e)
        {
            privateWarningBlock.Visibility = Visibility.Visible;
        }

        private void privateRadio_Unchecked(object sender, RoutedEventArgs e)
        {
            privateWarningBlock.Visibility = Visibility.Collapsed;
        }

        private void lightCheck_Checked(object sender, RoutedEventArgs e)
        {
            lightNoteBlock.Visibility = Visibility.Visible;
        }

        private void lightCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            lightNoteBlock.Visibility = Visibility.Collapsed;
        }
    }
}