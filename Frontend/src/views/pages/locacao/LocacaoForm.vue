<template>
  <vs-card>
    <div slot="header">
      <h3>Cadastro de locaçao</h3>
    </div>

    <div>
      <vs-row>
        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <span class="label-campo">Cliente</span>
          <v-select
            label="nome"
            v-model="selectCliente"
            :options="optionsClientes"
            class="display-inline-block mar-1"
          ></v-select>
        </vs-col>

        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <span class="label-campo">Filme</span>
          <v-select
            label="titulo"
            v-model="selectFilme"
            :options="optionsFilmes"
            class="display-inline-block mar-1"
          ></v-select>
        </vs-col>
      </vs-row>

      <vs-row vs-type="flex" vs-justify="flex-end" class="mt-6 pr-2">
        <vs-button v-if="!produtoId" @click="salvar()" class="font-semibold" type="filled">Cadastrar</vs-button>

        <vs-button v-if="produtoId" @click="salvar()" class="font-semibold" type="filled">Editar</vs-button>
      </vs-row>
    </div>
  </vs-card>
</template>

<script>
import vSelect from "vue-select";
import api_cliente from "@/api/api_cliente";
import api_filme from "@/api/api_filme";
import utils from "@/assets/utils";

export default {
  components: {
    "v-select": vSelect,
  },

  data() {
    return {
      selectCliente: null,
      optionsClientes: [],

      selectFilme: null,
      optionsFilmes: [],
    };
  },

  created() {
    this.getSelectClientes();
    this.getSelectFilmes();
  },

  methods: {
    getSelectClientes() {
      this.$vs.loading();
      api_cliente
        .getOptionsSelect()
        .then((response) => {
          this.$vs.loading.close();

          this.optionsClientes = response.data;
        })
        .catch((error) => {
          var exception = utils.getError(error);

          this.$vs.loading.close();
          this.$vs.notify({
            color: "danger",
            title: "Erro ao carregar as select clientes!",
            text: exception,
          });
        });
    },

    getSelectFilmes() {
      this.$vs.loading();
      api_filme
        .getOptionsSelect()
        .then((response) => {
          this.$vs.loading.close();

          this.optionsFilmes = response.data;
        })
        .catch((error) => {
          var exception = utils.getError(error);

          this.$vs.loading.close();
          this.$vs.notify({
            color: "danger",
            title: "Erro ao carregar as select filmes!",
            text: exception,
          });
        });
    },
  },
};
</script>

<style scoped>
</style>