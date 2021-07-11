<script>
import RAKDeleteButton from 'rak_nuxt/components/Buttons/RAKDeleteButton'
import EXIButton from '@/components/Buttons/EXIButton'

export default {
  name: 'EXIDeleteButton',
  extends: RAKDeleteButton,
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
