<script setup>
import {ref, reactive} from "vue";
import {AddSetting} from "@/api/setting.js";
import {message} from "ant-design-vue";
import {UploadFile} from "@/api/sysFile.js";
// import { uploadOutlined, } from '@ant-design/icons-vue';


const fileList = ref([ ]);
const gap = ref(20)

const baseSetting = reactive({
  webName: '12',
  loginName: '',
  copyright: '',
  backLogoUrl: '',
  loginLogoUrl: '',
})

const save = () => {

  let data = {key: 'baseSetting', value: JSON.stringify(toRaw(baseSetting))};
  AddSetting(data).then((res) => {
    let data = res.data;
    if (data.code === 200){
      message.success("保存成功")
    }
    else
      message.error(data.message);
  })
}

const handleChange = info => {
  if (info.file.status !== 'uploading') {
    // console.log(info.file, info.fileList);
  }
  if (info.file.status === 'success') {
    message.success(`${info.file.name} file uploaded successfully`);
  } else if (info.file.status === 'error') {
    message.error(`${info.file.name} file upload failed.`);
  }
};
const upload = (e) => {

  const formData = new FormData();
  formData.append('file', e.file);
  UploadFile(formData).then((res) => {
    if (res.code === 200) {
      baseSetting.backLogoUrl = res.data
      fileList.value = fileList.value.filter( item => item.uid === e.uid)
      fileList.value.push({
        uid: e.uid,
        name: e.file.name,
        status: 'success',
        url: baseSetting.backLogoUrl,
        thumbUrl: baseSetting.backLogoUrl,
      })
    }
  })

}

</script>

<template>

  <a-flex :gap="gap" vertical style="padding: 50px;">
    <a-flex style="width: 400px;" :gap="gap">
      <span style="width: 120px;font-weight: bold;">网站名称：</span>
      <a-input v-model:value="baseSetting.webName"  placeholder="请输入网站名称" />
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
          :customRequest="upload"
          @change="handleChange"
      >
        <a-button>
          <upload-outlined></upload-outlined>
          Upload
        </a-button>
      </a-upload>
    </a-flex>
<!--   <a-flex style="width: 400px;" :gap="gap">
      <span style="width: 120px;font-weight: bold;">后台LOGO：</span>
         <a-upload
             v-model:file-list="fileList"
             list-type="picture"
             action="http://localhost:5182/api/SysFileInfo/uploadFile"
             :preview-file="previewFile"
             :headers="headers"
             @change="handleChange"
         >
           <a-button>
             <upload-outlined></upload-outlined>
             Upload
           </a-button>
         </a-upload>
    </a-flex>-->


    <a-button type="primary" style="width: 200px;margin-top: 100px;" @click="save"  >保存</a-button>

  </a-flex>

</template>

<style scoped>



</style>