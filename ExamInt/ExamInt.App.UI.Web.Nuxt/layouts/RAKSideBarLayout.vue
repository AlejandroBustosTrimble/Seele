<template>
  <div>
    <side-bar
      short-title="EXIUM"
      title="EXIUM"
      logo=""
      main-class-prop="rakSideBar"
      :module-list="this.ModuleList"
    />
    <rak-header class="rakHeader" />
    <div class="main">
      <!-- your content here -->
      <nuxt />
    </div>
  </div>
</template>
<script>
import { FadeTransition } from 'vue2-transitions'
// import RAKHeader from '~/components/Layout/RAKHeader.vue';

export default {
  name: 'RAKSideBarLayout',
  components: {
    FadeTransition
  },
  data () {
    return {
      ModuleList: []
    }
  },
  beforeMount () {
    this.LoadModules()
  },
  methods: {
    LoadModules () {
      const context = this

      // TODO_RAK: esto ponerlo en store asi al recargar la pagina (F5 o CTRL R)no va a buscar los items de menu si ya esta logueado
      // al igual que con los datos del usuario
      context.ModuleList = [
        {
          EntityID: 1,
          Name: 'Facturar',
          Icon: 'fab fa-acquisitions-incorporated',
          Path: '/'
        },
        {
          EntityID: 8,
          Name: 'Mantenimiento',
          Icon: '',
          Path: '',
          Children:
                    [
                      {
                        EntityID: 2,
                        Name: 'Clientes',
                        Icon: '',
                        Path: '/Clients'
                      },
                      {
                        EntityID: 3,
                        Name: 'Articulos',
                        Icon: '',
                        Path: '/Clients'
                      }
                    ]
        },
        {
          EntityID: 4,
          Name: 'Listados',
          Icon: '',
          Path: '',
          Children: [
            {
              EntityID: 5,
              Name: 'Listado de Facturas',
              Icon: '',
              Path: '/SettlementList'
            },
            {
              EntityID: 6,
              Name: 'Listado de Clientes',
              Icon: '',
              Path: '/ClientList'
            },
            {
              EntityID: 7,
              Name: 'Listado de Articulos',
              Icon: '',
              Path: 'ArticleList'
            }
          ]
        }
      ]
    }
  }
}
</script>
<style>

/* Style page content */
.main {
  /* Same as the width of the sidebar */
  margin-left: 13%;

  padding: 0px 10px;
}

.rakSideBar
{
    width: 13%;
}

.rakHeader
{
    margin-left: 13%;
}

</style>
