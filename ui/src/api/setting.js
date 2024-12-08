import {request} from "@/utils/http/index.js";


export const AddSetting = (data) =>{
    return request({
        url: '/SysSetting/addOrUpdate',
        method: 'POST',
        params: data
    })
}