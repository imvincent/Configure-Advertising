using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace InstanceFactoy.ConfigureAdControlSample.Configuration
{
  /// <summary>
  /// Manages the configuration stored in app.config mimic.
  /// </summary>
  internal static class ConfigurationManager
  {
    #region Private Const Data Member

    /// <summary>
    /// Keeps the name of the config file to read from.
    /// </summary>
    private const string ConfigFileName
#if DEBUG
 = "Config.Debug.xml";
#else
    = "Config.Release.xml";
#endif // DEBUG

    #endregion Private Const Data Member


    #region Public Static Methods

    /// <summary>
    /// Reads the config file from the app directory.
    /// </summary>
    /// <returns>The XML config.</returns>
    public static async Task<XmlDocument> GetConfig()
    {
      StorageFile configFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(String.Format("ms-appx:///Assets/Configuration/{0}", ConfigurationManager.ConfigFileName)));

      return (await XmlDocument.LoadFromFileAsync(configFile));
    }

    /// <summary>
    /// Reads a value from the appSettings section for a given key.
    /// </summary>
    /// <param name="xmlConfig">The config as XML.</param>
    /// <param name="key">The key to look for.</param>
    /// <returns>The value or <c>null</c> if not found.</returns>
    public static string GetAppSettingsValue
      (
      XmlDocument xmlConfig,
      string key
      )
    {
      IXmlNode node = xmlConfig.DocumentElement.SelectSingleNode(String.Format("./appSettings/add[@key='{0}']/@value", key));

      if (node == null)
      {
        Debug.WriteLine(String.Format("Key >{0}< not found in appSettings section.", key));
        return (null);
      }

      // Return the value as stirng.
      return (node.NodeValue as String);
    }

    #endregion Public Static Methods
  }
}
