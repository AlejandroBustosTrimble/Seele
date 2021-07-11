<script>
import RAKFilterButton from 'rak_nuxt/components/Buttons/RAKFilterButton.vue'
import EXIButton from '@/components/Buttons/EXIButton'

export default {
  name: 'EXIFilterButton',
  extends: RAKFilterButton,
  props: {
    buttonComponent:
        {
          type: [String, Object],
          default: () => { return EXIButton },
          description: 'Componente que va a desempeñarse como boton'
        }
  }
}
</script>
