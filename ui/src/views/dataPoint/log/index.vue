<script setup>

import {reactive, ref, onMounted} from "vue";
import {message} from "ant-design-vue";
import {DelLog, ExportLogExcel, GetProtocolList,} from "@/api/dataPoint.js";
import {DeleteOutlined} from "@ant-design/icons-vue";
import dayjs from "dayjs";

defineOptions({
  name : 'protocolLog'
})

const status = ref(undefined)
const name = ref('')
const category = ref(undefined)
const oper = ref('')
const operateTime = ref([])

onMounted(() => {
  search(null)
})

const reset = () => {
  status.value = undefined
  name.value = ''
  category.value = undefined
  oper.value = undefined
  operateTime.value = []
}
const search = (page) =>{
  let query = {name:name.value, status: status.value === undefined? undefined: status.value === 'true', category: category.value,
    pageNum: paginationer.current, pageSize: paginationer.pageSize }
  if (oper.value !== ''){
    query.oper = oper.value
  }
  if (page){
    query.pageNum = page.current;
    query.pageSize = page.pageSize;
  }
  if( operateTime.value.length !== 0){

    query.startTime = dayjs(operateTime.value[0]).format('YYYY-MM-DD')
    query.endTime = dayjs(operateTime.value[1]).format('YYYY-MM-DD')
  }
  list(query)
}

const list = (query) => {
  GetProtocolList(query).then(res=>{
    if (res.code === 200){
      let result = res.data
      dataSource.value = result.rows
      paginationer.total = result.total
      paginationer.current = result.pageNum
      paginationer.pageSize = result.pageSize
    }
  })

}

const protocols = ref([
  {label: 'S7', value: 'S7'},
  {label: 'OpcUa', value: 'OpcUa'},
  {label: 'Modbus', value: 'Modbus'},
])

const paginationer = reactive({
  pageSize: 10,
  showSizeChanger: true,
  pageSizeOptions: ['10', '20', '30', '40'],
  showQuickJumper: false,
  showTotal: total => `共 ${total} 条`,
  current: 1,
  total: 0,
})

const selectRow = ref([])
const dataSource = ref([ ])
const columns= ref([
  {    title: '数据点id',    dataIndex: 'name',    key: 'name',   align: 'center' , ellipsis: true,  show:true , width: 200 },
  {    title: '协议',    dataIndex: 'category',    key: 'category',    align: 'center',  show:true, width: 150   },
  {    title: '状态',    dataIndex: 'status',    key: 'status',    align: 'center',  show:true, width: 130  },
  {    title: '消耗时间',    dataIndex: 'time',    key: 'time',    align: 'center',   show:true , width: 140 },
  {    title: '值',    dataIndex: 'value',    key: 'value',    align: 'center',    show:true , width: 150 },
  {    title: '异常原因',    dataIndex: 'reson',    key: 'reson',    align: 'left', ellipsis: true,   show:true , width: 250  },
  {    title: '操作',    dataIndex: 'oper',    key: 'oper',    align: 'center',  show:true , width: 130  },
  {    title: '开始时间',    dataIndex: 'createdTime',    key: 'createdTime',    align: 'center',   show:true, width: 150   },
  {    title: '结束时间',    dataIndex: 'endTime',    key: 'endTime',    align: 'center',  show:true , width: 150  },
])

const columnsCheck = (checked,data,index) => {
  data[index].show = checked
}


function handleResizeColumn(w, col) {
  col.width = w;
}
const rowSelection = {
  onChange: (selectedRowKeys, selectedRows) => {
    console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
    selectRow.value = selectedRows
    console.log(selectRow.value.length)
  },
  getCheckboxProps: record => ({
    disabled: record.name === 'Disabled User',
    name: record.name,
  }),
};

const handlePageChange = (page, pageSize) => {
  search(page)
}

const handleMenuClick = e => {
  console.log('click', e);
};


