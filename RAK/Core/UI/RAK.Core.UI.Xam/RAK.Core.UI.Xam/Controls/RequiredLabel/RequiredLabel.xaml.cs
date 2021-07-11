using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.Controls.RequiredLabel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequiredLabel : ContentView
    {
        public string RequiredIdentifier { get; set; }
        public string LabelText { get; set; }

        RequiredLabelViewModel VM;
        public RequiredLabel()
        {
            InitializeComponent();
        }

        public void Inicializar()
        {
            VM = new RequiredLabelViewModel();
            BindingContext = VM;

            if (!string.IsNullOrEmpty(RequiredIdentifier))
                VM.RequiredIdentifier = RequiredIdentifier;

            if (!string.IsNullOrEmpty(LabelText))
                VM.LabelText = LabelText;
        }

        protected override void OnParentSet()
        {
            this.Inicializar();
            base.OnParentSet();
        }
    }
}