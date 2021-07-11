using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RAK.Core.UI.Xam.Controls;
using RAK.Core.UI.Xam.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(CustomExtendedPickerRenderer))]
namespace RAK.Core.UI.Xam.Droid.Controls
{
    /// <summary>
    /// Custom Renderer para KITS.Xamarin.Framework.Controls.ExtendedDatePicker
    /// </summary>
    public class CustomExtendedPickerRenderer : ViewRenderer<ExtendedDatePicker, EditText>
    {
        DatePickerDialog _dialog;
        protected override void OnElementChanged(ElementChangedEventArgs<ExtendedDatePicker> e)
        {
            base.OnElementChanged(e);

            this.SetNativeControl(new Android.Widget.EditText(Forms.Context));
            if (Control == null || e.NewElement == null)
                return;

            this.Control.Click += OnPickerClick;
            this.Control.Text = Element.Date.ToString(Element.Format);
            this.Control.KeyListener = null;
            this.Control.FocusChange += OnPickerFocusChange;
            this.Control.Enabled = Element.IsEnabled;


            if (e.OldElement == null)
            {
                var gradientDrawable = new GradientDrawable();

                gradientDrawable.SetCornerRadius(10f);
                gradientDrawable.SetStroke(3, Android.Graphics.Color.Gray);
                gradientDrawable.SetColor(Android.Graphics.Color.White);
                //gradientDrawable.SetCornerRadius(Android.Graphics.Color.LightGray);
                Control.SetBackground(gradientDrawable);

                Control.SetPadding(10, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
            }
        }

        public CustomExtendedPickerRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Xamarin.Forms.DatePicker.DateProperty.PropertyName || e.PropertyName == Xamarin.Forms.DatePicker.FormatProperty.PropertyName)
            {
                SetDate(Element.Date);
            }
        }

        void OnPickerFocusChange(object sender, Android.Views.View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                ShowDatePicker();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (Control != null)
            {
                this.Control.Click -= OnPickerClick;
                this.Control.FocusChange -= OnPickerFocusChange;

                if (_dialog != null)
                {
                    _dialog.Hide();
                    _dialog.Dispose();
                    _dialog = null;
                }
            }

            base.Dispose(disposing);
        }

        void OnPickerClick(object sender, EventArgs e)
        {
            ShowDatePicker();
        }

        void SetDate(DateTime date)
        {
            this.Control.Text = date.ToString(Element.Format);
            Element.Date = date;
        }

        private void ShowDatePicker()
        {
            CreateDatePickerDialog(this.Element.Date.Year, this.Element.Date.Month - 1, this.Element.Date.Day);
            _dialog.Show();
        }

        void CreateDatePickerDialog(int year, int month, int day)
        {
            ExtendedDatePicker view = Element;

            _dialog = new DatePickerDialog(Context, AlertDialog.ThemeHoloLight, (o, e) =>
            {
                view.Date = e.Date;
                ((IElementController)view).SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
                Control.ClearFocus();

                _dialog = null;
            }, year, month, day);
        }
    }
}