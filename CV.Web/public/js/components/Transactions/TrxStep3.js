export const trxStep3 = `
<main class="grid col-start-3 col-end-11 grid-cols-1 grid-flow-row auto-rows-max place-self-center gap-1 mt-2">
            <div class="mb-3">
                <p class="text-lg mt-3"><span class="font-bold">Paso 3</span>: descargue su comprobante.</p>
            </div>
            <div class="grid shadow-sm mb-5 ml-20 mr-20 grid-cols-8 bg-zinc-300/30 h-full p-10" id="comprobante">
                <div class="p-3 text-center col-start-3 col-end-5 row-start-1 row-end-1">
                    <p class="font-bold text-2xl mb-3">Fecha</p>
                    <div class="text-gray-600 font-bold" id="date">12/12/2022</div>
                </div>
                <div class="p-3 text-center col-start-5 col-end-7 row-start-1 row-end-1">
                    <p class="font-bold text-2xl mb-3">Hora</p>
                    <div class="text-gray-600 font-bold" id="time">23:59:01</div>
                </div>
                <hr class="stroke-gray col-start-2 col-end-8">
                <div class="stroke-gray col-start-2 col-end-4 row-start-3 row-end-3">
                    <p class="font-bold text-lg">Cuenta de origen</p>
                    <p class="text-gray-600 mt-3" id="fromAccount">########</p>
                    <p class="text-gray-600 mt-3" id="nameEmisor">NOMBRE_COMPLETO_EMISOR</p>
                </div>
                <div class="stroke-gray col-start-6 col-end-8 row-start-3 row-end-3">
                    <p class="font-bold text-lg">Cuenta de destino</p>
                    <p class="text-gray-600 mt-3" id="toAliasOrCbu">###########    ALIAS_o_CBU/CVU</p>
                    <p class="text-gray-600 mt-3" id="nameReceiver">NOMBRE_COMPLETO_RECEPTOR</p>
                </div>
                <hr class="stroke-gray col-start-2 col-end-8">
                <div class="stroke-gray col-start-4 col-end-6 row-start-5 row-end-5">
                    <p class="font-bold text-3xl">Monto</p>
                    <p class="text-gray-600 text-2xl mt-3" id="amount">USD 1.000.000,00</p>
                </div>                
            </div>
            
            <div class="pt-3 w-full">
                <button id="btn-download" type="button" class="text-white inline-flex bg-blue-500 hover:bg-blue-600 focus:ring-4 focus:ring-blue-300 font-base rounded-lg px-5 py-2.5 mr-2 mb-2 ">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-6 h-6">
                <path stroke-linecap="round" stroke-linejoin="round" d="M3 16.5v2.25A2.25 2.25 0 005.25 21h13.5A2.25 2.25 0 0021 18.75V16.5M16.5 12L12 16.5m0 0L7.5 12m4.5 4.5V3" />
                </svg> 
                Descargar
                </button>
                <button id="btn-continue" type="button" class="text-white inline-flex bg-green-600 hover:bg-green-700 focus:ring-4 focus:ring-blue-300 font-base rounded-lg px-5 py-2.5 mr-2 mb-2 ">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-6 h-6">
                <path stroke-linecap="round" stroke-linejoin="round" d="M17.25 8.25L21 12m0 0l-3.75 3.75M21 12H3" />
                </svg>    
                Continuar
                </button>
            </div>
        </main>
`