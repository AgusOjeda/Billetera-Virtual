export const cardAccount = (params, accountId) => {
    return `
    <div class="px-3 rounded-lg grid grid-rows-4 col-span-4 shadow-sm border-neutral-200 m-3 cardCuenta">
    <div class="text-right text-gray-500 text-3xl flex justify-self-end mt-2">
        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2"
            stroke="gray" class="w-6 h-6 self-start">
            <path stroke-linecap="round" stroke-linejoin="round"
                d="M6.75 12a.75.75 0 11-1.5 0 .75.75 0 011.5 0zM12.75 12a.75.75 0 11-1.5 0 .75.75 0 011.5 0zM18.75 12a.75.75 0 11-1.5 0 .75.75 0 011.5 0z" />
        </svg>
    </div>
    <div class="font-extrabold text-3xl text-left mr-20">${params.Currency}$ ${params.Balance}</div>
    <div class="text-sm text-gray-600 text-left mt-3">${params.Cbu}</div>
    <div class="text-sm place-self-end " id="${accountId}">
        <button type="button" class="btn-transf inline-flex text-white bg-blue-900
         hover:bg-blue-800 
         focus:ring-4 focus:ring-blue-300 
         font-light rounded px-3 py-1 mb-2">
            Transferir</button>
    </div>
    </div>  
    `
}