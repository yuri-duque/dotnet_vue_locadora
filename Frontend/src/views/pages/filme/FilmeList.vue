<template>
  <vs-card class="d-theme-dark-bg p-2">
    <vs-table :data="users">
      <template slot="header">
        <vs-row vs-type="flex" vs-justify="space-between" class="mb-6">
          <h3 class="mt-2">Filmes</h3>

          <vs-button color="success" type="filled" :to="{ name: 'filme-cadastro'}">Cadastrar filme</vs-button>
        </vs-row>
      </template>

      <template slot="thead">
        <vs-th>Id</vs-th>
        <vs-th>Titulo</vs-th>
        <vs-th>Classificacao</vs-th>
        <vs-th>Lançamento</vs-th>
      </template>

      <template slot-scope="{data}">
        <vs-tr :key="indextr" v-for="(tr, indextr) in data">
          <vs-td>{{data[indextr].id}}</vs-td>
          <vs-td>{{data[indextr].titulo}}</vs-td>
          <vs-td>{{data[indextr].classificacaoIndicativa}}</vs-td>
          <vs-td>{{data[indextr].lancamento}}</vs-td>
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
        titulo: "Filme 1 - update",
        classificacaoIndicativa: 10,
        lancamento: true,
      },
      {
        id: 1,
        titulo: "Filme 1 - update",
        classificacaoIndicativa: 10,
        lancamento: true,
      },
      {
        id: 1,
        titulo: "Filme 1 - update",
        classificacaoIndicativa: 10,
        lancamento: true,
      },
    ],
  }),
};
</script>