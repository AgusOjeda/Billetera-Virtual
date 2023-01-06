export const trxStep2 = `
<main class="grid col-start-3 col-end-11 grid-cols-1 grid-flow-row auto-rows-max place-self-center gap-1 mt-2" id="mainStep2">
            <div class="mb-3">
                <p class="text-lg mt-3"><span class="font-bold">Paso 2</span>: revise los datos ingresados.</p>
            </div>
            <div class="grid row-span-4 shadow-sm mb-5 ml-10 mr-10 auto-cols-max w-fit px-5 bg-zinc-300/30 p-2">
                <div class="col-start-1 col-end-1 justify-self-end p-3 text-right row-start-1 row-end-1">
                    <p class="font-bold text-2xl mb-3">Origen</p>

                    <div class="font-bold">Cuenta
                        <div class="text-gray-600" id="desde-cuenta"></div>
                    </div>

                    <div class="font-bold mt-3">Monto
                        <div class="text-green-700 text-2xl" id="desde-monto">USD 1.000.000,00</div>
                    </div>
                </div>
                <div class="grid content-start row-start-1 row-end-1 col-start-2 col-end-2 mx-5">
                    <div class="circulo1 mt-6 justify-self-center"></div>
                    <hr class="hr-connect justify-self-center">
                </div>
                <div class="grid content-start row-start-2 row-end-2 col-start-2 col-end-2 mx-5">
                    <div class="circulo2 justify-self-center"></div>
                </div>
                <div class="col-start-3 col-end-3 justify-self-start row-start-2 row-end-3 text-left">
                    <div class="col-span-2 justify-self-end pb-3 pr-3">
                        <p class="font-bold text-2xl">Destino</p>
                        <div class="font-bold mt-3">Cuenta
                            <div class="text-gray-600" id="alias-o-cbu-receptor">
                            </div>
                        </div>
                        <div class="font-bold mt-3">Nombre
                            <div class="text-gray-600" id="n-completo-receptor">
                            </div>
                        </div>
                        <div class="font-bold mt-3">Dni
                            <div class="text-gray-600" id="dni-receptor"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="w-full">
                <button type="button" id="btn-transf-cancel"
                    class="text-white inline-flex bg-red-500 hover:bg-red-600 focus:ring-4 focus:ring-blue-300 font-base rounded-lg px-5 py-2.5 mr-2 mb-2">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-6 h-6">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />
                    </svg>
                    Cancelar
                </button>
                <button type="button" id="btn-transf"
                    class="text-white inline-flex bg-green-600 hover:bg-green-700 focus:ring-4 focus:ring-blue-300 font-base rounded-lg px-5 py-2.5 mr-2 mb-2">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-6 h-6">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M4.5 12.75l6 6 9-13.5" />
                    </svg>
                    Transferir
                </button>
            </div>
        </main>
`