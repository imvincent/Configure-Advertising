using System.Xml.Serialization;

namespace InstanceFactoy.ConfigureAdControlSample.Configuration
{
  /// <summary>
  /// Contaoner of the AdControl data that is required to create the control during runtime; to be deserialized from the configuration.
  /// </summary>
  public class AdControlConfigData
  {
    #region Public Properties

    /// <summary>
    /// Gets / sets the name of the AdControl to be replaced.
    /// </summary>
    [XmlAttribute]
    public string AdControlName { get; set; }

    /// <summary>
    /// Gets / sets the AdUnitId
    /// </summary>
    [XmlAttribute]
    public string AdUnitId { get; set; }

    /// <summary>
    /// Gets / sets the height of the control.
    /// </summary>
    [XmlAttribute]
    public double Height { get; set; }

    /// <summary>
    /// Gets / sets the width of the control.
    /// </summary>
    [XmlAttribute]
    public double Width { get; set; }

    #endregion Public Properties
  }
}
