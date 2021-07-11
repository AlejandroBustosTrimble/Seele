<template>
  <div>
    <b-navbar
      class="rakheader"
      toggleable="sm"
      fixed="top"
      sticky
      type="dark"
      variant="info"
    >
      <!-- <b-navbar-brand href="#">NavBar</b-navbar-brand> -->

      <b-navbar-toggle target="nav-collapse" />

      <b-collapse id="nav-collapse" is-nav>
        <!-- <b-navbar-nav>
                    <b-nav-item href="#">Link</b-nav-item>
                    <b-nav-item href="#" disabled>Disabled</b-nav-item>
                </b-navbar-nav> -->

        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto">
          <!-- <b-nav-form>
                        <b-form-input size="sm" class="mr-sm-2" placeholder="Search"></b-form-input>
                        <b-button size="sm" class="my-2 my-sm-0" type="submit">Search</b-button>
                    </b-nav-form> -->

          <b-nav-item-dropdown right>
            <!-- Using 'button-content' slot -->
            <template v-slot:button-content>
              <em>TODO_RAK</em>
            </template>
            <b-dropdown-item href="#">
              Perfil
            </b-dropdown-item>
            <b-dropdown-item href="#">
              Cerrar Sesion
            </b-dropdown-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </div>
</template>
<script>
export default {
  name: 'RAKHeader'
}
</script>
