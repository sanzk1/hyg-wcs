import {request} from "@/utils/http/index.js";


export const UploadFile = (data) =>{
    return request({
        url: '/SysFileInfo/uploadFile',
        method: 'POST',
        data: data,
        headers: {'Content-Type': 'multipart/form-data'}
    })
}