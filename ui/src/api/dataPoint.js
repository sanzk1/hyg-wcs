import {request} from "@/utils/http/index.js";

export const GetOpcUaList = (data)=>{
    return request({
        url: '/OpcUaDataPoint/getList',
        method: 'post',
        data: data
    })
}
export const AddOrUpdateOpcUa = (data)=>{
    return request({
        url: '/OpcUaDataPoint/addOrUpdate',
        method: 'post',
        data: data
    })
}
export const DelOpcUa = (data)=>{
    return request({
        url: '/OpcUaDataPoint/del',
        method: 'delete',
        data: data
    })
}
export const ReadByIdOpcUa = (data)=>{
    return request({
        url: '/OpcUaDataPoint/readById/' + data,
        method: 'get',
    })
}
export const WriteByIdOpcUa = (data)=>{
    return request({
        url: '/OpcUaDataPoint/writeById',
        method: 'post',
        data: data
    })
}

export const GetS7List = (data)=>{
    return request({
        url: '/S7DataPoint/getList',
        method: 'post',
        data: data
    })
}

export const DelS7 = (data) => {
    return request({
        url: '/S7DataPoint/del',
        method: 'delete',
        data: data
    })
}
export const AddS7 = (data) => {
    return request({
        url: '/S7DataPoint/add',
        method: 'post',
        data: data
    })
}
export const UpdateS7 = (data) => {
    return request({
        url: '/S7DataPoint/update',
        method: 'put',
        data: data
    })
}

export const GetS7Details = (data) => {
    return request({
        url: '/S7DataPoint/getId/' + data,
        method: 'get'
    })
}
export const GetS7DataType = () => {
    return request({
        url: '/S7DataPoint/getVarType',
        method: 'get'
    })
}
export const GetS7CpuType = () => {
    return request({
        url: '/S7DataPoint/getCpuType',
        method: 'get'
    })
}
export const ReadS7 = (id) => {
    return request({
        url: '/S7DataPoint/readS7/' + id,
        method: 'get'
    })
}
export const WriteS7 = (data) => {
    return request({
        url: '/S7DataPoint/writeS7/' ,
        method: 'post',
        params: data
    })
}
export const GetProtocolList = (data) => {
    return request({
        url: '/ProtocolLog/getList/' ,
        method: 'post',
        data: data
    })
}
export const DelLog = (data) => {
    return request({
        url: '/ProtocolLog/del',
        method: 'delete',
        data: data
    })
}
export const ExportLogExcel = (data) => {
    return request({
        responseType: 'blob',
        url: 'ProtocolLog/exportExcel',
        method: 'post',
        data: data
    })
}

