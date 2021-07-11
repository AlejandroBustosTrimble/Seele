using RAK.Core.UI.Xam.Interfaces;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomEntryWithShakeLabel : ContentView
    {

        /// <summary>
        /// Color de Label en caso de Exito por defecto
        /// </summary>
        private Color SuccessLabelColor { get; set; } = Color.Green;

        /// <summary>
        /// Color de Label en caso de Error por defecto
        /// </summary>
        private Color ErrorLabelColor { get; set; } = Color.FromHex("#ff5955");

        /// <summary>
        /// Color de Label en caso de Warning por Defecto
        /// </summary>
        private Color WarningLabelColor { get; set; } = Color.Gray;

        /// <summary>
        /// Validador especifico con el que trabajara
        /// </summary>
        ICustomEntryWithShakeLabelValidator MyValidator;

        /// <summary>
        /// Boton 'Submit' externo. Puede registrarse o no
        /// Para registrarlo, utilizar: RegisterSubmitButtonToEnable
        /// En caso de registrarse, dependiendo del resultado de el validator, modifica el enable
        /// </summary>
        VisualElement SubmitVisualElement { get; set; }

        #region Binding Properties

        /// <summary>
        /// Binding Property -> Texto ingresado
        /// </summary>
        public static readonly BindableProperty MainTextProperty = BindableProperty.Create(
            propertyName: "MainText",
            returnType: typeof(string),
            declaringType: typeof(CustomEntryWithShakeLabel),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay
            );

        /// <summary>
        /// Texto ingresado
        /// </summary>
        public string MainText
        {
            get
            {
                return (string)GetValue(MainTextProperty);
            }
            set => SetValue(MainTextProperty, value);
        }

        /// <summary>
        /// Binding Property -> Placeholder del Textbox Principal
        /// </summary>
        private static readonly BindableProperty MainPlaceHolderProperty = BindableProperty.Create(
            propertyName: "MainPlaceHolder",
            returnType: typeof(string),
            declaringType: typeof(CustomEntryWithShakeLabel),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay
            );

        /// <summary>
        /// Placeholder del Textbox Principal
        /// </summary>
        public string MainPlaceHolder
        {
            get
            {
                return (string)GetValue(MainPlaceHolderProperty);
            }
            set => SetValue(MainPlaceHolderProperty, value);
        }

        #endregion

        #region Events

        public event OnSuccessEventHandler OnSuccessValidation = delegate { };
        public delegate void OnSuccessEventHandler();

        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        public CustomEntryWithShakeLabel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// PropertyChange
        /// </summary>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Evento -> Por cada input en el textbox
        /// </summary>
        private void TxtMainEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var newValue = e.NewTextValue;

            if (string.IsNullOrEmpty(newValue) && SubmitVisualElement != null)
                ChangeElementEnabled(false);

            if (MyValidator != null)
            {
                var ValidateResult = MyValidator.Validate(newValue);
                this.SetContext(ValidateResult);
            }
        }

        /// <summary>
        /// Setea label por defecto
        /// </summary>
        void SetContext(CustomEntryWithShakeLabelValidateResult ValidationResult)
        {
            lblError.Text = ValidationResult.Message;

            if (ValidationResult.IsSuccess)
            {
                lblError.TextColor = !ValidationResult.LabelColorToShowMessage.HasValue ? SuccessLabelColor : ValidationResult.LabelColorToShowMessage.Value;
                ChangeElementEnabled(true);
                OnSuccessValidation();
            }
            else if (ValidationResult.IsError)
            {
                lblError.TextColor = !ValidationResult.LabelColorToShowMessage.HasValue ? ErrorLabelColor : ValidationResult.LabelColorToShowMessage.Value;
                ChangeElementEnabled(false);
            }
            // -- Caso Warning
            else
            {
                lblError.TextColor = !ValidationResult.LabelColorToShowMessage.HasValue ? WarningLabelColor : ValidationResult.LabelColorToShowMessage.Value;
                ChangeElementEnabled(false);
            }

            // -- Si no es exito, siempre hacemos que el Label vibre
            if (ValidationResult.IsError)
            {
                ShakeLabelAsync(lblError);
            }

        }


        private void ChangeElementEnabled(bool Value)
        {
            if (SubmitVisualElement != null)
                SubmitVisualElement.IsEnabled = Value;
        }

        /// <summary>
        /// Setea foco en textbox principal
        /// </summary>
        public void SetFocusOnMainText()
        {
            this.txtMainEntry.Focus();
        }

        /// <summary>
        /// Simula vibracion de Label
        /// </summary>
        async System.Threading.Tasks.Task ShakeLabelAsync(Label label)
        {
            uint timeout = 50;
            await label.TranslateTo(-15, 0, timeout);
            await label.TranslateTo(15, 0, timeout);
            await label.TranslateTo(-10, 0, timeout);
            await label.TranslateTo(10, 0, timeout);
            await label.TranslateTo(-5, 0, timeout);
            await label.TranslateTo(5, 0, timeout);
            label.TranslationX = 0;
        }

        /// <summary>
        /// Registra un validador para su uso posterior
        /// </summary>
        public void RegisterValidator(ICustomEntryWithShakeLabelValidator Validator)
        {
            if (MyValidator == null)
            {
                MyValidator = Validator;
            }
        }

        /// <summary>
        /// En la mayoria de las pantallas, en caso de que la validacion no se de, no deberiamos poder apretar el boton de Submit
        /// Opcionalmente, permitimos registrar un boton para que este control administre tambien la visibilidad del mismo
        /// </summary>
        public void RegisterSubmitButtonToEnable(VisualElement visualElement)
        {
            if (SubmitVisualElement == null)
            {
                SubmitVisualElement = visualElement;
                SubmitVisualElement.IsEnabled = false;
            }
        }

        /// <summary>
        /// Permite modificar el tipo de keyboard
        /// </summary>
        public void ChangeDefaultMainTextKeyboard(Keyboard keyboardType)
        {
            this.txtMainEntry.Keyboard = keyboardType;
        }

        /// <summary>
        /// Setea MaxLen en Main Entry
        /// </summary>
        public void SetMaxLenToMainEntry(int maxLen)
        {
            this.txtMainEntry.MaxLength = maxLen;
        }
    }
}