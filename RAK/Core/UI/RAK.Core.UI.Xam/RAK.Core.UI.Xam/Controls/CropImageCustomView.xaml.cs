
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CropImageCustomView : ContentView
	{

        /// <summary>
        /// Maxima Calidad
        /// </summary>
        const int MAX_QUALITY = 100;

        /// <summary>
        /// Binding Property -> Source para el CropView
        /// </summary>
        public static readonly BindableProperty CropViewSourceProperty = BindableProperty.Create(
            propertyName: "CropViewSource",
            returnType: typeof(ImageSource),
            declaringType: typeof(CropImageCustomView),
            defaultValue: default(byte[]),
            defaultBindingMode: BindingMode.TwoWay
            );

        /// <summary>
        /// ImageSource para el CropView
        /// </summary>
        public ImageSource CropViewSource
        {
            get
            {
                return (ImageSource)GetValue(CropViewSourceProperty);
            }
            set => SetValue(CropViewSourceProperty, value);
        }

        /// <summary>
        /// Layout al que se le va a agregar el Control
        /// </summary>
        public StackLayout LayoutToAddControl { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public CropImageCustomView ()
		{
			InitializeComponent ();
            LayoutToAddControl = buttonLayout;
        }
        
        /// <summary>
        /// Setea el tipo de transformacion
        /// </summary>
        public void SetTransformationType(TransformationCropType Type)
        {
            if(Type == TransformationCropType.Circle)
                cropView.PreviewTransformations = new List<FFImageLoading.Work.ITransformation>() { new FFImageLoading.Transformations.CircleTransformation() };
            else
                cropView.PreviewTransformations = new List<FFImageLoading.Work.ITransformation>() { new FFImageLoading.Transformations.CropTransformation() };
        }
        
        /// <summary>
        /// Obtiene un los Bytes de la imagen
        /// </summary>
        public async Task<byte[]> GetBytesFromCropView()
        {
            var result = await cropView.GetImageAsJpegAsync(MAX_QUALITY);
            byte[] bytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                result.CopyTo(ms);
                bytes = ms.ToArray();
            }
            return bytes;
        }

    }

    /// <summary>
    /// Tipos de enumeraciones
    /// </summary>
    public enum TransformationCropType
    {
        Circle = 1,
        Rectangle = 2
    }

}