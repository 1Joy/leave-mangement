// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import ElementUI from 'element-ui'
import Echarts from 'echarts'
import router from './router'
import {store} from './store'
import $ from 'jquery'
import './assets/styles/index.scss'
import './assets/styles/iconfont.css'
import 'element-ui/lib/theme-chalk/index.css'
import './permission'
import Toast from 'vue-toast-component'
import BaiduMap from 'vue-baidu-map'

Vue.config.productionTip = false
window.jQuery = window.$ = $;
Vue.use(ElementUI, {
  size: 'medium',
});
Vue.use(BaiduMap, {
  ak: 'pIFAC8QgGzxDLRT4hFThQG5DUuiTCSf2'
});
Vue.use(Toast)
Vue.prototype.$echarts = Echarts 
/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
})
