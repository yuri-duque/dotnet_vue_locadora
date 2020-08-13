/*=========================================================================================
  File Name: router.js
  Description: Routes for vue-router. Lazy loading is enabled.
  ----------------------------------------------------------------------------------------
  Item Name: Vuexy - Vuejs, HTML & Laravel Admin Dashboard Template
  Author: Pixinvent
  Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/


import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

const router = new Router({
    mode: 'history',
    base: process.env.BASE_URL,
    scrollBehavior () {
        return { x: 0, y: 0 }
    },
    routes: [

        {
            path: '',
            component: () => import('./layouts/main/Main.vue'),
            children: [
              // LOCACAO
              {
                path: '/',
                name: 'locacao-list',
                component: () => import('./views/pages/locacao/LocacaoList.vue')
              },
              {
                path: '/locacao/cadastro',
                name: 'locacao-cadastro',
                component: () => import('./views/pages/locacao/LocacaoForm.vue')
              },
              {
                path: '/locacao/editar',
                name: 'locacao-editar',
                component: () => import('./views/pages/locacao/LocacaoForm.vue')
              },
              
              // CLIENTE
              {
                path: '/cliente',
                name: 'cliente-list',
                component: () => import('./views/pages/cliente/ClienteList.vue')
              },
              {
                path: '/cliente/cadastro',
                name: 'cliente-cadastro',
                component: () => import('./views/pages/cliente/ClienteForm.vue')
              },
              {
                path: '/cliente/editar',
                name: 'cliente-editar',
                component: () => import('./views/pages/cliente/ClienteForm.vue')
              },

              // FILME
              {
                path: '/filme',
                name: 'filme-list',
                component: () => import('./views/pages/filme/FilmeList.vue')
              },
              {
                path: '/filme/cadastro',
                name: 'filme-cadastro',
                component: () => import('./views/pages/filme/FilmeForm.vue')
              },
              {
                path: '/filme/editar',
                name: 'filme-editar',
                component: () => import('./views/pages/filme/FilmeForm.vue')
              },
            ],
        },
        {
            path: '',
            component: () => import('@/layouts/full-page/FullPage.vue'),
            children: [
              {
                path: '/pages/error-404',
                name: 'page-error-404',
                component: () => import('@/views/pages/Error404.vue')
              },
            ]
        },
        // Redirect to 404 page, if no match found
        {
            path: '*',
            redirect: '/pages/error-404'
        }
    ],
})

router.afterEach(() => {
  // Remove initial loading
  const appLoading = document.getElementById('loading-bg')
    if (appLoading) {
        appLoading.style.display = "none";
    }
})

export default router
