<script setup>
import {reactive, ref, onMounted, defineProps} from 'vue';
import {useRouter} from "vue-router";
import {AddS7, GetS7CpuType, GetS7DataType, GetS7Details, ReadS7, UpdateS7, WriteS7} from "@/api/dataPoint.js";
import {message} from "ant-design-vue";

defineOptions({
  name : 's7Edit'
})

const props = defineProps({
  id: ''
})

const disabled = ref(true)
const point = reactive({
  id : undefined,
  name : '',
  category : '',
  ip : '',
  port : 102,
  cpuType : 'S71200',
  rack : 0,
  slot : 1,
  db : 0,
  startAddress : 1,
  dataType : '',
  length : 1,
  operate : 0,
  // bit : 0,
  remark : '',
  hardwareType : 0,

})


onMounted(() => {
  if (props.id != undefined && props.id != 0 ){
    disabled.value = true
    GetS7Details(props.id).then(res => {
      let data = res.data

      point.id = data.id
      point.name = data.name
      point.category = data.category
      point.ip = data.ip
      point.port = data.port
      point.cpuType = data.cpuType
      point.rack = data.rack
      point.slot = data.slot
      point.db = data.db
      point.startAddress = data.startAddress
      point.dataType = data.dataType
      point.length = data.length
      point.operate = data.operate
      point.remark = data.remark
    })
  }else {
    disabled.value = false
  }

  GetS7DataType().then(res => {
    if (res.code === 200){
      res.data.forEach(item => {
        varTypes.value.push({label: item, value: item})
      })

    }
  })
  GetS7CpuType().then(res => {
    if (res.code === 200){
      res.data.forEach(item => {
        cpuTypes.value.push({label: item, value: item})
      })

    }
  })

})
const route = useRouter()
const edit = () => {
  disabled.value = false

}
const save = () => {
  disabled.value = true
  if (point.id === undefined || point.id === 0 ){
    AddS7(point).then(res => {
      if (res.code === 200){
        message.success("保存成功");
      }else {
        message.error(res.data.msg)
      }
    })
  }else {
    UpdateS7(point).then(res => {
      if (res.code === 200){
        message.success("保存成功");
      }else {
        message.error(res.msg)
      }
    })
  }

}
const cancel = () => {
  route.go(-1)
}
const varTypes = ref([])
const cpuTypes = ref([])

const readData = () => {
  loadingR.value = true
  ReadS7(point.id).then(res => {
    if (res.code === 200){
      valueR.value = res.data
    }
    loadingR.value = false
  })

}
const loadingR = ref(false)
const loadingW = ref(false)
const valueR = ref({})
const writeData = () => {
  loadingW.value = true
  let data = {id: point.id, value: valueW.value}
  WriteS7(data).then(res => {
    if (res.code === 200){
      msgW.value = res.data.msg
      if (!res.data.state){
        message.error("写入失败")
      }else {
        message.success("写入成功")
      }
    }
    loadingW.value = false

  })
}
const valueW = ref('')
const msgW = ref('')
const Refresh = () => {
  location.reload()

}
</script>

