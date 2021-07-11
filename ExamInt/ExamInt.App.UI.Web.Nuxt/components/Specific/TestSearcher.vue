<script>
import EXISearcher from '@/components/Editors/EXISearcher.vue'

export default {
  name: 'TestSearcher',
  extends: EXISearcher,
  methods: {
    fetchData (request) {
      const tempDDD = [
        {
          ID: 1,
          Name: 'AleeBustos'
        },
        {
          ID: 2,
          Name: 'Guillermo'
        },
        {
          ID: 3,
          Name: 'Bustos'
        },
        {
          ID: 4,
          Name: 'Rakna'
        },
        {
          ID: 5,
          Name: 'NatiBustos'
        },
        {
          ID: 6,
          Name: 'Florencia'
        }
      ]

      const returnList = tempDDD.filter((item) => { return item.Name.includes(request.Name) || item.ID === request.ID })

      this.setItems(returnList)
    }
  }
}
</script>
