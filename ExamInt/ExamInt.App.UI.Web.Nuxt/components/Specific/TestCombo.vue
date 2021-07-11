<script>
import EXICombo from '@/components/Editors/EXICombo.vue'

export default {
  name: 'TestCombo',
  extends: EXICombo,
  methods: {
    fetchData () {
      // TODO_RAK: este combo seria de ejemplo
      const pitems = [
        {
          EntityID: 1,
          Name: 'Sarasa'
        },
        {
          EntityID: 2,
          Name: 'Lelele'
        },
        {
          EntityID: 3,
          Name: 'ererer'
        },
        {
          EntityID: 4,
          Name: 'eEeMMNMNM'
        },
        {
          EntityID: 5,
          Name: 'zzzz'
        }
      ]

      this.setItems(pitems)
    }
  }
}
</script>