<template>

  <a-card style="margin-top: 10px;height: 75vh;">

    <a-flex  gap="middle"   >
      <a-button  @click="Refresh">刷新</a-button>
      <a-button  @click="cancel">取消</a-button>
      <a-button type="primary"  @click="edit">编辑</a-button>
      <a-button type="primary" @click="save" :disabled="disabled" >保存</a-button>

    </a-flex>

    <a-flex style="padding: 10px;"  gap="middle"  wrap="wrap" >
      <a-flex class="gutter-box" vertical >
        <span style="font-weight: bold;">名称</span>
        <a-input placeholder="请输入名称" v-model:value="point.name" :disabled="disabled"  />
      </a-flex>
      <br/>
      <a-flex class="gutter-box"  vertical >
        <span style="font-weight: bold;">类别</span>
        <a-input placeholder="请输入类别" v-model:value="point.category"  :disabled="disabled"  />
      </a-flex>
      <br/>
      <a-flex class="gutter-box"  vertical >
        <span style="font-weight: bold;">IP</span>
        <a-input placeholder="请输入IP" v-model:value="point.ip" :disabled="disabled"  />
      </a-flex>
      <br/>
      <a-flex class="gutter-box"  vertical >
        <span style="font-weight: bold;">端口</span>
        <a-input placeholder="请输入端口"  v-model:value="point.port" :disabled="disabled"  />
      </a-flex>
      <a-flex class="gutter-box"  vertical >
        <span style="font-weight: bold;">芯片型号</span>
        <a-select
            v-model:value="point.cpuType"
            size="Middle"
            :options="cpuTypes"
            :disabled="disabled"
        ></a-select>
      </a-flex>

      <br/>
      <a-flex class="gutter-box" vertical >
        <span style="font-weight: bold;">机架号</span>
        <a-input placeholder="请输入IP" v-model:value="point.rack"  :disabled="disabled"  />
      </a-flex>
      <br/>
      <a-flex class="gutter-box"  vertical >
        <span style="font-weight: bold;">slot</span>
        <a-input placeholder="请输入slot" v-model:value="point.slot" :disabled="disabled"   />
      </a-flex>
      <br/>
      <a-flex class="gutter-box"  vertical >
        <span style="font-weight: bold;">DB</span>
        <a-input placeholder="请输入db" v-model:value="point.db"  :disabled="disabled"  />
      </a-flex>
      <br/>
      <a-flex class="gutter-box"  vertical >
        <span style="font-weight: bold;">偏移量</span>
        <a-input placeholder="请输入偏移量" v-model:value="point.startAddress" :disabled="disabled"  />
      </a-flex>
      <br/>
      <a-flex class="gutter-box"  vertical >
        <span style="font-weight: bold;">数据类型</span>
        <a-select
            v-model:value="point.dataType"
            size="Middle"
            :options="varTypes"
            :disabled="disabled"
        ></a-select>
      </a-flex>
      <br/>
      <a-flex class="gutter-box"  vertical >
        <span style="font-weight: bold;">长度</span>
        <a-input-number placeholder="请输入长度" style="width: 180px;" v-model:value="point.length" :disabled="disabled"  />
      </a-flex>
      <br/>
      <a-flex class="gutter-box"  vertical >
        <span style="font-weight: bold;">操作</span>
        <a-select
            ref="select"
            v-model:value="point.operate"
            :disabled="disabled"
            style="width: 180px;"
        >
          <a-select-option :value="0">Read</a-select-option>
          <a-select-option :value="1">Write</a-select-option>
        </a-select>
      </a-flex>
      <br/>

      <br/>
      <a-flex class="gutter-box"  vertical >
        <span style="font-weight: bold;">备注</span>
        <a-input placeholder="请输入备注" style="width: 300px;" v-model:value="point.remark"  :disabled="disabled"  />
      </a-flex>

    </a-flex>

    <a-flex style="margin-top: 10%;" horizontal>
      <a-flex >
        <a-button type="primary" :loading="loadingR" @click="readData">
          读取
        </a-button>
        <span class="label-b">值：</span>
        <a-input style="width: 200px;" v-model:value="valueR.value"  :disabled="disabled"  />
        <span class="error"  >{{ valueR.msg}}</span>
      </a-flex>
      <a-flex style="margin-left: 250px;">
        <a-button type="primary" :loading="loadingW"  @click="writeData">
          写入
        </a-button>
        <span class="label-b">值：</span>
        <a-input style="width: 200px;" v-model:value="valueW"   />
        <span class="error" >{{ msgW }}</span>
      </a-flex>

    </a-flex>

  </a-card>

</template>

<style scoped>
.gutter-box{
  width: 180px;
}
.error{
  font-weight: bold;width: 200px;color: red;margin-left: 20px;
}
.label-b{
  font-weight: bold;width: 50px;line-height: 30px;margin-left: 20px;
}
</style>