<script>
import EXIMultipleSelect from '@/components/Editors/EXIMultipleSelect.vue'

export default {
  name: 'TestMultipleSelect',
  extends: EXIMultipleSelect,
  methods: {
    fetchData (request) {
      const tempDDD = [
        { ID: 1, Name: 'Computacion' },
        { ID: 2, Name: 'Arte' },
        { ID: 3, Name: 'Matematica' },
        { ID: 4, Name: 'Lengua' }
      ]

      this.setItems(tempDDD)
    }
  }
}
</script>
