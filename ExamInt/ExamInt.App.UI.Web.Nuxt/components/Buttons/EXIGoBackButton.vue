<script>
import RAKGoBackButton from 'rak_nuxt/components/Buttons/RAKGoBackButton'
import EXIButton from '@/components/Buttons/EXIButton'

export default {
  name: 'EXIGoBackButton',
  extends: RAKGoBackButton,
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
