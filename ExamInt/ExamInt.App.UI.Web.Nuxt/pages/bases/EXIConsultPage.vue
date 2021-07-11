<script>
import RAKConsultPage from 'rak_nuxt/pages/RAKConsultPage'
import EXISection from '@/components/Others/EXISection'
import EXINewButton from '@/components/Buttons/EXINewButton'
import EXIFilterButton from '@/components/Buttons/EXIFilterButton'
import EXIGrid from '@/components/Others/EXIGrid'

export default {
  name: 'EXIConsultPage',
  extends: RAKConsultPage,
  props: {
    filterSectionComponent:
        {
          type: [String, Object],
          default: () => { return EXISection },
          description: 'Componente que va a desempeñarse como seccion'
        },
    newButtonComponent:
        {
          type: [String, Object],
          default: () => { return EXINewButton },
          description: 'Componente que va a desempeñarse como boton de Nuevo'
        },
    filterButtonComponent:
        {
          type: [String, Object],
          default: () => { return EXIFilterButton },
          description: 'Componente que va a desempeñarse como boton de filtro'
        },
    gridComponent:
        {
          type: [String, Object],
          default: () => { return EXIGrid },
          description: 'Componente que va a desempeñarse como Grilla'
        }
  }
}
</script>
