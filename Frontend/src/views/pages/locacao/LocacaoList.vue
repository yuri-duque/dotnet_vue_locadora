<template>
  <vs-card class="d-theme-dark-bg p-2">
    <vs-table :data="locacoes">
      <template slot="header">
        <vs-row vs-type="flex" vs-justify="space-between" class="mb-6">
          <h3 class="mt-2">Locações</h3>

          <vs-button
            color="success"
            type="filled"
            :to="{ name: 'locacao-cadastro'}"
          >Cadastrar locação</vs-button>
        </vs-row>
      </template>

      <template slot="thead">
        <vs-th>Id</vs-th>
        <vs-th>Nome Cliente</vs-th>
        <vs-th>Titulo Filme</vs-th>
        <vs-th>Data Locação</vs-th>
        <vs-th>Data Devolução</vs-th>
        <vs-th>Ações</vs-th>
      </template>

      <template slot-scope="{data}">
        <vs-tr :key="indextr" v-for="(tr, indextr) in data">
          <vs-td>{{data[indextr].id}}</vs-td>
          <vs-td>{{data[indextr].cliente.nome}}</vs-td>
          <vs-td>{{data[indextr].filme.titulo}}</vs-td>
          <vs-td>{{formatarData(data[indextr].dataLocacao)}}</vs-td>
          <vs-td>{{formatarData(data[indextr].dataDevolucao)}}</vs-td>
          <vs-td>
            <vs-row vs-type="flex" vs-justify="space-around" vs-align="center">
              <vs-button color="primary" type="flat" class="p-1" @click="devolver(data[indextr].id)">
                <feather-icon class="w-5 h-5" icon="TruckIcon" title="Devolver" />
              </vs-button>

              <vs-button color="primary" type="flat" class="p-1" :to="`locacao/editar?id=${data[indextr].id}`">
                <feather-icon class="w-5 h-5" icon="EditIcon" title="Editar" />
              </vs-button>

              <vs-button color="danger" type="flat" class="p-1" @click="excluir(data[indextr].id)">
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
import api_locacao from "@/api/api_locacao";
import utils from "@/assets/utils";

export default {
  data: () => ({
    locacoes: [],
  }),

  created() {
    this.getAll();
  },

  methods: {
    getAll() {
      this.$vs.loading();
      api_locacao
        .getAll()
        .then((response) => {
          this.$vs.loading.close();

          this.locacoes = response.data;
        })
        .catch((error) => {
          var exception = utils.getError(error);

          this.$vs.loading.close();
          this.$vs.notify({
            color: "danger",
            title: "Erro ao carregar as locações!",
            text: exception,
          });
        });
    },

    formatarData(dataString) {
      if (!dataString) return null;

      var data = new Date(dataString);

      return data.toLocaleString();
    },

    devolver(){

    },

    excluir(){

    }
  },
};
</script>