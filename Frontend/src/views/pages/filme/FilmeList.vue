<template>
  <vs-card class="d-theme-dark-bg p-2">
    <vs-table :data="filmes">
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
        <vs-th>Ações</vs-th>
      </template>

      <template slot-scope="{data}">
        <vs-tr :key="indextr" v-for="(tr, indextr) in data">
          <vs-td>{{data[indextr].id}}</vs-td>
          <vs-td>{{data[indextr].titulo}}</vs-td>
          <vs-td>{{data[indextr].classificacaoIndicativa}}</vs-td>
          <vs-td>{{data[indextr].lancamento}}</vs-td>

          <vs-td>
            <vs-row vs-type="flex" vs-justify="space-around" vs-align="center">
              <vs-button color="primary" type="flat" class="p-0" :to="`filme/editar?id=${data[indextr].id}`">
                <feather-icon class="w-5 h-5" icon="EditIcon" title="Editar" />
              </vs-button>

              <vs-button color="danger" type="flat" class="p-0">
                <feather-icon class="w-5 h-5" icon="TrashIcon" title="Excluir" />
              </vs-button>
            </vs-row>
          </vs-td>
        </vs-tr>
      </template>
    </vs-table>
  </vs-card>
</template>

<script>
import api_filme from "@/api/api_filme";
import utils from "@/assets/utils";

export default {
  data: () => ({
    filmes: [],
  }),

  created() {
    this.getAll();
  },

  methods: {
    getAll() {
      this.$vs.loading();
      api_filme
        .getAll()
        .then((response) => {
          this.$vs.loading.close();

          this.filmes = response.data;
        })
        .catch((error) => {
          var exception = utils.getError(error);

          this.$vs.loading.close();
          this.$vs.notify({
            color: "danger",
            title: "Erro ao carregar as filmes!",
            text: exception,
          });
        });
    },
  },
};
</script>