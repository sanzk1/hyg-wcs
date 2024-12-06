<script setup>
import {ref} from "vue";
import { uploadOutlined, } from '@ant-design/icons-vue';

const previewFile = async file => {
  console.log('Your upload file:', file);
  const res = await fetch('https://next.json-generator.com/api/json/get/4ytyBoLK8', {
    method: 'POST',
    body: file,
  });
  const { thumbnail } = await res.json();
  return thumbnail;
};
const fileList = ref([]);
const gap = ref(20)

</script>

<template>

  <a-flex :gap="gap" vertical style="padding: 50px;">
    <a-flex style="width: 400px;" :gap="gap">
      <span style="width: 120px;font-weight: bold;">网站名称：</span>
      <a-input  placeholder="请输入网站名称" />
    </a-flex>
    <a-flex style="width: 400px;" :gap="gap">
      <span style="width: 120px;font-weight: bold;">登录页名称：</span>
      <a-input  placeholder="请输入登录页名称" />
    </a-flex>
    <a-flex style="width: 400px;" :gap="gap">
      <span style="width: 120px;font-weight: bold;">主页版权：</span>
      <a-input  placeholder="请输入" />
    </a-flex>
    <a-flex style="width: 400px;" :gap="gap">
      <span style="width: 120px;font-weight: bold;">登录页LOGO：</span>
      <a-upload
          v-model:file-list="fileList"
          list-type="picture"
          action="//jsonplaceholder.typicode.com/posts/"
          :preview-file="previewFile"
      >
        <a-button>
          <upload-outlined></upload-outlined>
          Upload
        </a-button>
      </a-upload>
    </a-flex>
   <a-flex style="width: 400px;" :gap="gap">
      <span style="width: 120px;font-weight: bold;">后台LOGO：</span>
         <a-upload
             v-model:file-list="fileList"
             list-type="picture"
             action="//jsonplaceholder.typicode.com/posts/"
             :preview-file="previewFile"
         >
           <a-button>
             <upload-outlined></upload-outlined>
             Upload
           </a-button>
         </a-upload>
    </a-flex>


    <a-button type="primary" style="width: 200px;margin-top: 100px;"  >保存</a-button>

  </a-flex>

</template>

<style scoped>



</style>