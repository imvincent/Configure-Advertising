using InstanceFactoy.ConfigureAdControlSample.Common;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace InstanceFactoy.ConfigureAdControlSample.SubPages
{
  /// <summary>
  /// A basic page that provides characteristics common to most applications.
  /// </summary>
  public sealed partial class SamplePage300x600 : AdControlContainingPage
  {
    #region Public Constructor

    /// <summary>
    /// Initializes a new instance of <see cref="SamplePage300x600"/>
    /// </summary>
    public SamplePage300x600()
    {
      this.InitializeComponent();

      // Replace the AdControls
      SetupAdControls("AdControlConfigList300x600");
    }

    #endregion Public Constructor
  }
}
