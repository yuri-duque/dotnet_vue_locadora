<template>
  <vs-card class="d-theme-dark-bg p-2">
    <vs-table :data="users">
      <template slot="header">
        <vs-row vs-type="flex" vs-justify="space-between" class="mb-6">
          <h3 class="mt-2">Clientes</h3>

          <vs-button
            color="success"
            type="filled"
            :to="{ name: 'cliente-cadastro'}"
          >Cadastrar cliente</vs-button>
        </vs-row>
      </template>

      <template slot="thead">
        <vs-th>Id</vs-th>
        <vs-th>Nome</vs-th>
        <vs-th>CPF</vs-th>
        <vs-th>Data de nascimento</vs-th>
      </template>

      <template slot-scope="{data}">
        <vs-tr :key="indextr" v-for="(tr, indextr) in data">
          <vs-td>{{data[indextr].id}}</vs-td>
          <vs-td>{{data[indextr].nome}}</vs-td>
          <vs-td>{{data[indextr].cpf}}</vs-td>
          <vs-td>{{data[indextr].dataNascimento}}</vs-td>
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
        cpf: "00000000000",
        nome: "yuri-update",
        dataNascimento: "1997-01-22T00:00:00",
      },
      {
        id: 1,
        cpf: "00000000000",
        nome: "yuri-update",
        dataNascimento: "1997-01-22T00:00:00",
      },
      {
        id: 1,
        cpf: "00000000000",
        nome: "yuri-update",
        dataNascimento: "1997-01-22T00:00:00",
      },
    ],
  }),
};
</script>