export const trxStep1 = `
    <main class="grid col-start-3 col-end-11 grid-cols-2 grid-flow-row auto-rows-max place-self-center gap-5">
            <div class="col-span-2">
                <p class="text-lg mt-3"><span class="font-bold">Paso 1</span>: indique origen y destino de los fondos.</p>
            </div>
            <div class="h-full shadow-sm ml-20 p-3 bg-gold/30 rounded-lg">
                <h1 class="font-bold text-xl">Origen</h1>
                <label for="cuenta-origen" class="mt-5">Cuenta</label>
                <select title="Cuenta de origen" id="cuenta-origen"
                    class="bg-gray-50 border border-gray-300 text-center p-3 rounded-lg w-full text-gray-500 text-sm">              
                </select>
                <label for="disponible-cuenta" class="mt-5">Dinero disponible en cuenta</label>
                <input type="text" name="disponible-cuenta" id="disponible-cuenta"
                    class="bg-gray-50 border border-gray-300 text-center p-3 rounded-lg w-full text-green-500 font-bold"
                    placeholder="1000,00" disabled>
                <label for="origen-monto" class="mt-5">Total a transferir</label>
                <input type="text" name="origen-monto" id="origen-monto"
                    class="bg-gray-50 border border-gray-300 text-center p-3 rounded-lg w-full text-gray-500 font-bold"
                    placeholder="1000,00">
            </div>
            <div class="h-full shadow-sm mr-20 p-3 bg-zinc-300/30 rounded-lg">
                <h1 class="font-bold text-xl">Destino</h1>
                <div class="mt-5">
                    <div class="grid grid-cols-[auto_1fr_auto] items-center">
                        <div></div>
                        <div class="mt-5">
                            <input checked id="destino-is-cbu" type="radio" value="" name="default-radio"
                                class="w-4 h-4 bg-gray-100 border-gray-300">
                            <label for="cbu" class="ml-2 text-lg font-medium text-gray-900 dark:text-gray-300">
                                CBU/CVU
                            </label>
                            <input id="destino-is-alias" type="radio" value="" name="default-radio"
                                class="ml-5 w-4 h-4 bg-gray-100 border-gray-300  dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600">
                            <label for="alias" class="ml-2 text-lg font-medium text-gray-900 dark:text-gray-300">
                                Alias
                            </label>
                        </div>
                        <div></div>
                    </div>
                    <label for="cbu-alias-destino" class="mt-5">Identificacion de cuenta</label>
                    <input type="text" name="cbu-alias-destino" id="cbu-alias-destino"
                        class="bg-gray-50 border border-gray-300 text-center p-3 text-sm rounded-lg w-full text-gray-500"
                        placeholder="Indique hacia donde va a transferir...">
                    <label for="motivo" class="mt-5">Motivo</label>
                    <input type="text" name="motivo" id="motivo"
                        class="bg-gray-50 border border-gray-300 text-center p-3 text-sm rounded-lg w-full text-gray-500"
                        placeholder="Agregue un motivo...">
                </div>
            </div>
            <div class="col-span-2">
                <button id="step1-continuar-transf" type="button"
                    class="inline-flex text-white bg-green-600 hover:bg-green-700 focus:ring-4 focus:ring-blue-300 font-base rounded-lg px-5 py-2.5 mr-2 mb-2 ">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-6 h-6">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M17.25 8.25L21 12m0 0l-3.75 3.75M21 12H3" />
                    </svg>
                    Continuar
                </button>
            </div>
        </main> 
`