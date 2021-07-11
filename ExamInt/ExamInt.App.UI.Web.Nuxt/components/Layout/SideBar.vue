<template>
    <nav :class="cssFullClass" id="sidenav-main">
        <div class="container-fluid">

            <!--Toggler-->
            <!-- <navbar-toggle-button @click.native="showSidebar">
                <span class="navbar-toggler-icon"></span>
            </navbar-toggle-button> -->
            <router-link v-show="showLogo" class="navbar-brand" to="/">
                <img :src="logo" class="navbar-brand-img" alt="...">
            </router-link>

            <span style="border-bottom:2px solid #d0d0d0;margin-bottom:-20px;"></span>

            <slot name="mobile-right">
                <ul class="nav align-items-center d-md-none">
                </ul>
            </slot>
            <slot></slot>
            <div class="navbar-collapse collapse show" id="sidenav-collapse-main">

                <div class="navbar-collapse-header d-md-none">
                    <div class="row">
                        <div class="col-6 collapse-brand">
                            <router-link to="/">
                                <img :src="logo">
                            </router-link>
                        </div>
                        <div class="col-6 collapse-close">
                            <!-- <navbar-toggle-button @click.native="closeSidebar"></navbar-toggle-button> -->
                        </div>
                    </div>
                </div>

                <ul class="navbar-nav">
                    <slot name="links">
                      <template v-for="moduleItem in ModuleList">
                        <side-bar-item v-bind:key="moduleItem.EntityID" :link="{Name: moduleItem.Name, Icon: moduleItem.Icon, Path: moduleItem.Path, Children: moduleItem.Children}" />
                      </template>
                    </slot>
                </ul>
                
            </div>
          </div>
    </nav>
</template>
<script>
  

  export default {
    name: 'sidebar',
    components: {
      
    },
    props: {
      logo: {
        type: String,
        default: 'img/brand/logoh.png',
        description: 'Icono'
      },
      autoClose: {
        type: Boolean,
        default: true,
        description: 'AutoCierre'
      },
      mainClassProp: {
            type: String,
            default: "",
            description: "ClaseCss"
          },
      ModuleList: {
            type: Array,
            default: undefined,
            description: ""
      }    },
   data() {
      return {
        
      };
    },
    provide() {
      return {
        autoClose: this.autoClose
      };
    },
    computed: {
            // a computed getter
            cssFullClass: function () {
                return 'sidenav ' + this.mainClassProp;
            },
            showLogo: function()
            {
              return (this.logo != undefined && this.logo != null && this.logo != '');
            }
        },
    methods: {
      closeSidebar() {
        // this.$sidebar.displaySidebar(false)
      },
      showSidebar() {
        // this.$sidebar.displaySidebar(true)
      },      
      logout(){       
        let context = this; 
        // this.$store.dispatch('logout').then(()=> {          
        //   context.$router.push('login') 
        // }) ;
      }
    },
    beforeDestroy() {
      // if (this.$sidebar.showSidebar) {
      //   this.$sidebar.showSidebar = false;
      // }
    }
  };
</script>
<style>
.sidenav {
  height: 100%; /* Full-height: remove this if you want "auto" height */
  position: fixed; /* Fixed Sidebar (stay in place on scroll) */
  z-index: 1; /* Stay on top */
  top: 0; /* Stay at the top */
  left: 0;
  background-color: #111; /* Black */
  overflow-x: hidden; /* Disable horizontal scroll */
  padding-top: 50px;
}

/* On smaller screens, where height is less than 450px, change the style of the sidebar (less padding and a smaller font size) */
@media screen and (max-height: 450px) {
  .sidenav {padding-top: 15px;}
  .sidenav a {font-size: 18px;}
}

</style> 