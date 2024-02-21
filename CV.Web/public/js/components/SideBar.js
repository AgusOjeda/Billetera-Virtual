export const sideBar = () => {
    return `
        <sidebar class="bg-bgSidebar text-left">
            <p class="text-brand font-GilroyExtraBold text-4xl mb-20 mt-14 ml-3 p-1">GStick</p>
            <section class="text-bgPrimary hover:text-gray-400 p-1 m-3 mt-3 rounded-lg" id="side-home">Home</section>
            <section class="text-bgPrimary hover:text-gray-400 p-1 m-3  mt-3 rounded-lg" id="side-transf">Transferencias</section>
            <section class="text-bgPrimary hover:text-gray-400 p-1 m-3 mt-3 rounded-lg" id="side-movs">Movimientos</section>
            <section class="text-bgPrimary hover:text-gray-400 p-1 m-3 mt-3 rounded-lg" id="side-perfil">Perfil</section>
            
            <hr class="text-bgPrimary mt-32 mb-10 mx-3">

            <div class="grid mx-3 grid-rows-2">
                <div class="place-self-center">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="white" viewBox="0 0 24 24" stroke-width="1.5" stroke="stroke-light" class="w-10">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M15.75 6a3.75 3.75 0 11-7.5 0 3.75 3.75 0 017.5 0zM4.501 20.118a7.5 7.5 0 0114.998 0A17.933 17.933 0 0112 21.75c-2.676 0-5.216-.584-7.499-1.632z" />
                    </svg>
                </div>
                <div class="text-bgPrimary place-self-center" id="customerName"></div>
                <div class="text-red-500 hover:text-red-700 place-self-center text-sm" id="btn-logout">Cerrar sesión</div>
            </div>
        </sidebar>
`
}