<script setup>

import { ref } from 'vue';
import BaseSetting from "@/components/setting/BaseSetting.vue";

defineOptions({
  name : 'setting'
})

const tabPosition = ref('top');
const activeKey = ref('1');

</script>

<template>
  <a-card>

    <a-tabs v-model:activeKey="activeKey" :tab-position="tabPosition" animated>
      <a-tab-pane key="1" tab="基础配置"><BaseSetting /></a-tab-pane>
      <a-tab-pane key="2" tab="基础配置">Content of Tab 2</a-tab-pane>
      <a-tab-pane key="3" tab="基础配置">Content of Tab 3</a-tab-pane>
    </a-tabs>

  </a-card>

</template>

<style scoped>


</style>