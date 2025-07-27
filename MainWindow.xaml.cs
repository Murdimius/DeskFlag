using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using MessageBox = System.Windows.MessageBox;

namespace DeskFlag
{
    public partial class MainWindow
    {
        private NotifyIcon _trayIcon;
        private readonly List<BitmapSource> _frames = [];
        private int _currentFrame;
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTrayIcon();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPngFrames();

            if (_frames.Count == 0)
            {
                MessageBox.Show("No frames loaded from GIF.");
                return;
            }

            StartAnimation();
            PositionNearTray();
        }

        private void LoadPngFrames()
        {
            var framesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "ImageSequence");
            if (!Directory.Exists(framesPath))
            {
                MessageBox.Show("ImageSequence folder not found.");
                return;
            }

            var files = Directory.GetFiles(framesPath, "*.png");
            Array.Sort(files);

            foreach (var file in files)
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(file, UriKind.Absolute);
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                _frames.Add(image);
            }
        }
        
        private void InitializeTrayIcon()
        {
            _trayIcon = new NotifyIcon
            {
                Icon = new Icon(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "flag_icon.ico")),
                Visible = true,
                Text = "DeskFlag"
            };

            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Exit", null, (s, e) => ExitApp());

            _trayIcon.ContextMenuStrip = contextMenu;
        }

        private void ExitApp()
        {
            _trayIcon.Visible = false;
            _trayIcon.Dispose();
            System.Windows.Application.Current.Shutdown();
        }

        private void PositionNearTray()
        {
            if (Screen.PrimaryScreen == null) return;
            var screen = Screen.PrimaryScreen.WorkingArea;
            Left = screen.Right - Width - 10;
            Top = screen.Bottom - Height - 10;
        }
        
        private void StartAnimation()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(41.666)
            };
            _timer.Tick += (s, e) =>
            {
                FlagImage.Source = _frames[_currentFrame];
                _currentFrame = (_currentFrame + 1) % _frames.Count;
            };
            _timer.Start();
        }
    }
}
