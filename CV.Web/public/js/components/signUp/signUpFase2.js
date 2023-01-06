export const SignUpFase2 = () => `
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
                    class="w-28 h-2 bg-[#EFF0F7] mx-auto rounded-full text-lg flex items-center"
                  ></div>
                  <div class="absolute -top-0 w-14 h-2 bg-secondary mx-auto rounded-full text-lg flex items-center"></div>
                </div>
                <div
                  class="w-10 h-10 bg-[#638fff] mx-auto rounded-full text-lg text-secondary flex items-center"
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
<h2 class="text-textColor text-2xl">Domicilio</h1>
          <p class="text-gray-300 text-xs"></p>
          <form id="form" class="grid grid-cols-2 gap-4">
            <div class="relative mt-8">
              <input
                type="text"
                id="street"
                class="peer w-full border-0 focus:outline-none focus:ring-0 bg-inherit border-b py-1 focus:border-purple-600 focus:border-b-2 transition-colors placeholder-transparent"
                placeholder="Calle"
                minlength="3"
                maxlength="50"
                required
              />
              <label
                id="street-label"
                for="street"
                class="absolute left-0 -top-4 text-purple-600 cursor-text peer-focus:-top-4 peer-placeholder-shown:top-1 peer-placeholder-shown:text-gray-600 transition-all"
                >Calle</label>
            </div>
            <div class="relative mt-8">
              <input
                type="number"
                id="number"
                class="peer w-full border-0 focus:outline-none focus:ring-0 bg-inherit border-b py-1 focus:border-purple-600 focus:border-b-2 transition-colors placeholder-transparent"
                placeholder="Numero"
                minlength="1"
                maxlength="5"
                required
              />
              <label
                id="number-label"
                for="number"
                class="absolute left-0 -top-4 text-purple-600 cursor-text peer-focus:-top-4 peer-placeholder-shown:top-1 peer-placeholder-shown:text-gray-600 transition-all"
                >Numero</label>
            </div>
            <div class="relative mt-8">
              <input
                type="text"
                id="location"
                class="peer w-full border-0 focus:outline-none focus:ring-0 bg-inherit border-b py-1 focus:border-purple-600 focus:border-b-2 transition-colors placeholder-transparent"
                placeholder="Localidad"
                minlength="3"
                maxlength="50"
                required
              />
              <label
                id="location-label"
                for="location"
                class="absolute left-0 -top-4 text-purple-600 cursor-text peer-focus:-top-4 peer-placeholder-shown:top-1 peer-placeholder-shown:text-gray-600 transition-all"
                >Localidad</label>
            </div>
            <div class="relative mt-8">
              <label
                id="province-label"
                for="province-select"
                class="absolute left-0 -top-4 text-purple-600 cursor-text transition-all"
                >Provincia</label>
                <select name="province" id="province-select" class="peer w-full border-0 focus:outline-none focus:ring-0 bg-inherit border-b py-1 focus:border-purple-600 focus:border-b-2 transition-colors placeholder-transparent">
                  <option value="Buenos Aires">Buenos Aires</option>
                  <option value="Buenos Aires Capital">Buenos Aires Capital</option>
                  <option value="Catamarca">Catamarca</option>
                  <option value="Chaco">Chaco</option>
                  <option value="Chubut">Chubut</option>
                  <option value="Cordoba">Cordoba</option>
                  <option value="Corrientes">Corrientes</option>
                  <option value="Entre Rios">Entre Rios</option>
                  <option value="Formosa">Formosa</option>
                  <option value="Jujuy">Jujuy</option>
                  <option value="La Pampa">La Pampa</option>
                  <option value="La Rioja">La Rioja</option>
                  <option value="Mendoza">Mendoza</option>
                  <option value="Misiones">Misiones</option>
                  <option value="Neuquen">Neuquen</option>
                  <option value="Rio Negro">Rio Negro</option>
                  <option value="Salta">Salta</option>
                  <option value="San Juan">San Juan</option>
                  <option value="San Luis">San Luis</option>
                  <option value="Santa Cruz">Santa Cruz</option>
                  <option value="Santa Fe">Santa Fe</option>
                  <option value="Santiago del Estero">Santiago del Estero</option>
                  <option value="Tierra del Fuego">Tierra del Fuego</option>
                  <option value="Tucuman">Tucuman</option>
              </select>
            </div>
            <button
              class="w-full p-2 text-white mt-8 rounded-3xl bg-[#1D5BFB] font-semibold text-xl col-span-2"
              id="btnAddress"
              type="submit"
            >
              Continuar
            </button>
          </form>
        </div>
        </div>
`
