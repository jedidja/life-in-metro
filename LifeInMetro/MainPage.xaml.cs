using System;
using NotificationsExtensions.TileContent;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LifeInMetro
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int MapWidth = 96;
        private const int MapHeight = 96;
        private const int CellSize = 5;

        private readonly LifeViewModel viewModel;
        private readonly CellMap cellMap;

        private readonly DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();

            viewModel = new LifeViewModel();
            DataContext = viewModel;

            cellMap = new CellMap(MapHeight, MapWidth, GameOfLifeImage, CellSize);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000 / 10);
            timer.Tick += timer_Tick;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            timer.Start();
        }

        private void timer_Tick(object sender, object e)
        {
            cellMap.NextGeneration();
            viewModel.Generation += 1;

            UpdateLiveTilesIfRequired();
        }

        private void UpdateLiveTilesIfRequired()
        {
            if (viewModel.Generation % 50 == 0)
            {
                string line1 = string.Format("Generation: {0}", viewModel.Generation);
                string line2 = string.Format("% Alive: {0}", cellMap.PercentageOfAliveCells);

                var tileContentWide = TileContentFactory.CreateTileWideText05();
                tileContentWide.TextBody1.Text = line1;
                tileContentWide.TextBody2.Text = line2;

                var tileContentSquare = TileContentFactory.CreateTileSquareText03();
                tileContentSquare.TextBody1.Text = line1;
                tileContentSquare.TextBody2.Text = line2;
                tileContentWide.SquareContent = tileContentSquare;

                var tileNotification = tileContentWide.CreateNotification();
                TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
            }
        }
    }
}
