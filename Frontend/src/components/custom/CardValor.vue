<template>
  <vs-card class="d-theme-dark-bg mx-2">
    <div slot="header">
      <vs-row vs-type="flex" vs-justify="space-between">
        <h3>{{titulo}}</h3>
        <p class="fonte-mes" v-show="mes || ano">{{mes}} - {{ano}}</p>
      </vs-row>
    </div>

    <div>
      <p class="fonte bold">{{this.formatarValor(valor)}}</p>
    </div>
  </vs-card>
</template>

<script>
export default {
  props: {
    titulo: "",
    mes: "",
    ano: "",
    valor: "",
    icone: "",
  },

  methods: {
    formatarValor() {
      if (!this.valor) return "R$ 0,00";

      var valueFormatado = this.valor.toLocaleString("pt-br", {
        style: "currency",
        currency: "BRL",
      });
      return valueFormatado;
    },
  },
};
</script>

<style scoped>
.fonte {
  font-size: 1.8rem;
}

.fonte-mes {
  font-size: 1.2rem;
}

.bold {
  font-weight: bold;
}
</style>