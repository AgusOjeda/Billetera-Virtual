export const SignUpStep3 = () => `
<section class="w-full border-b border-gray-200">
        <div class="Progres flex gap-4 p-8">
          <i class="bx bx-arrow-back text-2xl self-center text-textColor"></i>
          <div class="flex gap-4">
            <h3 class="font-bold self-center text-2xl text-textColor">
              Verificacion de Cuenta
            </h3>
            <p
              id="state"
              class="bg-[#e9e8fc] text-sm text-[#6e6eff] self-center p-1 rounded-md"
            >
              En progeso
            </p>
          </div>
        </div>
        </section>
        <section
        id="step"
        class="w-full bg-white bg-opacity-10 rounded-2xl shadow-5xl border border-opacity-30 backdrop-filter backdrop-blur-sm mt-12"
        >
        <div class="w-full h-full p-6">
          <div class="max-w-2xl mx-auto my-4 border-b-2 pb-4">
              <div class="flex justify-center items-center">
                <div
                  class="w-10 h-10 bg-[#638fff] mx-auto rounded-full text-lg text-secondary flex items-center"
                >
                  <span class="text-white font-bold text-center w-full">1</span>
                </div>
                <!-- divisor -->
                <div class="relative ml-5 mr-5">
                  <div
                    class="w-28 h-2 bg-[#EFF0F7] mx-auto rounded-full text-lg flex items-center"
                  ></div>
                </div>
                <div
                  class="w-10 h-10 bg-[#EFF0F7] mx-auto rounded-full text-lg text-secondary flex items-center"
                >
                  <span class="text-[#6F6C90] font-bold text-center w-full"
                    >2</span
                  >
                </div>
                <!-- divisor -->
                <div class="relative ml-5 mr-5">
                  <div
                    class="w-28 h-2 bg-[#EFF0F7] mx-auto rounded-full text-lg flex items-center"
                  ></div>
                </div>
                <div
                  class="w-10 h-10 bg-[#EFF0F7] mx-auto rounded-full text-lg text-secondary flex items-center"
                >
                  <span class="text-[#6F6C90] font-bold text-center w-full"
                    >3</span
                  >
                </div>
              </div>
            </div>
        </div>
        <div id="inputdata" class="w-full h-full px-6 pb-6">
          <h2 class="text-textColor text-2xl">Datos Personales</h1>
          <p class="text-gray-300 text-xs">Por favor ingrese sus datos para continuar con el registro</p>
          <form id="form" class="grid grid-cols-2 gap-4">
            <div class="relative mt-8">
              <input
                type="text"
                id="firstName"
                class="peer w-full border-0 focus:outline-none focus:ring-0 bg-inherit border-b py-1 focus:border-purple-600 focus:border-b-2 transition-colors placeholder-transparent"
                placeholder="Nombre"
                minlength="3"
                maxlength="20"
                required
              />
              <label
                id="firstName-label"
                for="firstName"
                class="absolute left-0 -top-4 text-purple-600 cursor-text peer-focus:-top-4 peer-placeholder-shown:top-1 peer-placeholder-shown:text-gray-600 transition-all"
                >Nombre</label>
            </div>
            <div class="relative mt-8">
              <input
                type="text"
                id="lastName"
                class="peer w-full border-0 focus:outline-none focus:ring-0 bg-inherit border-b py-1 focus:border-purple-600 focus:border-b-2 transition-colors placeholder-transparent"
                placeholder="Apellido"
                minlength="3"
                maxlength="20"
                required
              />
              <label
                id="lastName-label"
                for="lastName"
                class="absolute left-0 -top-4 text-purple-600 cursor-text peer-focus:-top-4 peer-placeholder-shown:top-1 peer-placeholder-shown:text-gray-600 transition-all"
                >Apellido</label>
            </div>
            <div class="relative mt-8">
              <input
                type="text"
                id="dni"
                class="peer w-full border-0 focus:outline-none focus:ring-0 bg-inherit border-b py-1 focus:border-purple-600 focus:border-b-2 transition-colors placeholder-transparent"
                placeholder="DNI"
                minlength="8"
                maxlength="8"
                required
              />
              <label
                id="dni-label"
                for="dni"
                class="absolute left-0 -top-4 text-purple-600 cursor-text peer-focus:-top-4 peer-placeholder-shown:top-1 peer-placeholder-shown:text-gray-600 transition-all"
                >DNI</label>
            </div>
            <div class="relative mt-8">
              <input
                type="text"
                id="CUIL"
                class="peer w-full border-0 focus:outline-none focus:ring-0 bg-inherit border-b py-1 focus:border-purple-600 focus:border-b-2 transition-colors placeholder-transparent"
                placeholder="CUIL"
                minlength="11"
                maxlength="11"
                required
              />
              <label
                id="CUIL-label"
                for="CUIL"
                class="absolute left-0 -top-4 text-purple-600 cursor-text peer-focus:-top-4 peer-placeholder-shown:top-1 peer-placeholder-shown:text-gray-600 transition-all"
                >CUIL</label>
            </div>
            <div class="relative mt-8">
              <input
                type="tel"
                id="tel"
                class="email peer w-full border-0 focus:outline-none focus:ring-0 bg-inherit border-b py-1 focus:border-purple-600 focus:border-b-2 transition-colors placeholder-transparent"
                placeholder="Telefono"
                minlength="10"
                maxlength="10"
                required
              />
              <label
                id="tel-label"
                for="tel"
                class="absolute left-0 -top-4 text-purple-600 cursor-text peer-focus:-top-4 peer-placeholder-shown:top-1 peer-placeholder-shown:text-gray-600 transition-all"
                >Telefono</label>
            </div>
            <button
              class="w-full p-2 text-white mt-8 rounded-3xl bg-[#1D5BFB] font-semibold text-xl col-span-2"
              id="signup"
              type="submit"
            >
              Continuar
            </button>
          </form>
        </div>
        </section>
    `
