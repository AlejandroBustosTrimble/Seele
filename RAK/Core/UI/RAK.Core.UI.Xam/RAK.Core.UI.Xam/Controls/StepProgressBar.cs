using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XF = Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{
    /// <summary>
    /// Barra de pasos de una secuencia de pantallas/cambios en una pantalla para indicar el avance de acciones
    /// </summary>
    public class StepProgressBar : StackLayout
    {
        #region Const

        private const int STEP_CONTROL_WIDTH = 30;
        private const int STEP_CONTROL_HEIGHT = 8;
        private const int STEP_CONTROL_HORIZONTAL_MARGIN = 5;
        private const int STEP_CONTROL_VERTICAL_MARGIN = 5;
        private const decimal STEP_BORDER_WIDTH = 0.5m;
        private const decimal STEP_BORDER_RADIUS = 20;

        private const String UNSELECTED_STYLE_KEY = "unSelectedStyle";
        private const String SELECTED_STYLE_KEY = "selectedStyle";

        #endregion

        #region Members

        Button _lastStepSelected;
        public static readonly BindableProperty StepsProperty = BindableProperty.Create(nameof(Steps), typeof(int), typeof(StepProgressBar), 0);
        public static readonly BindableProperty StepSelectedProperty = BindableProperty.Create(nameof(StepSelected), typeof(int), typeof(StepProgressBar), 1, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty StepColorProperty = BindableProperty.Create(nameof(StepColor), typeof(XF.Color), typeof(StepProgressBar), Color.Black, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty ShowStepNameProperty = BindableProperty.Create(nameof(ShowStepName), typeof(Boolean), typeof(StepProgressBar), true, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty IsEditableProperty = BindableProperty.Create(nameof(IsEditable), typeof(Boolean), typeof(StepProgressBar), false, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty NoCurrentStepColorProperty = BindableProperty.Create(nameof(NoCurrentStepColor), typeof(XF.Color), typeof(StepProgressBar), Color.Transparent, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty HasStepSeparatorProperty = BindableProperty.Create(nameof(HasStepSeparator), typeof(Boolean), typeof(StepProgressBar), false, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty StepWidthProperty = BindableProperty.Create(nameof(StepWidth), typeof(int), typeof(StepProgressBar), STEP_CONTROL_WIDTH, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty StepHeigthProperty = BindableProperty.Create(nameof(StepHeigth), typeof(int), typeof(StepProgressBar), STEP_CONTROL_HEIGHT, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty StepLeftMarginProperty = BindableProperty.Create(nameof(StepLeftMargin), typeof(int), typeof(StepProgressBar), STEP_CONTROL_HORIZONTAL_MARGIN, defaultBindingMode: BindingMode.TwoWay);


        public static readonly BindableProperty StepTopMarginProperty = BindableProperty.Create(nameof(StepTopMargin), typeof(int), typeof(StepProgressBar), STEP_CONTROL_VERTICAL_MARGIN, defaultBindingMode: BindingMode.TwoWay);


        #endregion

        #region Properties

        /// <summary>
        /// Color del control que indica el paso actual
        /// </summary>
        public Color StepColor
        {
            get { return (Color)GetValue(StepColorProperty); }
            set { SetValue(StepColorProperty, value); }
        }

        /// <summary>
        /// Cantidad de pasos totales
        /// </summary>
        public int Steps
        {
            get { return (int)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }

        /// <summary>
        /// Paso seleccionado
        /// </summary>
        /// <remarks>
        /// Si seteo 0 entonces ninguno esta seleccionado
        /// </remarks>
        public int StepSelected
        {
            get { return (int)GetValue(StepSelectedProperty); }
            set { SetValue(StepSelectedProperty, value); }
        }

        /// <summary>
        /// Obtiene si debe mostrar el nombre del paso
        /// </summary>
        public Boolean ShowStepName
        {
            get { return (Boolean)GetValue(ShowStepNameProperty); }
            set { SetValue(ShowStepNameProperty, value); }
        }

        /// <summary>
        /// Obtiene si es editable
        /// </summary>
        public Boolean IsEditable
        {
            get { return (Boolean)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }

        /// <summary>
        /// Color del control de los pasos no seleccionados
        /// </summary>
        public XF.Color NoCurrentStepColor
        {
            get { return (XF.Color)GetValue(NoCurrentStepColorProperty); }
            set { SetValue(NoCurrentStepColorProperty, value); }
        }

        /// <summary>
        /// Obtiene si tienen separadores los pasos
        /// </summary>
        public Boolean HasStepSeparator
        {
            get { return (Boolean)GetValue(HasStepSeparatorProperty); }
            set { SetValue(HasStepSeparatorProperty, value); }
        }

        /// <summary>
        /// Ancho de control de paso
        /// </summary>
        public int StepWidth
        {
            get { return (int)this.GetValue(StepWidthProperty); }
            set { this.SetValue(StepWidthProperty, value); }
        }

        /// <summary>
        /// Alto de control de paso
        /// </summary>
        public int StepHeigth
        {
            get { return (int)this.GetValue(StepHeigthProperty); }
            set { this.SetValue(StepHeigthProperty, value); }
        }

        /// <summary>
        /// Margen izquierdo de control de paso
        /// </summary>
        public int StepLeftMargin
        {
            get { return (int)this.GetValue(StepLeftMarginProperty); }
            set { this.SetValue(StepLeftMarginProperty, value); }
        }

        /// <summary>
        /// Margen de superior de control de paso
        /// </summary>
        public int StepTopMargin
        {
            get { return (int)this.GetValue(StepTopMarginProperty); }
            set { this.SetValue(StepTopMarginProperty, value); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Ctor
        /// </summary>
        public StepProgressBar()
        {
            this.Orientation = StackOrientation.Horizontal;
            this.HorizontalOptions = LayoutOptions.Center;
            this.Padding = new Thickness(10, 0);
            this.Spacing = 0;
            this.AddStyles();

        }

        #endregion

        #region Methods

        #region Protected

        /// <summary>
        /// OnPropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == StepsProperty.PropertyName)
            {
                for (int i = 1; i <= this.Steps; i++)
                {
                    var button = new Button();
                    if(this.ShowStepName)
                    {
                        button.Text = $"{i}";
                    }
                    button.ClassId = $"{i}";
                    if(this.StepSelected == i)
                    {
                        this.SelectElement(button);
                    }
                    else
                    {
                        button.Style = Resources[UNSELECTED_STYLE_KEY] as Style;
                    }
                    

                    if(this.IsEditable)
                    {
                        button.Clicked += Handle_Clicked;
                    }

                    this.Children.Add(button);

                    if(this.HasStepSeparator)
                    {
                        if (i < this.Steps)
                        {
                            var separatorLine = new BoxView()
                            {
                                BackgroundColor = Color.Silver,
                                HeightRequest = 1,
                                WidthRequest = 5,
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.FillAndExpand
                            };
                            this.Children.Add(separatorLine);
                        }
                    }
                }
            }
            else if (propertyName == StepSelectedProperty.PropertyName)
            {
                var children = this.Children.FirstOrDefault(p => (!string.IsNullOrEmpty(p.ClassId) && Convert.ToInt32(p.ClassId) == this.StepSelected));
                if (children != null) SelectElement(children as Button);

            }
            else if (propertyName == StepColorProperty.PropertyName)
            {
                this.AddStyles();
            }
        }

        /// <summary>
        /// Handle_Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Handle_Clicked(object sender, System.EventArgs e)
        {
            this.SelectElement(sender as Button);
        }

        /// <summary>
        /// Selecciona el control de un paso
        /// </summary>
        /// <param name="elementSelected"></param>
        private void SelectElement(Button elementSelected)
        {

            if (this._lastStepSelected != null)
            {
                this._lastStepSelected.Style = Resources[UNSELECTED_STYLE_KEY] as Style;
            }

            elementSelected.Style = Resources[SELECTED_STYLE_KEY] as Style;

            this.StepSelected = Convert.ToInt32(elementSelected.Text);
            this._lastStepSelected = elementSelected;
        }

        /// <summary>
        /// Agrega los estilos
        /// </summary>
        private void AddStyles()
        {
            var unselectedStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty,   Value = this.NoCurrentStepColor },
                    new Setter { Property = Button.BorderColorProperty,   Value = this.NoCurrentStepColor },
                    new Setter { Property = Button.TextColorProperty,   Value = StepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = STEP_BORDER_WIDTH },
                    new Setter { Property = Button.BorderRadiusProperty,   Value = STEP_BORDER_RADIUS },
                    new Setter { Property = Button.MarginProperty,   Value = new Thickness(this.StepLeftMargin, this.StepTopMargin) },
                    new Setter { Property = HeightRequestProperty,   Value = this.StepHeigth },
                    new Setter { Property = WidthRequestProperty,   Value = this.StepWidth}
            }
            };

            var selectedStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty, Value = StepColor },
                    new Setter { Property = Button.TextColorProperty, Value = Color.White },
                    new Setter { Property = Button.BorderColorProperty, Value = StepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = STEP_BORDER_WIDTH },
                    new Setter { Property = Button.BorderRadiusProperty,   Value = STEP_BORDER_RADIUS },
                    new Setter { Property = Button.MarginProperty,   Value = new Thickness(this.StepLeftMargin, this.StepTopMargin) },
                    new Setter { Property = HeightRequestProperty,   Value = this.StepHeigth },
                    new Setter { Property = WidthRequestProperty,   Value = this.StepWidth },
                    new Setter { Property = Button.FontAttributesProperty,   Value = FontAttributes.Bold }
            }
            };

            Resources = new ResourceDictionary();
            Resources.Add(UNSELECTED_STYLE_KEY, unselectedStyle);
            Resources.Add(SELECTED_STYLE_KEY, selectedStyle);
        }

        #endregion

        #endregion
    }
}
