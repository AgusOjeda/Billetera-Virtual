export const SignUpFase3 = () => `
<div class="w-full h-full p-6">
<div class="max-w-2xl mx-auto my-4 border-b-2 pb-4">
    <div class="flex justify-center items-center">
      <div
        class="w-10 h-10 bg-secondary mx-auto rounded-full text-lg text-secondary flex items-center"
      >
        <span class="text-white font-bold text-center w-full">1</span>
      </div>
      <!-- divisor -->
      <div class="relative ml-5 mr-5">
        <div
          class="w-28 h-2 bg-secondary mx-auto rounded-full text-lg flex items-center"
        ></div>
        
      </div>
      <div
        class="w-10 h-10 bg-secondary mx-auto rounded-full text-lg text-secondary flex items-center"
      >
        <span class="text-white font-bold text-center w-full"
          >2</span
        >
      </div>
      <!-- divisor -->
      <div class="relative ml-5 mr-5">
        <div
          class="w-28 h-2 bg-[#EFF0F7] mx-auto rounded-full text-lg flex items-center"
        ></div>
        <div class="absolute -top-0 w-14 h-2 bg-secondary mx-auto rounded-full text-lg flex items-center"></div>
      </div>
      <div
        class="w-10 h-10 bg-[#638fff] mx-auto rounded-full text-lg text-secondary flex items-center"
      >
        <span class="text-white font-bold text-center w-full"
          >3</span
        >
      </div>
    </div>
  </div>
</div>
<div id="inputdata" class="w-full h-full px-6 pb-6">
<div class="grid grid-cols-2 justify-items-center gap-4">
  <div class="w-full p-10 rounded-lg flex flex-col items-center text-center border-2 border-dashed border-gold" id="uploadAreaFront">
    <img class="pointer-events-none" src="../images/dniFrente.png">
    <p class="font-semibold text-2xl mt-2 mb-2">Por favor suba una foto del Frente del Documento</p>
    <p class="text-sm">Arrastre y suelte o <button class="text-secondary" id="btnUploadFront">seleccione un archivo</button></p>
    <p id="supportFront" class="text-xs text-gray-400 font-medium">Soporta: JPEG, JPG, PNG</p>
    <input type="file" id="fileFront" hidden>
  </div>
  <div class="w-full p-10 rounded-lg flex flex-col items-center text-center border-2 border-dashed border-gold" id="uploadAreaBack">
    <img class="pointer-events-none" src="../images/dniDorso.png">
    <p class="font-semibold text-2xl mt-2 mb-2">Por favor suba una foto del Dorso del Documento</p>
    <p class="text-sm">Arrastre y suelte o <button class="text-secondary" id="btnUploadBack">seleccione un archivo</button></p>
    <p id="supportBack" class="text-xs text-gray-400 font-semibold">Soporta: JPEG, JPG, PNG</p>
    <input type="file" id="fileBack" hidden>
  </div>
</div>

  <button
    class="w-full p-2 text-white mt-8 rounded-3xl bg-[#1D5BFB] font-semibold text-xl col-span-2"
    id="btnContinue"
    type="submit"
  >
    Continuar
  </button>
</form>
</div>
</div>
`
