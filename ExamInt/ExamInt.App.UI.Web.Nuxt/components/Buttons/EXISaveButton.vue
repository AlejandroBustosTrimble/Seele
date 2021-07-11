<script>
import RAKSaveButton from 'rak_nuxt/components/Buttons/RAKSaveButton'
import EXIButton from '@/components/Buttons/EXIButton'

export default {
  name: 'EXISaveButton',
  extends: RAKSaveButton,
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
