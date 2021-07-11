<script>
import RAKUpdateButton from 'rak_nuxt/components/Buttons/RAKUpdateButton'
import EXIButton from '@/components/Buttons/EXIButton'

export default {
  name: 'EXIUpdateButton',
  extends: RAKUpdateButton,
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
