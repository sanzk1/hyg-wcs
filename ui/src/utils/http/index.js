import axios from 'axios'
import { setupInterceptors } from './interceptors'
import {message} from "ant-design-vue";

export function createAxios(options = {}) {
    const defaultOptions = {
        baseURL: '/api',
        timeout: 12000,
    }
    const service = axios.create({
        ...defaultOptions,
        ...options,
    })
    setupInterceptors(service)
    return service
}

export const request = createAxios()

export const mockRequest = createAxios({
    baseURL: '/mock-api',
})

export function download(url, params, filename, config) {
    // downloadLoadingInstance = Loading.service({ text: "正在下载数据，请稍候", spinner: "el-icon-loading", background: "rgba(0, 0, 0, 0.7)", })
    return createAxios().post(url, params, {
        transformRequest: [(params) => { return tansParams(params) }],
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        responseType: 'blob',
        ...config
    }).then(async (data) => {
        const isBlob = blobValidate(data);
        if (isBlob) {
            const blob = new Blob([data])
            saveAs(blob, filename)
        } else {
            const resText = await data.text();
            const rspObj = JSON.parse(resText);
            const errMsg = errorCode[rspObj.code] || rspObj.msg || errorCode['default']
            message.error(errMsg);
        }
        // downloadLoadingInstance.close();
    }).catch((r) => {
        console.error(r)
        message.error('下载文件出现错误，请联系管理员！')
        // downloadLoadingInstance.close();
    })
}