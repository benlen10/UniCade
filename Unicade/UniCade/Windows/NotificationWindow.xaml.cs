﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Threading;

/// <summary>
/// Interaction logic for Notification.xaml
/// </summary>
// ReSharper disable once CheckNamespace
public partial class NotificationWindow
{
    #region Constructors

    /// <summary>
    /// Public constructor for the NotificationWindow 
    /// </summary>
    /// <param name="titleText">Heading text for the nofication</param>
    /// <param name="bodyText"> Body text for the notification</param>
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public NotificationWindow(string titleText, string bodyText)
    {
        InitializeComponent();

        Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                TextBlock11.Text = titleText;
                TextBlock0.Text = bodyText;
                var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
                var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));

                Left = corner.X - ActualWidth - 100;
                Top = corner.Y - ActualHeight;
            }));
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Called once the notification animation is completed and closes the window
    /// </summary>
    private void DoubleAnimationCompleted(object sender, EventArgs e)
    {
        Close();
    }

    #endregion

}

