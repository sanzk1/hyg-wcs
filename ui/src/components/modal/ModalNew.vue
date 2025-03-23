<script setup>
import {computed, onMounted, ref, watch, watchEffect, defineProps} from "vue";
import {useDraggable} from "@vueuse/core";

onMounted(() =>{

})

const props = defineProps({
  title: String,
  cancelText: String,
  okText: String,
  show: false,
  cancel: Function,
  handleOk: Function
})

const modalTitleRef = ref(null);
const { x, y, isDragging } = useDraggable(modalTitleRef);

const startX = ref(0);
const startY = ref(0);
const startedDrag = ref(false);
const transformX = ref(0);
const transformY = ref(0);
const preTransformX = ref(0);
const preTransformY = ref(0);
const dragRect = ref({
  left: 0,
  right: 0,
  top: 0,
  bottom: 0,
});
watch([x, y], () => {
  if (!startedDrag.value) {
    startX.value = x.value;
    startY.value = y.value;
    const bodyRect = document.body.getBoundingClientRect();
    const titleRect = modalTitleRef.value.getBoundingClientRect();
    dragRect.value.right = bodyRect.width - titleRect.width;
    dragRect.value.bottom = bodyRect.height - titleRect.height;
    preTransformX.value = transformX.value;
    preTransformY.value = transformY.value;
  }
  startedDrag.value = true;
});
watch(isDragging, () => {
  if (!isDragging) {
    startedDrag.value = false;
  }
});
watchEffect(() => {
  if (startedDrag.value) {
    transformX.value =
        preTransformX.value +
        Math.min(Math.max(dragRect.value.left, x.value), dragRect.value.right) -
        startX.value;
    transformY.value =
        preTransformY.value +
        Math.min(Math.max(dragRect.value.top, y.value), dragRect.value.bottom) -
        startY.value;
  }
});
const transformStyle = computed(() => {
  return {
    transform: `translate(${transformX.value}px, ${transformY.value}px)`,
  };
});

</script>

<template>
  <a-modal ref="modalRef" :open="props.show" :wrap-style="{ overflow: 'hidden' }" @ok="props.handleOk" @cancel="props.cancel">
    <slot name="content"></slot>
    <template #title>
      <div ref="modalTitleRef" style="width: 100%; cursor: move">{{props.title}}</div>
    </template>
    <template #modalRender="{ originVNode }">
      <div :style="transformStyle">
        <component :is="originVNode" />
      </div>
    </template>
  </a-modal>
</template>

<style scoped>

</style>