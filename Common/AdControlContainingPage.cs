#region Using

using InstanceFactoy.ConfigureAdControlSample.Configuration;
using InstanceFactoy.ConfigureAdControlSample.Xml.Serialization;
using Microsoft.Advertising.WinRT.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

#endregion Using


namespace InstanceFactoy.ConfigureAdControlSample.Common
{
  /// <summary>
  /// Base class for pages containing AdControls.
  /// </summary>
  public abstract class AdControlContainingPage : LayoutAwarePage
  {
    #region Private Methods

    /// <summary>
    /// Replaces the AdControls according to the settimgs of the configuration data.
    /// </summary>
    /// <param name="adControlConfigDataList">The list of AdControls to be created.</param>
    /// <param name="adApplicationId">The AdApplicationId to be set.</param>
    private void ReplaceAdControls
      (
      List<AdControlConfigData> adControlConfigDataList,
      string adApplicationId
      )
    {
      // iterate over the list of configured AdControls.
      foreach (AdControlConfigData configData in adControlConfigDataList)
      {
        // Find the AdControl to be replaced.
        AdControl oldAdControl = FindName(configData.AdControlName) as AdControl;

        // If the oldAdControl is not found, we can't replace it. So try the next one.
        if (oldAdControl == null)
        {
          Debug.WriteLine(String.Format("AdControl name >{0}< not found", configData.AdControlName));
          continue;
        }

        // Get the parent of the adcontrol. Needs to be a kind of panel.
        Panel parent = oldAdControl.Parent as Panel;

        // If the parent is not a panel, try the next one.
        if (parent == null)
        {
          Debug.WriteLine(String.Format("AdControl name >{0}< is not placed inside a panel or derived class. AdControl cannot be replaced.",
            configData.AdControlName));
          continue;
        }

        // Keep the position of the old AdControl.
        int postion = parent.Children.IndexOf(oldAdControl);

        // and remove it
        parent.Children.RemoveAt(postion);

        // Create the new AdControl with the correct ApplicationId and AdUnitId.
        AdControl adControl = new AdControl()
        {
          ApplicationId = adApplicationId,
          AdUnitId = configData.AdUnitId, // From msdn: This property can be set only when the AdControl is instantiated. Once set, this property cannot be modified.
          HorizontalAlignment = HorizontalAlignment.Left,
          Height = configData.Height,
          IsAutoRefreshEnabled = true,
          Name = configData.AdControlName, // Name is required for error handling
          VerticalAlignment = VerticalAlignment.Top,
          Width = configData.Width
        };

        // Add the error hander
        adControl.ErrorOccurred += OnAdControlErrorOccurred;

        // Add the new control to the parent at the correct position.
        parent.Children.Insert(postion, adControl);
      }
    }

    #endregion Private Methods


    #region Private Static Methods

    /// <summary>
    /// Reads the AdControl configuration data into an object list and retuns this list.
    /// </summary>
    /// <param name="xmlConfig">XML configuration.</param>
    /// <param name="sectionName">Name of the section where the AdControl config is set.</param>
    /// <returns>List of AdControl config settings; list might be empty.</returns>
    private static List<AdControlConfigData> GetAdControlConfiguration
      (
      XmlDocument xmlConfig,
      string sectionName
      )
    {
      List<AdControlConfigData> configDataList = new List<AdControlConfigData>();

      XmlNodeList xmlConfigDataList = xmlConfig.DocumentElement.SelectNodes(String.Format("./{0}/AdControlConfigData", sectionName));

      foreach (IXmlNode node in xmlConfigDataList)
      {
        AdControlConfigData configData = XmlDeserializer.Deserialize<AdControlConfigData>(node);

        configDataList.Add(configData);
      }

      return (configDataList);
    }

    #endregion Private Static Methods


    #region Protected Methods

    /// <summary>
    /// Sets up the AdControls of the passed page.
    /// </summary>
    /// <param name="configSectionName">Name of the section in the app.config containing the AdControl settings for this <paramref name="page"/>.</param>
    protected async void SetupAdControls
      (
      string configSectionName
      )
    {
      try
      {
        // Load the app.config.
        XmlDocument xmlConfig = await ConfigurationManager.GetConfig();

        // Get the AdApplicationId
        string adApplicationId = ConfigurationManager.GetAppSettingsValue(xmlConfig, "AdApplicationId");

        // Extract the AdControl config data from the XML into a object list.
        List<AdControlConfigData> adControlConfigDataList = AdControlContainingPage.GetAdControlConfiguration(xmlConfig, configSectionName);

        // Replace the AdControls
        ReplaceAdControls(adControlConfigDataList, adApplicationId);
      }
      catch (Exception e)
      {
        Debug.WriteLine(String.Format("Exception occurred:{0}{1}", Environment.NewLine, e));
      }
    }

    /// <summary>
    /// Handles the event when an error occurred in the ads.
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="args">Event arguments.</param>
    protected virtual void OnAdControlErrorOccurred
      (
      object sender,
      AdErrorEventArgs args
      )
    {
      AdControl adControl = sender as AdControl;

      // If event is not thrown by an Ad Control, do nothing
      if (adControl == null)
      {
        return;
      }

      // Just write out an error message.
      Debug.WriteLine("AdControl error (" + adControl.Name + "): " + args.Error + " ErrorCode: " + args.ErrorCode.ToString());

      // Replace adControl by TextBlock showing error message.
      // Find the TextBlock by naming conventions. The name of the TextBlock is the name of the AdControl plus "ErrorTextBlock".
      TextBlock textBlock = FindName(String.Format("{0}ErrorTextBlock", adControl.Name)) as TextBlock;

      if (textBlock == null)
      {
        return;
      }

      // Hide the adControl
      adControl.Visibility = Visibility.Collapsed;

      // Show the TextBlock and copy properties.
      textBlock.Visibility = Visibility.Visible;

      textBlock.Height = adControl.Height;

      textBlock.Width = adControl.Width;

      // Set some text.
      textBlock.Text = String.Format("Sorry!{0}Can't show AdControl '{1}'.{0}{0}ErrorCode: {2}{0}{0}ErrorMessage: {3}", 
        Environment.NewLine, adControl.Name, args.ErrorCode, args.Error.Message);
    }

    /// <summary>
    /// Populates the page with content passed during navigation.  Any saved state is also
    /// provided when recreating a page from a prior session.
    /// </summary>
    /// <param name="navigationParameter">The parameter value passed to
    /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
    /// </param>
    /// <param name="pageState">A dictionary of state preserved by this page during an earlier
    /// session.  This will be null the first time a page is visited.</param>
    protected override void LoadState
      (
      Object navigationParameter,
      Dictionary<String, Object> pageState
      )
    {
      base.LoadState(navigationParameter, pageState);

      // Run storyboard if one exists.
      Storyboard storyboard = FindName("PopInStoryboard") as Storyboard;

      if (storyboard != null)
      {
        storyboard.Begin();
      }
    }

    #endregion Protected Methods
  }
}
