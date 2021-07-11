<script>
import RAKNewButton from 'rak_nuxt/components/Buttons/RAKNewButton'
import EXIButton from '@/components/Buttons/EXIButton'

export default {
  name: 'EXINewButton',
  extends: RAKNewButton,
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
