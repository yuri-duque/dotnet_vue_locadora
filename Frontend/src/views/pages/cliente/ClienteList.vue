<template>
  <vs-card class="d-theme-dark-bg p-2">
    <vs-table :data="clientes">
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
        <vs-th>Ações</vs-th>
      </template>

      <template slot-scope="{data}">
        <vs-tr :key="indextr" v-for="(tr, indextr) in data">
          <vs-td>{{data[indextr].id}}</vs-td>
          <vs-td>{{data[indextr].nome}}</vs-td>
          <vs-td>{{mascaraCPF(data[indextr].cpf)}}</vs-td>
          <vs-td>{{formatarData(data[indextr].dataNascimento)}}</vs-td>

          <vs-td>
            <vs-row vs-type="flex" vs-justify="space-around" vs-align="center" class="pointer">
              <vs-button color="primary" type="flat" class="p-0" :to="`cliente/editar?id=${data[indextr].id}`">
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
import api_cliente from "@/api/api_cliente";
import utils from "@/assets/utils";

export default {
  data: () => ({
    clientes: [],
  }),

  created() {
    this.getAll();
  },

  methods: {
    getAll() {
      this.$vs.loading();
      api_cliente
        .getAll()
        .then((response) => {
          this.$vs.loading.close();

          this.clientes = response.data;
        })
        .catch((error) => {
          var exception = utils.getError(error);

          this.$vs.loading.close();
          this.$vs.notify({
            color: "danger",
            title: "Erro ao carregar as clientes!",
            text: exception,
          });
        });
    },

    formatarData(dataString) {
      if (!dataString) return null;

      var data = new Date(dataString);

      return data.toLocaleDateString("pt-BR");
    },

    mascaraCPF(cpf) {
      cpf = cpf.replace(/\D/g, "");
      cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2");
      cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2");
      cpf = cpf.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
      return cpf;
    },
  },

  watch:{
    dataNascimento(newValue){
      return this.formatarData(newValue);
    }
  }
};
</script>