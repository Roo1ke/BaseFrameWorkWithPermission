!function(e,t){if("object"==typeof exports&&"object"==typeof module)module.exports=t(require("vue"));else if("function"==typeof define&&define.amd)define(["vue"],t);else{var n="object"==typeof exports?t(require("vue")):t(e.Vue);for(var o in n)("object"==typeof exports?exports:e)[o]=n[o]}}(window,function(e){return function(e){var t={};function n(o){if(t[o])return t[o].exports;var r=t[o]={i:o,l:!1,exports:{}};return e[o].call(r.exports,r,r.exports,n),r.l=!0,r.exports}return n.m=e,n.c=t,n.d=function(e,t,o){n.o(e,t)||Object.defineProperty(e,t,{enumerable:!0,get:o})},n.r=function(e){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},n.t=function(e,t){if(1&t&&(e=n(e)),8&t)return e;if(4&t&&"object"==typeof e&&e&&e.__esModule)return e;var o=Object.create(null);if(n.r(o),Object.defineProperty(o,"default",{enumerable:!0,value:e}),2&t&&"string"!=typeof e)for(var r in e)n.d(o,r,function(t){return e[t]}.bind(null,r));return o},n.n=function(e){var t=e&&e.__esModule?function(){return e.default}:function(){return e};return n.d(t,"a",t),t},n.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},n.p="",n(n.s=162)}({0:function(e,t,n){"use strict";var o=n(2),r=n.n(o),i=function(e){e.component(this.name,this)},u=function(e){return e.mixins=e.mixins||[],e.components=e.components||{},e.install=e.install||i,e},s=function(e){return e.changedTouches[0]||e.touches[0]};n.d(t,"c",function(){return a}),n.d(t,"a",function(){return u}),n.d(t,"b",function(){return s});var a=r.a.prototype.$isServer},1:function(e,t,n){"use strict";function o(e,t,n,o,r,i,u,s){var a,c="function"==typeof e?e.options:e;if(t&&(c.render=t,c.staticRenderFns=n,c._compiled=!0),o&&(c.functional=!0),i&&(c._scopeId="data-v-"+i),u?(a=function(e){(e=e||this.$vnode&&this.$vnode.ssrContext||this.parent&&this.parent.$vnode&&this.parent.$vnode.ssrContext)||"undefined"==typeof __VUE_SSR_CONTEXT__||(e=__VUE_SSR_CONTEXT__),r&&r.call(this,e),e&&e._registeredComponents&&e._registeredComponents.add(u)},c._ssrRegister=a):r&&(a=s?function(){r.call(this,this.$root.$options.shadowRoot)}:r),a)if(c.functional){c._injectStyles=a;var f=c.render;c.render=function(e,t){return a.call(t),f(e,t)}}else{var l=c.beforeCreate;c.beforeCreate=l?[].concat(l,a):[a]}return{exports:e,options:c}}n.d(t,"a",function(){return o})},117:function(e,t,n){},162:function(e,t,n){"use strict";n.r(t);var o=n(0),r=Object(o.a)({name:"wv-loadmore",props:{type:{type:String,default:"default"},text:{type:String,default:"正在加载"}}}),i=(n(232),n(1)),u=Object(i.a)(r,function(){var e=this,t=e.$createElement,n=e._self._c||t;return n("div",{staticClass:"weui-loadmore",class:{"weui-loadmore_line":"line"===e.type||"lineDot"===e.type,"weui-loadmore_dot":"lineDot"===e.type}},["default"===e.type?n("i",{staticClass:"weui-loading"}):e._e(),e._v(" "),n("span",{staticClass:"weui-loadmore__tips",domProps:{textContent:e._s("lineDot"===e.type?"":e.text)}})])},[],!1,null,"5090a40a",null);t.default=u.exports},2:function(t,n){t.exports=e},232:function(e,t,n){"use strict";var o=n(117);n.n(o).a}})});