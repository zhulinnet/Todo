import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";

import request from '@/util/request';
import element from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css';

Vue.config.productionTip = false;
Vue.prototype.$http = request;


Vue.use(element,{
  size:'mini'
});

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
