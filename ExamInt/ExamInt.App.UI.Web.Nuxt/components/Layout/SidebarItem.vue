<template>
    <li :class="CssFullClass">
      <nuxt-link :to="link.Path"
              @click.native="linkClick"
              :target="link.target"
              :href="link.Path">
          <template>
              <i :class="link.icon"></i>
              <span class="nav-link-text">{{ link.Name }}</span>
              <span  v-if="HasChildren" class="float-right">
                <fa :icon="ChildrenMenuIcon"/>
              </span>
          </template>
      </nuxt-link>
      <ul v-if="HasChildren" :class="ChildrenMenuCssClass">
          <template v-for="cMod in link.Children">
            <sidebar-item v-bind:key="cMod.EntityID" :link="cMod" />
          </template>
      </ul>
    </li>
</template>
<script>
  export default {
    name: 'sidebar-item',
    props: {
      link: {
        type: Object,
        default: () => {
          return {
            Name: '',
            Path: '',
            Children: []
          };
        },
        description:
          'Links'
      },
      mainClassProp: {
            type: String,
            default: "",
            description: "clase principal"
          },
    },
    inject: {
      autoClose: {
        default: true
      }
    },
    data() {
      return {
        Children: [],
        collapsed: true,
        mainClass: this.mainClassProp,
      };
    },
    computed: {
            // a computed getter
            CssFullClass: function () {
                var cssClass = 'nav-item ' + this.mainClass;
                
                if(!this.collapsed)
                {
                  cssClass = cssClass + ' active';
                }

                return cssClass;
            },
            ChildrenMenuCssClass: function ()
            {
                var baseCssClass = 'treeview-menu';

                if(!this.collapsed)
                {
                    baseCssClass = baseCssClass + ' menu-open';
                }

                return baseCssClass;
            },
            ChildrenMenuIcon: function()
            {
                if(this.collapsed)
                {
                    return ['fa', 'angle-left'];
                }
                else
                {
                    return ['fa', 'angle-down'];
                }
            },
            HasChildren: function ()
            {
                return (this.link.Children && this.link.Children.length);
            }
        },
    methods: {
      linkClick() {
        if (
          this.autoClose &&
          this.$sidebar &&
          this.$sidebar.showSidebar === true
        ) {
          this.$sidebar.displaySidebar(false);
        }

        if(this.HasChildren)
        {
            this.collapsed = !this.collapsed;
        }
      }
    }
  };
</script>
<style scoped>

.nav-link-text, .nav-link>i {
    font-size: 12px;
    color: #fff;
    border-left: 3px solid transparent;
}

.nav-item>a {
  display: block;
  text-decoration: none !important;
  border-left: 3px solid transparent;
}

/* Item donde estoy parado */
.nav-item:hover>a{
    border-left-color: #19b5fe;
    /* TODO_RAK: Poner en scss los colores (Remarcar) #19b5fe,  (Fondo Oscuro) #111 y (Texto en fondo Oscuro) #fff */
}

/* Icono de Despliege de submenu */
a.nuxt-link-active
{
  color:  #19b5fe;
}

/* Menu Desplegable */
.treeview-menu {
    margin: 0 1px;
    display: none;
    list-style: none;
    padding-left: 15px !important;
    margin:0;
    padding-left: 5px;
}

.menu-open {
    display: block !important;
}

</style>