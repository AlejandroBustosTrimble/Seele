using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Camera
{
    public static class Camera
    {
        #region Const

        private const string TOP_TEXT_DEF_VALUE = "Mantenga la camara sobre el codigo\nA una distancia de 15 cm\nPara enfocar toque la pantalla";
        private const string BOTTOM_TEXT_DEF_VALUE = "Esperando el codigo...";
        private const int DELAY_BETWEEN_CONTINOUS_SCANS_MS = 2000;
        private const int MIN_WIDTH_RESOLUTION_LOGIC = 1300;

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Scann utilizando el MobileBarcodeScanner
        /// </summary>
        public static async Task<string> ScanBarCode()
        {
            var scanner = new MobileBarcodeScanner();

            scanner.TopText = TOP_TEXT_DEF_VALUE;
            scanner.BottomText = BOTTOM_TEXT_DEF_VALUE;

            var resultPermissions = await CheckPermissions();

            // -- Si no hay permisos retornamos null
            if (!resultPermissions)
                return null;

            var options = new MobileBarcodeScanningOptions();

            options.UseFrontCameraIfAvailable = false;
            // Como internamente seteo que este en LandScape fuerzo que no se pueda rotar, porque lee mucho mejor
            // los codigos de barra
            options.AutoRotate = false;
            options.DisableAutofocus = false;
            options.TryInverted = true;
            options.TryHarder = true;
            // Este valor vi que lo usaban varios con respecto a esta libreria
            // Por otro lado intente con valores mas bajo (1000) y valores mas altos y con el valor actual funciona mejor
            options.DelayBetweenContinuousScans = DELAY_BETWEEN_CONTINOUS_SCANS_MS;
            // Limito los formatos para que lea mas rapido, ya que la mayoria de los codigos que vamos a leer son ITF y CODE_128 (agregue tmb 39 porque vi que lo usaban
            // en algunas paginas)
            options.PossibleFormats = new List<ZXing.BarcodeFormat>() { ZXing.BarcodeFormat.CODE_39, ZXing.BarcodeFormat.ITF, ZXing.BarcodeFormat.CODE_128 };
            options.CameraResolutionSelector = HandleCameraResolutionSelectorDelegate;
            options.Orientation = MobileBarcodeScanningOptions.BarcodeScannerOrientation.Landscape;
            options.Torch = false; 
            options.EnableAutoFocusPeriodically = true;
            scanner.FlashButtonText = "Flash";
            scanner.CancelButtonText = "Cancelar";

            ZXing.Result result = null;

            var scannerResult = scanner.Scan(options);

            result = await scannerResult;

            if (result != null)
            {
                return result.Text;
            }
            else
            {
                return "";
            }
        }

        public static async Task<string> ScanQRCode()
        {
            var scanner = new MobileBarcodeScanner();

            scanner.TopText = TOP_TEXT_DEF_VALUE;
            scanner.BottomText = BOTTOM_TEXT_DEF_VALUE;

            var resultPermissions = await CheckPermissions();

            // -- Si no hay permisos retornamos null
            if (!resultPermissions)
                return null;

            ZXing.Result result = await scanner.Scan(new MobileBarcodeScanningOptions { AutoRotate = true });

            if (result != null)
            {
                return result.Text;
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion

        #region Protected

        /// <summary>
        /// Maneja la seleccion de resolucion para la camara
        /// </summary>
        /// <param name="availableResolutions"></param>
        /// <returns></returns>
        private static ZXing.Mobile.CameraResolution HandleCameraResolutionSelectorDelegate(List<ZXing.Mobile.CameraResolution> availableResolutions)
        {
            var targetRatio = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Height;
            var targetArea = DeviceDisplay.MainDisplayInfo.Height * DeviceDisplay.MainDisplayInfo.Width;
            CameraResolutionData result = null;

            Console.WriteLine($"Camera.ScanBarCode. ResolucionPantalla: {DeviceDisplay.MainDisplayInfo.Width}x{DeviceDisplay.MainDisplayInfo.Height}");

            var resList = new List<CameraResolutionData>();
            foreach (var res in availableResolutions)
            {
                var resWrapper = new CameraResolutionData(res, targetRatio, targetArea);
                resList.Add(resWrapper);
                Console.WriteLine($"Camera.ScanBarCode. Resolucion: {res.Width}x{res.Height}, Equality: {resWrapper.EqualityRatio}");
            }

            if (availableResolutions.Any(r => r.Width > MIN_WIDTH_RESOLUTION_LOGIC))
            {
                double aspectTolerance = 0.25;

                var filteredResList = resList.Where(r => r.AspectRatioDifference <= aspectTolerance).OrderBy(r => r.AreaSize);

                var filteredResCount = filteredResList.Count();
                if (filteredResCount > 0)
                {
                    var index = (int)Math.Truncate((double)filteredResCount / 2);
                    index--;

                    if (index < 0)
                    {
                        index = 0;
                    }

                    result = filteredResList.ToList()[index];
                }
            }
            else
            {
                // Esto aplica para los celulares viejos, para que utilice la logica base para obtener la resolucion
                
                //a tolerance of 0.1 should not be recognizable for users
                double aspectTolerance = 0.1;
                //calculating our targetRatio
                
                var targetHeight = DeviceDisplay.MainDisplayInfo.Height;
                var minDiff = double.MaxValue;
                //camera API lists all available resolutions from highest to lowest, perfect for us
                //making use of this sorting, following code runs some comparisons to select the lowest resolution that matches the screen aspect ratio
                //selecting the lowest makes QR detection actual faster most of the time
                foreach (var r in resList)
                {
                    //if current ratio is bigger than our tolerance, move on
                    //camera resolution is provided landscape ...
                    if (r.AspectRatioDifference > aspectTolerance)
                        continue;
                    else
                        if (Math.Abs(r.InnerEntity.Height - targetHeight) < minDiff)
                        minDiff = Math.Abs(r.InnerEntity.Height - targetHeight);
                    result = r;
                }
            }

            return result?.InnerEntity;
        }

        private class CameraResolutionData
        {
            public ZXing.Mobile.CameraResolution InnerEntity { get; private set; }

            /// <summary>
            /// Ratio de Aspecto
            /// </summary>
            public double AspectRatio {get; }

            /// <summary>
            /// Area
            /// </summary>
            public double AreaSize { get; }

            /// <summary>
            /// Ratio de diferencia en el aspecto con la resolucion "objetivo"
            /// </summary>
            public double AspectRatioDifference { get; }

            /// <summary>
            /// Ratio de diferencia en el Area con la resolucion "objetivo"
            /// </summary>
            public double AreaSizeRatioDifference { get; }

            /// <summary>
            /// Ratio de igualdad con la resolucion "objetivo"
            /// </summary>
            public double EqualityRatio
            {
                get
                {
                    return Math.Abs( (1 - this.AreaSizeRatioDifference) * (1 -AspectRatioDifference));
                }
            }

            public CameraResolutionData(ZXing.Mobile.CameraResolution camResolution, double targetAspectRatio, double targetAreaSize)
            {
                this.InnerEntity = camResolution;
                this.AspectRatio = (double)this.InnerEntity.Width / this.InnerEntity.Height;
                this.AreaSize = this.InnerEntity.Height * this.InnerEntity.Width;

                this.AspectRatioDifference = Math.Abs(this.AspectRatio - targetAspectRatio);
                this.AreaSizeRatioDifference = Math.Abs(this.AreaSize - targetAreaSize) / targetAreaSize;
            }
        }


        // -- Chequea permisos de la camara
        private async static Task<bool> CheckPermissions()
        {
            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

            if (cameraStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];

                if (cameraStatus != PermissionStatus.Granted)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return true;
        }

        #endregion

        #endregion
    }

}
