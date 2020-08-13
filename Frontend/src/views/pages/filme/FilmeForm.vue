<template>
  <vs-card>
    <div slot="header">
      <h3>Cadastro de filme</h3>
    </div>

    <div>
      <vs-row>
        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <vs-input
            label="Titulo"
            v-model="titulo"
            class="w-full"
            name="titulo"
            v-validate="'required'"
            :danger="errors.has('titulo')"
            :danger-text="errors.first('titulo')"
          />
        </vs-col>

        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <span class="label-campo">Classificação indicativa</span>
          <v-select
            label="descricao"
            v-model="selectClassificacao"
            :options="optionsClassificacao"
            class="display-inline-block mar-1"
          ></v-select>
        </vs-col>
      </vs-row>

      <vs-row>
        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <label for>Lançamento</label>
          <vs-switch v-model="lancamento" />
        </vs-col>
      </vs-row>

      <vs-row vs-type="flex" vs-justify="flex-end" class="mt-6 pr-2">
        <vs-button v-if="!id" @click="validar" class="font-semibold">Cadastrar</vs-button>

        <vs-button v-if="id" @click="validar" class="font-semibold" type="filled">Editar</vs-button>
      </vs-row>
    </div>
  </vs-card>
</template>

<script>
import vSelect from "vue-select";
import api_filme from "@/api/api_filme";

import { Validator } from "vee-validate";
import utils from "@/assets/utils";

const dict = {
  custom: {
    titulo: {
      required: "O titulo é obrigatório!",
    },
  },
};

Validator.localize("pt", dict);

export default {
  components: {
    "v-select": vSelect,
  },

  data() {
    return {
      id: null,

      titulo: null,

      selectClassificacao: null,
      optionsClassificacao: [
        { id: 0, descricao: "Livre" },
        { id: 10, descricao: "Dez" },
        { id: 12, descricao: "Doze" },
        { id: 14, descricao: "Quatorze" },
        { id: 16, descricao: "Dezesseis" },
        { id: 18, descricao: "Dezoito" },
      ],

      lancamento: false,
    };
  },

  created() {
    this.id = this.$route.query.id;

    if (this.id) {
      this.preencherCamposEdit();
    }
  },

  methods: {
    async validar() {
      var valido = 0;

      var result = await utils.validar(this.$validator);

      if (!result) valido++;

      if (valido == 0) {
        this.submit();
      } else {
        this.$vs.notify({
          color: "danger",
          title: "Erro",
          text: "Algum dos campos está com erro, verifique e tente novamente",
        });
      }
    },

    submit() {
      this.$vs.loading();

      debugger;

      const data = {
        titulo: this.titulo,
        classificacaoIndicativa: this.selectClassificacao.id,
        lancamento: this.lancamento,
      };

      if (!this.id) {
        // SALVAR
        api_filme
          .salvar(data)
          .then(() => {
            this.$vs.loading.close();

            this.$vs.notify({
              color: "success",
              title: "Login",
              text: "Filme cadastrado com sucesso!",
            });

            this.$router.push({ name: "filme-list" });
          })
          .catch((error) => {
            var exception = utils.getError(error);

            this.$vs.loading.close();
            this.$vs.notify({
              color: "danger",
              title: "Erro ao cadastrar filme",
              text: exception,
            });
          });
      } else {
        // EDITAR
        data.id = parseInt(this.id);

        api_filme
          .editar(this.id, data)
          .then(() => {
            this.$vs.loading.close();

            this.$vs.notify({
              color: "success",
              title: "Login",
              text: "Filme editado com sucesso!",
            });

            this.$router.push({ name: "filme-list" });
          })
          .catch((error) => {
            var exception = utils.getError(error);

            this.$vs.loading.close();
            this.$vs.notify({
              color: "danger",
              title: "Erro ao editar filme",
              text: exception,
            });
          });
      }
    },

    preencherCamposEdit() {
      this.$vs.loading();

      api_filme
        .getById(this.id)
        .then((response) => {
          this.$vs.loading.close();

          if (response.data) {
            this.titulo = response.data.titulo;

            var select = this.optionsClassificacao.filter(function (
              item
            ) {
              return item.id == response.data.classificacaoIndicativa;
            });

            this.selectClassificacao = select[0];

            this.lancamento = response.data.lancamento;
          } else {
            this.$vs.notify({
              color: "danger",
              title: "Nenhum filme encontrado",
            });
          }
        })
        .catch((error) => {
          var exception = utils.getError(error);

          this.$vs.loading.close();
          this.$vs.notify({
            color: "danger",
            title: "Erro ao carregar dados do filme",
            text: exception,
          });
        });
    },
  },
};
</script>

<style scoped>
</style>