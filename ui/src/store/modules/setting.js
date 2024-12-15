import { defineStore } from 'pinia'
import {GetSetting} from "@/api/setting.js";

export const useSettingStore = defineStore('setting', {
    state: () => ({
        baseSetting: null,
    }),
    getters: {
        webName(){
            return this.baseSetting?.webName
        },
        loginName(){
            return this.baseSetting?.loginName
        },
        copyright(){
            return this.baseSetting?.copyright
        },
        backLogoUrl(){
            return this.baseSetting?.backLogoUrl
        },
        loginLogoUrl(){
            return this.baseSetting?.loginLogoUrl
        },


    },
    actions: {
      async getSetting(){
          try {
             let {data} = await GetSetting({key:'baseSetting'})
              this.baseSetting = JSON.parse(data.value);

              return Promise.resolve(data.value)
          }catch (error){
              return Promise.reject(error)
          }
      },

    },

    resetSetting() {
        this.$reset()
    },
    persist: {
        key: 'vue-antDesign-admin-setting',
    },

})