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
          <vs-td>{{mascara_CPF(data[indextr].cpf)}}</vs-td>
          <vs-td>{{formatar_Data(data[indextr].dataNascimento)}}</vs-td>

          <vs-td>
            <vs-row vs-type="flex" vs-justify="space-around" vs-align="center" class="pointer">
              <vs-button
                color="primary"
                type="flat"
                class="p-0"
                :to="`cliente/editar?id=${data[indextr].id}`"
              >
                <feather-icon class="w-5 h-5" icon="EditIcon" title="Editar" />
              </vs-button>

              <vs-button color="danger" type="flat" class="p-0">
                <feather-icon
                  class="w-5 h-5"
                  icon="TrashIcon"
                  title="Excluir"
                  @click="openConfirmDialog(data[indextr].id)"
                />
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

import { mascaraCPF } from "@/assets/utils/mask";

export default {
  data: () => ({
    clientes: [],

    id: null,
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

    excluir() {},

    openConfirmDialog(id) {
      this.id = id;
      this.$vs.dialog({
        type: "confirm",
        color: "danger",
        title: `Confirmação`,
        acceptText: "Sim",
        cancelText: "Cancelar",
        text: "Deseja realmente deletar esse cliente?",
        accept: this.deletar,
      });
    },

    deletar() {
      this.$vs.loading();
      api_cliente
        .deletar(this.id)
        .then(() => {
          this.$vs.loading.close();

          this.$vs.notify({
            color: "success",
            title: "Cliente deletado!",
            text: "Cliente deletado com sucesso",
          });

          this.getAll();
        })
        .catch((error) => {
          var exception = utils.getError(error);

          this.$vs.loading.close();
          this.$vs.notify({
            color: "danger",
            title: "Erro ao deletar o cliente!",
            text: exception,
          });
        });
    },

    formatar_Data(dataString) {
      if (!dataString) return null;

      var data = new Date(dataString);

      return data.toLocaleDateString("pt-BR");
    },

    mascara_CPF(cpf) {
      return mascaraCPF(cpf);
    },
  },

  watch: {
    dataNascimento(newValue) {
      return this.formatarData(newValue);
    },
  },
};
</script>