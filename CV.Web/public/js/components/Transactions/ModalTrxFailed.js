export const trxFailed = (msg) => {
    return `
        <main class="col-start-3 col-end-11 grid grid-rows-1 text-center place-items-center">
            <div class="bg-white shadow-sm rounded-2xl mt-2 w-6/12 place-content-center">
                <div class="space-y-8 p-8">
                    <image title="trx-fallida" class="mr-auto ml-auto" src="../img/fracaso.jpg"></image>
                    <h3 class="text-3xl text-center font-bold">Ha ocurrido un error :(</h3>
                    <p class="text-sm mt-4">${msg}</p>
                    <button id="modal-continue"
                        class="place-items-center mb-100 w-80 text-white bg-blue-600 hover:bg-blue-700 focus:ring-4 focus:ring-blue-300 font-base rounded-lg px-5 py-2"
                        type="button">Continuar</button>
                </div>
            </div>
        </main>
`
}