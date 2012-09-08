using System;
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
        private readonly CellMap currentMap;
        private readonly CellMap nextMap;
        private readonly CellMapDisplay cellMapDisplay;

        private readonly DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();

            viewModel = new LifeViewModel();
            DataContext = viewModel;

            cellMapDisplay = new CellMapDisplay(LifeCanvas, MapWidth, MapHeight, CellSize);

            currentMap = new CellMap(MapHeight, MapWidth);
            nextMap = new CellMap(MapHeight, MapWidth);

            var r = new Random();

            var initLength = MapWidth * MapHeight / 2;
            do
            {
                nextMap.SetCell(r.Next(MapWidth), r.Next(MapHeight));
            } while (--initLength != 0);

            currentMap.CopyCells(nextMap);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000 / 18.2);
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
            viewModel.Generation += 1;
            currentMap.NextGeneration(nextMap, cellMapDisplay);
            currentMap.CopyCells(nextMap);
        }
    }
}
