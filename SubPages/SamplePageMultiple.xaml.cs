using InstanceFactoy.ConfigureAdControlSample.Common;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace InstanceFactoy.ConfigureAdControlSample.SubPages
{
  /// <summary>
  /// A basic page that provides characteristics common to most applications.
  /// </summary>
  public sealed partial class SamplePageMultiple : AdControlContainingPage
  {
    #region Public Constructor

    /// <summary>
    /// Initializes a new instance of <see cref="SamplePageMultiple"/>
    /// </summary>
    public SamplePageMultiple()
    {
      this.InitializeComponent();

      // Replace the AdControls
      SetupAdControls("AdControlConfigListMultiple");
    }

    #endregion Public Constructor
  }
}
