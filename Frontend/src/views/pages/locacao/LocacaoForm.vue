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

      <vs-row v-if="id">
        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <vs-input
            label="Data de locação"
            v-model="dataLocacao"
            class="w-full"
          />
        </vs-col>

        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <vs-input
            label="Data de devolução"
            v-model="dataDevolucao"
            class="w-full"
          />
        </vs-col>
      </vs-row>

      <vs-row vs-type="flex" vs-justify="flex-end" class="mt-6 pr-2">
        <vs-button v-if="!id" @click="submit" class="font-semibold" type="filled">Cadastrar</vs-button>

        <vs-button v-if="id" @click="submit" class="font-semibold" type="filled">Editar</vs-button>
      </vs-row>
    </div>
  </vs-card>
</template>

<script>
import vSelect from "vue-select";
import api_cliente from "@/api/api_cliente";
import api_filme from "@/api/api_filme";
import api_locacao from "@/api/api_locacao";
import utils from "@/assets/utils";

import {
  mascara_data,
  formatar_data,
  formatar_data_ao_contrario
} from "@/assets/utils/mask";

export default {
  components: {
    "v-select": vSelect,
  },

  data() {
    return {
      id: null,

      selectCliente: null,
      optionsClientes: [],

      selectFilme: null,
      optionsFilmes: [],

      dataLocacao: null,

      dataDevolucao: null
    };
  },

  created() {
    this.id = this.$route.query.id;

    if (this.id) {
      this.preencherCamposEdit();
    }

    this.getSelectClientes();
    this.getSelectFilmes();
  },

  methods: {
    submit() {
      this.$vs.loading();

      const data = {
        idCliente: this.selectCliente.id,
        idFilme: this.selectFilme.id
      };

      debugger;

      if (!this.id) {
        // SALVAR
        api_locacao
          .alugar(data)
          .then(() => {
            this.$vs.loading.close();

            this.$vs.notify({
              color: "success",
              title: "Login",
              text: "Locação cadastrada com sucesso!",
            });

            this.$router.push({ name: "locacao-list" });
          })
          .catch((error) => {
            var exception = utils.getError(error);

            this.$vs.loading.close();
            this.$vs.notify({
              color: "danger",
              title: "Erro ao cadastrar locação",
              text: exception,
            });
          });
      } else {
        // EDITAR
        data.id = parseInt(this.id);
        data.dataLocacao = formatar_data(this.dataLocacao);
        data.dataDevolucao = this.dataDevolucao ? formatar_data(this.dataDevolucao) : null;

        api_locacao
          .editar(this.id, data)
          .then(() => {
            this.$vs.loading.close();

            this.$vs.notify({
              color: "success",
              title: "Login",
              text: "Locação editado com sucesso!",
            });

            this.$router.push({ name: "locacao-list" });
          })
          .catch((error) => {
            var exception = utils.getError(error);

            this.$vs.loading.close();
            this.$vs.notify({
              color: "danger",
              title: "Erro ao editar locação",
              text: exception,
            });
          });
      }
    },

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

    preencherCamposEdit() {
      this.$vs.loading();

      api_locacao
        .getById(this.id)
        .then((response) => {
          this.$vs.loading.close();

          if (response.data) {
            this.selectCliente = {
              id: response.data.cliente.id,
              nome: response.data.cliente.nome,
            };

            this.selectFilme = {
              id: response.data.filme.id,
              titulo: response.data.filme.titulo,
            };

            this.dataLocacao = formatar_data_ao_contrario(response.data.dataLocacao);
            
            this.dataDevolucao = response.data.dataDevolucao  ? formatar_data_ao_contrario(response.data.dataDevolucao) : null;
          } else {
            this.$vs.notify({
              color: "danger",
              title: "Nenhuma locação encontrada",
            });
          }
        })
        .catch((error) => {
          var exception = utils.getError(error);

          this.$vs.loading.close();
          this.$vs.notify({
            color: "danger",
            title: "Erro ao carregar dados da locação",
            text: exception,
          });
        });
    },
  },

  watch:{
    dataLocacao(){
      var data = mascara_data(this.dataLocacao);
      if (this.dataLocacao != data) {
        this.dataLocacao = data;
      }
    },

    dataDevolucao(){
      var data = mascara_data(this.dataDevolucao);
      if (this.dataDevolucao != data) {
        this.dataDevolucao = data;
      }
    }
  }
};
</script>

<style scoped>
</style>