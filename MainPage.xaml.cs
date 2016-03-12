
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Lumia.Imaging;
using Windows.Graphics.Display;
using Lumia.Imaging.Artistic;
using Lumia.Imaging.Adjustments;
using Lumia.Imaging.Transforms;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace test2effect
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private BrightnessEffect _brightnessEffect;

        SwapChainPanelRenderer m_renderer;
        private WriteableBitmap _writeableBitmap;
        private HueSaturationEffect _saturation;
        private ContrastEffect _contrastEffect;
        IImageProvider _effect;
        StorageFile file;
        RadioButton _actualState;
        private BlurEffect _blurEffect;
        private GaussianNoiseEffect _gausianEffect;
        private VibranceEffect _vibranceEffect;
        private RotationEffect _rotation;
        private NegativeEffect _negativeEffect;
        private MagicPenEffect _magicPen;
        private SketchEffect _sketchEffect;
        public MainPage()
        {
            this.InitializeComponent();
            double scaleFactor = 1.0;
            scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;

            _writeableBitmap = new WriteableBitmap((int)(Window.Current.Bounds.Width * scaleFactor), (int)(Window.Current.Bounds.Height * scaleFactor));
            _brightnessEffect = new BrightnessEffect(0);
            _saturation = new HueSaturationEffect(0.0, 0.0);
            _contrastEffect = new ContrastEffect(0);
            _blurEffect = new BlurEffect(1);
            _gausianEffect = new GaussianNoiseEffect(1);
            _vibranceEffect = new VibranceEffect();
            _rotation = new RotationEffect(0);
            _negativeEffect = new NegativeEffect();
            _magicPen = new MagicPenEffect();
            _sketchEffect = new SketchEffect(SketchMode.Gray);
            SwapChainPanelTarget.Loaded += SwapChainPanelTarget_Loaded;
            _actualState = Brightness; 
            _effect = _brightnessEffect;
         
            
        }

        private async void SwapChainPanelTarget_Loaded(object sender, RoutedEventArgs e)
        {
            

            if (SwapChainPanelTarget.ActualHeight > 0 && SwapChainPanelTarget.ActualWidth > 0)
            {
                if (m_renderer !=null)
                {
                   m_renderer = new SwapChainPanelRenderer(_effect, SwapChainPanelTarget);
                    
                    
                    await LoadDefaultImageAsync();
                }
               
              
            }
        }

        
        private async Task LoadDefaultImageAsync()
        {
            if (file == null)
            {
                file = await StorageFile.GetFileFromApplicationUriAsync(new System.Uri("ms-appx:///Assets/kot.jpg"));
            }

            IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                // Rewind stream to start.                     
                fileStream.Seek(0);

                // Set the imageSource on the effect and render            
                ((IImageConsumer)_effect).Source = new Lumia.Imaging.RandomAccessStreamImageSource(fileStream);
                await m_renderer.RenderAsync();
           
           
        }


        

        private async void Saturation_Checked(object sender, RoutedEventArgs e)
        {
            if (_actualState != Saturation)
            {
                _actualState.IsChecked = false;
            }
            _effect = _saturation;
            _actualState = Saturation;
            m_renderer = new SwapChainPanelRenderer(_effect, SwapChainPanelTarget);
            await LoadDefaultImageAsync();
        }

       

        private async void Brightness_Checked(object sender, RoutedEventArgs e)
        {
           
            if (_actualState != Brightness)
            {
                _actualState.IsChecked = false;
            }
           
            _effect = _brightnessEffect;
            _actualState = Brightness; 
            m_renderer = new SwapChainPanelRenderer(_effect, SwapChainPanelTarget);
            await LoadDefaultImageAsync();
        }

        
       
        private async void Contrast_Checked(object sender, RoutedEventArgs e)
        {
            
            if (_actualState != Contrast)
            {
                _actualState.IsChecked = false;
            }
            
            _effect = _contrastEffect;
            _actualState = Contrast;
            m_renderer = new SwapChainPanelRenderer(_effect, SwapChainPanelTarget);
            await LoadDefaultImageAsync();
        }

        private async void Blur_Checked(object sender, RoutedEventArgs e)
        {
            if (_actualState != Blur)
            {
                _actualState.IsChecked = false;
            }

            _effect = _blurEffect;
            _actualState = Blur;
            m_renderer = new SwapChainPanelRenderer(_effect, SwapChainPanelTarget);
            await LoadDefaultImageAsync();
        }

        private async void GausianNoise_Checked(object sender, RoutedEventArgs e)
        {
            if (_actualState != GausianNoise)
            {
                _actualState.IsChecked = false;
            }

            _effect = _gausianEffect;
            _actualState = GausianNoise;
            m_renderer = new SwapChainPanelRenderer(_effect, SwapChainPanelTarget);
            await LoadDefaultImageAsync();
        }

        private async void Vibrance_Checked(object sender, RoutedEventArgs e)
        {
            if (_actualState != Vibrance)
            {
                _actualState.IsChecked = false;
            }

            _effect = _vibranceEffect;
            _actualState = Vibrance;
            m_renderer = new SwapChainPanelRenderer(_effect, SwapChainPanelTarget);
            await LoadDefaultImageAsync();
        }
        private async void Rotation_Checked(object sender, RoutedEventArgs e)
        {

            if (_actualState != Rotation)
            {
                _actualState.IsChecked = false;
            }
            _effect = _rotation;
            _actualState = Rotation; 
            m_renderer = new SwapChainPanelRenderer(_effect, SwapChainPanelTarget);
            await LoadDefaultImageAsync();
        }

       
        private async void Effect_changed_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (_effect == _brightnessEffect)
            {
                _brightnessEffect.Level = e.NewValue;

            }
            else if (_effect == _saturation)
            {
                _saturation.Hue = e.NewValue;
                _saturation.Saturation = e.NewValue;

            }
            else if (_effect == _contrastEffect)
            {
                _contrastEffect.Level = e.NewValue;
            }
            else if(_effect == _blurEffect)
            {
                _blurEffect.KernelSize = (int)((1+e.NewValue)*20 +1);
            }
            else if(_effect == _gausianEffect)
            {
                _gausianEffect.Level = (int)((1 + e.NewValue) * 40 + 1);
            }
            else if(_effect == _vibranceEffect)
            {
                _vibranceEffect.Level = (e.NewValue+1) + 0.001;
            }
            else if(_effect == _rotation)
            {
                _rotation.RotationAngle = (e.NewValue + 1) * 180; 
            }
          

            await m_renderer.RenderAsync();
        }

        private async void Negative_Click(object sender, RoutedEventArgs e)
        {
            _effect = _negativeEffect;
            m_renderer = new SwapChainPanelRenderer(_effect, SwapChainPanelTarget);
            await LoadDefaultImageAsync();
        }

        
        private void PickImage_Click(object sender, RoutedEventArgs e)
        {
            //SaveImage.IsEnabled = false;

            var openPicker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                ViewMode = PickerViewMode.Thumbnail
            };

            // Filter to include a sample subset of file types.
            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".jpg");

            PickImagef(openPicker);
        }

        private void SaveImage_Click(object sender, RoutedEventArgs e)
        {
            //SaveButton.IsEnabled = false;

            var savePicker = new FileSavePicker()
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                SuggestedFileName = string.Format("QuickstartImage_{0}", DateTime.Now.ToString("yyyyMMddHHmmss"))
            };

            savePicker.FileTypeChoices.Add("JPG File", new List<string> { ".jpg" });

            SaveImagef(savePicker);
        }

        private async Task<bool> SaveImageAsync(StorageFile file)
        {
            if (_effect == null)
            {
                return false;
            }

            string errorMessage = null;

            try
            {
                using (var jpegRenderer = new JpegRenderer(_effect))
                using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    // Jpeg renderer gives the raw buffer containing the filtered image.
                    IBuffer jpegBuffer = await jpegRenderer.RenderAsync();
                    await stream.WriteAsync(jpegBuffer);
                    await stream.FlushAsync();
                }
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                var dialog = new MessageDialog(errorMessage);
                await dialog.ShowAsync();
                return false;
            }
            return true;
        }
        private async void PickImagef(FileOpenPicker openPicker)
        {
            // Open the file picker.
            file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                m_renderer = new SwapChainPanelRenderer(_effect, SwapChainPanelTarget);
                await LoadDefaultImageAsync();
                SaveImage.IsEnabled = true;
            }
        }

        private async void SaveImagef(FileSavePicker savePicker)
        {
            var file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                await SaveImageAsync(file);
            }

           
        }

        private async void MagicPen_Click(object sender, RoutedEventArgs e)
        {
            _effect = _magicPen;
            m_renderer = new SwapChainPanelRenderer(_effect, SwapChainPanelTarget);
            await LoadDefaultImageAsync();
        }

        private async void Sketch_Click(object sender, RoutedEventArgs e)
        {
            _effect = _sketchEffect;
            m_renderer = new SwapChainPanelRenderer(_effect, SwapChainPanelTarget);
            await LoadDefaultImageAsync();
        }
    }
}
