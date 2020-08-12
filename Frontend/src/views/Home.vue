<template>
  <div>
    <vs-row vs-w="12">
      <vs-col vs-type="flex" vs-lg="4" vs-sm="6" vs-xs="12">
        <CardValor titulo="Saldo atual" :valor="2000" />
      </vs-col>
      <vs-col vs-type="flex" vs-lg="4" vs-sm="6" vs-xs="12">
        <CardValor titulo="Despesas" mes="AGO" ano="2020" :valor="450" />
      </vs-col>
      <vs-col vs-type="flex" vs-lg="4" vs-sm="6" vs-xs="12">
        <CardValor titulo="Receitas" mes="AGO" ano="2020" :valor="1000" />
      </vs-col>
    </vs-row>
    
    <vs-row vs-w="12">
      <vs-col vs-type="flex" vs-lg="6" vs-xs="12">
        <RadialBar titulo="Receitas"/>
      </vs-col>
      <vs-col vs-type="flex" vs-lg="6" vs-xs="12">
        <RadialBar titulo="Despesas"/>
      </vs-col>
    </vs-row>
  </div>
</template>

<script>
import CardValor from "@/components/custom/CardValor.vue";
import RadialBar from "../components/custom/GraficoRadialBar";

export default {
  components: {
    CardValor,
    RadialBar
  },
};
</script>