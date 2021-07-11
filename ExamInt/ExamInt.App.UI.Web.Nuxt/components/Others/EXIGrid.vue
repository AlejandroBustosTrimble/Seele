<script>
import RAKGrid from 'rak_nuxt/components/Others/RAKGrid.vue'
import EXISection from '@/components/Others/EXISection'

export default {
  name: 'EXIGrid',
  extends: RAKGrid,
  props:
    {
      sectionComponent: {
        type: [String, Object],
        default: () => { return EXISection },
        description: 'Componente que va a desempeñarse como seccion'
      }
    }
}
</script>
