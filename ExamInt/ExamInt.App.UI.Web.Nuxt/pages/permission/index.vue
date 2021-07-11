<template>
  <!-- <div>
    {{ entityName }}
    <EXIButton text="GetById" @click="GetById" />
  </div> -->
  <EXIConsultPage
    :grid-fields="gridFields"
    :entity-key="entityKey"
    :entity-name="entityName"
    :repository="repository"
  >
    <template v-slot:filter :model="model">
      <!-- <EXINumeric v-model="model.request.nuuuum" /> -->
      <h1>Aquí podría ir un título de página</h1>
    </template>
  </EXIConsultPage>
</template>
<script>
import GridHelper from 'rak_nuxt/helpers/GridHelper.js'
import EXIConsultPage from '@/pages/bases/EXIConsultPage'
import PermissionRepository from '~/helpers/Repositories/PermissionRepository'

import EXINumeric from '~/components/Editors/EXINumeric.vue'
// import EXIButton from '~/components/Buttons/EXIButton.vue'

export default {
  name: 'PermissionIndex',
  layout: 'RAKSideBarLayout',
  components: {
    // EXIButton
    EXIConsultPage,
    EXINumeric
  },
  props: {

  },
  data () {
    return {
      gridFields: [
        GridHelper.createField('ID', 'ID'),
        GridHelper.createField('Name', 'Nombre'),
        GridHelper.createField('Surname', 'Apellido'),
        GridHelper.createField('RequestDateTime', 'Fecha Permiso'),
      ],
      entityKey: 'permission',
      entityName: 'Permiso',
      repository: PermissionRepository
    }
  },
  methods: {
    // GetById () {
    //   const self = this

    //   PermissionRepository.GetById(this, { ID: 1 },
    //     function (data) {
    //       debugger;
    //     })
    // },
  }
}
</script>
