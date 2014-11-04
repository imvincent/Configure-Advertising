using InstanceFactoy.ConfigureAdControlSample.Common;
using InstanceFactoy.ConfigureAdControlSample.SubPages;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace InstanceFactory.ConfigureAdControlSample
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class MainPage : LayoutAwarePage
  {
    #region Public Constructors

    public MainPage()
    {
      this.InitializeComponent();
    }

    #endregion Public Constructors


    #region Private Methods

    /// <summary>
    /// Handles the click event of an item in the list.
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="args">Event-Arguments.</param>
    private void OnItemClicked
      (
      object sender,
      ItemClickEventArgs args
      )
    {
      TextBlock item = args.ClickedItem as TextBlock;

      // A text block is not clicked => do nothing
      if (item == null)
      {
        return;
      }

      switch (item.Name)
      {
        case "Sample300x600TextBlock":
          Frame.Navigate(typeof(SamplePage300x600));
          break;
        case "Sample300x250TextBlock":
          Frame.Navigate(typeof(SamplePage300x250));
          break;
        case "SampleMultipleTextBlock":
          Frame.Navigate(typeof(SamplePageMultiple));
          break;
        default:
          return; // Unknown: do nothing
      }
    }

    #endregion Private Methods
  }
}
