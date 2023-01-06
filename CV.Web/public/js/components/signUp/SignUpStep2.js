export const SignUpStep2 = (data) => `
<section
        class="flex flex-col gap-2 p-2 justify-center items-center mt-24"
      >
        <div
          class="p-8 gap-2 bg-white bg-opacity-10 rounded-2xl shadow-5xl border border-opacity-30 backdrop-filter backdrop-blur-sm mt-12"
        >
          <h4 class="font-semibold text-textColor text-xl">
            Por favor revisa tu email
          </h4>
          <p class="text-base text-gray-500 mt-2 font-medium">
            Hemos enviado un codigo a <strong>${data.email}</strong>
          </p>
          <form>
            <div class="flex flex-row gap-4 justify-between mt-4 mb-4">
              <input
                name="code"
                class="code-input text-center w-12 text-2xl p-4 rounded-md border border-gray-300 font-bold bg-inherit"
                required
              />
              <input
                name="code"
                class="code-input text-center w-12 text-2xl p-2 rounded-md border border-gray-300 font-bold bg-inherit"
                required
              />
              <input
                name="code"
                class="code-input text-center w-12 text-2xl p-2 rounded-md border border-gray-300 font-bold bg-inherit"
                required
              />
              <input
                name="code"
                class="code-input text-center w-12 text-2xl p-2 rounded-md border border-gray-300 font-bold bg-inherit"
                required
              />
              <input
                name="code"
                class="code-input text-center w-12 text-2xl p-2 rounded-md border border-gray-300 font-bold bg-inherit"
                required
              />
              <input
                name="code"
                class="code-input text-center w-12 text-2xl p-2 rounded-md border border-gray-300 font-bold bg-inherit"
                required
              />
              <input
                name="code"
                class="code-input text-center w-12 text-2xl p-2 rounded-md border border-gray-300 font-bold bg-inherit"
                required
              />
              <input
                name="code"
                class="code-input text-center w-12 text-2xl p-2 rounded-md border border-gray-300 font-bold bg-inherit"
                required
              />
            </div>
          </form>
          <p id="wrongCode" class="text-red text-sm font-bold"></p>
          <div class="grid justify-items-end">
            <button
              id="verifyBtn"
              type="submit"
              class="bg-secondary px-12 py-2 rounded-2xl text-white font-medium mt-2 w-fit"
            >
              Verificar
            </button>
          </div>
        </div>
      </section>
`