const del = () =>{
  if (selectRow.value.length === 0){
    message.warn("请选中删除行")
    return ;
  }
  let arr = selectRow.value.map( item => item.id);
  DelLog(arr).then(res => {
    // console.log(res)
    if (res.code === 200){
      message.success("删除成功！")
      search(null)
    }
  })
}
const exportExcel = () => {
  let query = {name:name.value, status: status.value, category: category.value,
    pageNum: paginationer.current, pageSize: paginationer.pageSize }

  if( operateTime.value.length !== 0){

    query.startTime = dayjs(operateTime.value[0]).format('YYYY-MM-DD')
    query.endTime = dayjs(operateTime.value[1]).format('YYYY-MM-DD')
  }
  ExportLogExcel(query).then(res => {
    let debug = res;
    console.log(debug)
    if (debug) {
      let elink = document.createElement('a');
      elink.download = '数据读写记录.xls';
      elink.style.display = 'none';
      let blob = new Blob([debug], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet ' });
      elink.href = URL.createObjectURL(blob);
      document.body.appendChild(elink);
      elink.click();
      document.body.removeChild(elink);
    } else {
      message.error('导出异常请联系管理员');
    }
  })

}
</script>

<template>
  <ACard>
    <a-flex :gap="50" wrap="wrap">
      <a-flex gap="middle">
        <label class="from-label">名称：</label>
        <a-input v-model:value="name" placeholder="请输入名称" />
      </a-flex>

      <a-flex gap="middle">
        <label class="from-label">协议：</label>
        <a-select
            v-model:value="category"
            size="Middle"
            :options="protocols"
            style="width: 180px;"
        ></a-select>
      </a-flex>

      <a-flex gap="middle">
        <label class="from-label">是否成功：</label>
        <a-select

            style="width: 180px;"
            v-model:value="status"
        >
          <a-select-option value="true"  >成功</a-select-option>
          <a-select-option value="false" >失败</a-select-option>
        </a-select>

      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">读写操作：</label>
        <a-select
            style="width: 180px;"
            v-model:value="oper"
        >
          <a-select-option value="Read" >读</a-select-option>
          <a-select-option value="Write" >写</a-select-option>
        </a-select>
      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">操作时间</label>
        <a-range-picker v-model:value="operateTime"  />
      </a-flex>

      <a-flex gap="large">
        <a-button type="primary" @click="search('')" block> <SearchOutlined />查询</a-button>
        <a-button block @click="reset"> <RedoOutlined />重置</a-button>
      </a-flex>

    </a-flex>


  </ACard>
  <a-card style="margin-top: 10px;">
    <a-popconfirm
        title="是否确定删除？"
        ok-text="Yes"
        cancel-text="No"
        @confirm="del"
    >
      <a-button type="primary" danger  style="margin: 10px;">
        <DeleteOutlined />
        删除
      </a-button>
    </a-popconfirm>
    <a-button type="primary"  @click="exportExcel"  style="margin: 10px;">
      导出
    </a-button>

    <a-dropdown>
      <template #overlay>
        <a-menu @click="handleMenuClick">
          <a-menu slot="overlay">
            <a-menu-item v-for="(item,index) in columns" :key="index"><a-checkbox :checked="item.show" @change="(e)=>{columnsCheck(e.target.checked,columns,index)}">{{item.title}}</a-checkbox></a-menu-item>
          </a-menu>
        </a-menu>
      </template>
      <a-button>
        筛选列
      </a-button>
    </a-dropdown>
    <a-table :data-source="dataSource" :columns="columns.filter((col,num)=>{if(col.show){return col}})" row-key="id"   size="middle" bordered
             :row-class-name="(_record, index) => (index % 2 === 1 ? 'table-striped' : null)"    :scroll="{ x: 2000, y: 800 }"
             :row-selection="rowSelection"  @change="handlePageChange"
             :pagination="paginationer"  @resizeColumn="handleResizeColumn" >
      <template #bodyCell="{ column, text, record }">
        <template  v-if="column.dataIndex === 'operate'">
          <a-tag v-if="record.operate === 0" color="#87d068">Read</a-tag>
          <a-tag v-if="record.operate === 1" color="#1677ff">Write</a-tag>
        </template>
        <template v-if="column.dataIndex === 'name'">
          <a-tooltip placement="topLeft" :title="record.name" >{{record.name}}</a-tooltip>
        </template>
        <template v-if="column.dataIndex === 'reson'">
          <a-tooltip placement="topLeft" :title="record.reson" >{{record.reson}}</a-tooltip>
        </template>
        <template v-if="column.dataIndex === 'status'">
          <a-tag v-if="record.status" color="#87d068">成功</a-tag>
          <a-tag  v-if="!record.status" color="#f50">失败</a-tag>
        </template>
        <template v-if="column.dataIndex === 'time'">
          {{ record.time }} 毫秒
        </template>

      </template>

    </a-table>

  </a-card>

</template>

<style scoped>
.ant-input {
  width: 200px;
}
.from-label{
  font-weight: bold;
  line-height: 32px;
}
</style>