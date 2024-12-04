<script setup>
import {UserOutlined, ContainerTwoTone, MehTwoTone, BookTwoTone, ContactsTwoTone } from "@ant-design/icons-vue";

defineOptions({
  name:'console'

})
</script>

<template>

  <div style="display: flex; align-items: center;">

    <a-card class="card-console">

      <a-flex :gap="100" style="justify-content: center;">
        <a-flex>
<!--          <UserOutlined class="card-console-icon"/>-->

          <MehTwoTone class="card-console-icon"/>
        </a-flex>
        <a-flex vertical style="display: flex;">
          <span class="card-console-title">Users</span>
          <span style="font-weight: bold;">100</span>
        </a-flex>

      </a-flex>

    </a-card>
    <a-card class="card-console">
      <a-flex :gap="100" style="justify-content: center;">
        <a-flex>
          <ContactsTwoTone  class="card-console-icon"/>
        </a-flex>
        <a-flex vertical style="display: flex;">
          <span class="card-console-title">Roles</span>
          <span style="font-weight: bold;">100</span>
        </a-flex>

      </a-flex>
    </a-card>
    <a-card class="card-console">
      <a-flex :gap="100" style="justify-content: center;">
        <a-flex>
          <BookTwoTone class="card-console-icon"/>
        </a-flex>
        <a-flex vertical style="display: flex;">
          <span class="card-console-title">Menus</span>
          <span style="font-weight: bold;">100</span>
        </a-flex>

      </a-flex>
    </a-card>
    <a-card class="card-console">
      <a-flex :gap="100" style="justify-content: center;">
        <a-flex>
          <ContainerTwoTone  class="card-console-icon" />
        </a-flex>
        <a-flex vertical style="display: flex;">
          <span class="card-console-title">Operate</span>
          <span style="font-weight: bold;">100</span>
        </a-flex>

      </a-flex>
    </a-card>


  </div>


</template>

<style scoped>

.card-console{
  width: 400px;
  height: 200px;
  margin: 10px;
  align-content: center;
}
.card-console-title{
  font-size: 24px;
  font-weight: bold;
}
.card-console-icon{
  font-size: 60px;
}

</style>