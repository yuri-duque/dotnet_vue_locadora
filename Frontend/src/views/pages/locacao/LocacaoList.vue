<template>
  <vs-card class="d-theme-dark-bg p-2">
    <vs-table :data="users">
      <template slot="header">
        <vs-row vs-type="flex" vs-justify="space-between" class="mb-6">
            <h3 class="mt-2">Locações</h3>
            
            <vs-button color="success" type="filled" :to="{ name: 'locacao-cadastro'}">Cadastrar locação</vs-button>
        </vs-row>
      </template>

      <template slot="thead">
        <vs-th>Id</vs-th>
        <vs-th>Nome Cliente</vs-th>
        <vs-th>Titulo Filme</vs-th>
        <vs-th>Data Locação</vs-th>
        <vs-th>Data Devolução</vs-th>
      </template>

      <template slot-scope="{data}">
        <vs-tr :key="indextr" v-for="(tr, indextr) in data">
          <vs-td>{{data[indextr].id}}</vs-td>
          <vs-td>{{data[indextr].cliente.nome}}</vs-td>
          <vs-td>{{data[indextr].filme.titulo}}</vs-td>
          <vs-td>{{data[indextr].dataLocacao}}</vs-td>
          <vs-td>{{data[indextr].dataDevolucao}}</vs-td>
        </vs-tr>
      </template>
    </vs-table>
  </vs-card>
</template>

<script>
export default {
  data: () => ({
    users: [
      {
        id: 1,
        cliente: {
            nome: "yuri-update",
        },
        filme: {
            titulo: "Filme 1 - update",
            lancamento: true
        },
        dataLocacao: "2020-08-11T20:21:02.863198",
        dataDevolucao: "2020-08-12T20:21:12.517885"
      },
      {
        id: 1,
        cliente: {
            nome: "yuri-update",
        },
        filme: {
            titulo: "Filme 1 - update",
            lancamento: true
        },
        dataLocacao: "2020-08-11T20:21:02.863198",
        dataDevolucao: "2020-08-12T20:21:12.517885"
      },
      {
        id: 1,
        cliente: {
            nome: "yuri-update",
        },
        filme: {
            titulo: "Filme 1 - update",
            lancamento: true
        },
        dataLocacao: "2020-08-11T20:21:02.863198",
        dataDevolucao: "2020-08-12T20:21:12.517885"
      },
    ],
  }),
};
</script>