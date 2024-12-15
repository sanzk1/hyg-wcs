import {request} from "@/utils/http/index.js";


export const AddSetting = (data) =>{
    return request({
        url: '/SysSetting/addOrUpdate',
        method: 'POST',
        data: data
    })
}
export const GetSetting = (data) =>{
    return request({
        url: '/SysSetting/get',
        method: 'GET',
        params: data
    })
}
