using RAK.Core.UI.Xam.General;
using RAK.Core.UI.Xam.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalImageSelector : ContentPage
    {
        /// <summary>
        /// View Model
        /// </summary>
        ModalImageSelectorSearchViewModel vm;

        RequestType requestType;

        /// <summary>
        /// Task
        /// </summary>
        private TaskCompletionSource<ModalImageSelectorViewModel> _taskCompletionSource;

        /// <summary>
        /// Api a llamar para obtener entidades
        /// </summary>
        private string api;

        private object parameters;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entities"></param>
        public ModalImageSelector(string api, RequestType requestType, bool PermiteBuscar, string textoSinBusqueda, object parameters)
        {
            // -- Asignamos propiedades
            this.api = api;
            this.parameters = parameters;
            this.requestType = requestType;

            vm = new ModalImageSelectorSearchViewModel();
            vm.PermiteBuscar = PermiteBuscar;
            if (!string.IsNullOrEmpty(textoSinBusqueda))
                vm.TextoSinBusqueda = textoSinBusqueda;

            BindingContext = vm;

            InitializeComponent();
        }

        /// <summary>
        /// Tap item en la lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (_taskCompletionSource != null)
            {
                _taskCompletionSource.SetResult((ModalImageSelectorViewModel)e.Item);
                _taskCompletionSource = null;
            }
        }

        /// <summary>
        /// Cuando se ingresa texto en el cuadro de busqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            // -- Cancelamos el timer actual
            DisposeTimer();
            // -- Creamos el timer nuevamente
            timer = new System.Threading.Timer(TimerElapsed, null, VALIDATION_DELAY, VALIDATION_DELAY);
        }

        /// <summary>
        /// Obtiene las entidades llamando a la api
        /// </summary>
        /// <returns></returns>
        private async Task GetEntities()
        {
            this.vm.Entities = new List<ModalImageSelectorViewModel>();

            if (vm.SearchText.Length >= 3 || !vm.PermiteBuscar)
            {
                var added = this.parameters.AddProperty("Text", vm.SearchText);

                if (this.requestType == RequestType.Post)
                {
                    // -- Ejecutamos
                    //var res = new ModalImageSelectorViewModel();//await ServiceHelper.PostList<ModalImageSelectorViewModel>(this.api, this.parameters.AddProperty("Text", vm.SearchText));
                    // -- Seteamos las entidades
                    //this.vm.Entities = res.ResponseVM;
                }
                else if (this.requestType == RequestType.Get)
                {
                    ParameterCollection param = new ParameterCollection();
                    foreach (var item in added)
                    {
                        param.Add(item.Key, item.Value.ToString());
                    }

                    // -- Ejecutamos
                    var res = await ServiceHelper.GetList<ModalImageSelectorViewModel>(this.api, param);
                    // -- Seteamos las entidades
                    this.vm.Entities = res.ResponseVM;
                }
            }
        }

        /// <summary>
        /// Obtiene entidad
        /// </summary>
        /// <returns></returns>
        private Task<ModalImageSelectorViewModel> GetEntity()
        {
            _taskCompletionSource = new TaskCompletionSource<ModalImageSelectorViewModel>();
            return _taskCompletionSource.Task;
        }

        /// <summary>
        /// Metodo estatico a llamar desde las otras paginas
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="navigation"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static async Task<ModalImageSelectorViewModel> Entity(INavigation navigation, string api, RequestType requestType, bool PermiteBuscar, string TextoSinBusqueda, object parameters = null)
        {
            var viewModel = new ModalImageSelector(api, requestType, PermiteBuscar, TextoSinBusqueda, parameters);
            await navigation.PushModalAsync(viewModel);
            var entity = await viewModel.GetEntity();
            await navigation.PopModalAsync();

            return entity;
        }

        #region Timer

        // -- Delay del timer
        static int VALIDATION_DELAY = 1250;
        // -- Timer
        System.Threading.Timer timer = null;

        // -- Time elapsed
        private void TimerElapsed(Object obj)
        {
            DisposeTimer();
            GetEntities();
        }

        // -- Time Dispose
        private void DisposeTimer()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }

        #endregion

        private async void btnClose_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }

    public class ModalImageSelectorSearchViewModel : ViewModelBase
    {
        /// <summary>
        /// Member de entidades
        /// </summary>
        private List<ModalImageSelectorViewModel> entities = new List<ModalImageSelectorViewModel>();
        private string searchText = "";
        private bool permiteBuscar = true;
        private string textoSinBusqueda = "Ingrese 3 caracteres para buscar...";

        public string TextoSinBusqueda
        {
            get
            {
                return textoSinBusqueda;
            }
            set
            {
                textoSinBusqueda = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Propiedad de entidades
        /// </summary>
        public List<ModalImageSelectorViewModel> Entities
        {
            get
            {
                return entities;
            }
            set
            {
                entities = value;
                RaisePropertyChanged();
                RaisePropertyChanged("NoTieneTexto");
                RaisePropertyChanged("SinResultados");
            }
        }

        /// <summary>
        /// Texto para buscar
        /// </summary>
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                RaisePropertyChanged("NoTieneTexto");
                RaisePropertyChanged("SinResultados");
            }
        }

        /// <summary>
        /// Indica que tiene al menos 3 caracteres
        /// </summary>
        public bool NoTieneTexto
        {
            get
            {
                return SearchText.Length < 3 && PermiteBuscar;
            }
        }

        /// <summary>
        /// Indica si se encontraron registros
        /// </summary>
        public bool SinResultados
        {
            get
            {
                return Entities != null && Entities.Count == 0 && (SearchText.Length > 3 || !PermiteBuscar);
            }
        }

        public bool PermiteBuscar
        {
            get
            {
                return permiteBuscar;
            }
            set
            {
                permiteBuscar = value;
                RaisePropertyChanged();
            }
        }
    }
}