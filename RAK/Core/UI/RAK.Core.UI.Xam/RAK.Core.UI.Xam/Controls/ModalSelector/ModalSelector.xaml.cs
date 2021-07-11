using RAK.Core.UI.Xam.General;
using RAK.Core.UI.Xam.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalSelector : ContentPage
    {
        /// <summary>
        /// View Model
        /// </summary>
        ModalSelectorSearchViewModel vm;

        RequestType requestType;

        /// <summary>
        /// Task
        /// </summary>
        private TaskCompletionSource<ModalSelectorViewModel> _taskCompletionSource;

        /// <summary>
        /// Api a llamar para obtener entidades
        /// </summary>
        private string api;

        private object parameters;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entities"></param>
        public ModalSelector(string api, RequestType requestType, bool permiteBuscar, object parameters)
        {
            // -- Asignamos propiedades
            this.api = api;
            if (parameters != "")
                this.parameters = parameters;
            this.requestType = requestType;

            vm = new ModalSelectorSearchViewModel();
            vm.PermiteBuscar = permiteBuscar;
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
                _taskCompletionSource.SetResult((ModalSelectorViewModel)e.Item);
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
            this.vm.Entities = new List<ModalSelectorViewModel>();

            if (vm.SearchText.Length >= 3 || !vm.PermiteBuscar)
            {


                var added = this.parameters.AddProperty("Text", vm.SearchText);

                if (this.requestType == RequestType.Post)
                {
                    // -- Ejecutamos
                    var res = await ServiceHelper.PostList<ModalSelectorViewModel>(this.api, this.parameters.AddProperty("Text", vm.SearchText));
                    // -- Seteamos las entidades
                    this.vm.Entities = res.ResponseVM;
                }
                else if (this.requestType == RequestType.Get)
                {
                    ParameterCollection param = new ParameterCollection();
                    foreach (var item in added)
                    {
                        if (item.Value != null)
                            param.Add(item.Key, item.Value.ToString());
                        else
                            param.Add(item.Key, "");
                    }

                    // -- Ejecutamos
                    var res = await ServiceHelper.GetList<ModalSelectorViewModel>(this.api, param);
                    // -- Seteamos las entidades
                    this.vm.Entities = res.ResponseVM;
                }
            }
        }

        private async void btnClose_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
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

        /// <summary>
        /// Obtiene entidad
        /// </summary>
        /// <returns></returns>
        private Task<ModalSelectorViewModel> GetEntity()
        {
            _taskCompletionSource = new TaskCompletionSource<ModalSelectorViewModel>();
            return _taskCompletionSource.Task;
        }

        /// <summary>
        /// Metodo estatico a llamar desde las otras paginas
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="navigation"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static async Task<ModalSelectorViewModel> Entity(INavigation navigation, string api, RequestType requestType, bool permiteBuscar, object parameters = null)
        {
            var viewModel = new ModalSelector(api, requestType, permiteBuscar, parameters);
            await navigation.PushModalAsync(viewModel);
            var entity = await viewModel.GetEntity();
            await navigation.PopModalAsync();

            return entity;
        }
    }

    public enum RequestType
    {
        Post,
        Get
    }

    /// <summary>
    /// View Model para la pagina
    /// </summary>
    public class ModalSelectorSearchViewModel : ViewModelBase
    {
        /// <summary>
        /// Member de entidades
        /// </summary>
        private List<ModalSelectorViewModel> entities = new List<ModalSelectorViewModel>();
        private string searchText = "";
        private bool permiteBuscar = true;

        /// <summary>
        /// Propiedad de entidades
        /// </summary>
        public List<ModalSelectorViewModel> Entities
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