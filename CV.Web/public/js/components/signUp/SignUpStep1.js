export const SignUpStep1 = (data) => `
<section class="grid grid-cols-2 gap-20 place-items-center mt-24">
<div class="mb-12 rounded-lg flex">
  <div class="bg-[#173b52] h-fit w-fit rounded-lg">
    <h1 class="text-9xl font-extrabold text-white text-center drop-shadow">
      G
    </h1>
  </div>
  <div class= h-fit w-fit rounded-lg">
    <h1 class="text-9xl font-extrabold text-[#173b52] text-center drop-shadow">
      Stick
    </h1>
  </div>
</div>
<div
  id="register"
  class="w-2/4 py-8 px-8 shadow-2xl bg-white bg-opacity-10 rounded-2xl shadow-5xl border border-opacity-30 backdrop-filter backdrop-blur-sm mt-12"
>
  <h2 class="text-textColor font-medium text-left text-2xl">Registrarse</h1>
  <div class="flex flex-col justify-start">
    <form id="form" class="flex flex-col mt-8">
      <div class="relative mt-8">
        <input
          type="email"
          id="email"
          class="email peer w-full border-0 focus:outline-none focus:ring-0 bg-inherit border-b py-1 focus:border-purple-600 focus:border-b-2 transition-colors placeholder-transparent"
          placeholder="Email"
          autocomplete="email"
          maxlength="50"
          required
        />
        <label
          id="email-label"
          for="email"
          class="absolute left-0 -top-4 text-purple-600 cursor-text peer-focus:-top-4 peer-placeholder-shown:top-1 peer-placeholder-shown:text-gray-600 transition-all"
          >Email</label>
      </div>
      <div class="flex flex-grow relative mt-8">
        <input
          type="password"
          id="password"
          class="peer w-full border-0 focus:outline-none focus:ring-0 bg-inherit border-b py-1 focus:border-purple-600 focus:border-b-2 transition-colors placeholder-transparent"
          placeholder="Contraseña"
          autocomplete="new-password" minlength="6"
          required
        />
        <i
          class="bx bx-show p-2.5 bg-inherit border-b border-[#6b7280] text-gray-600 cursor-pointer focus:border-b-2 focus:border-purple-600 transition-colors peer"
        ></i>
        <label
          id="password-label"
          for="password"
          class="absolute left-0 -top-4 text-purple-600 cursor-text peer-focus:-top-4 peer-placeholder-shown:top-1 peer-placeholder-shown:text-gray-600 transition-all"
          >Contraseña</label
        >
      </div>
      <div class="flex flex-grow relative mt-8">
        <input
          type="password"
          id="confirm-password"
          class="peer w-full border-0 focus:outline-none focus:ring-0 bg-inherit border-b py-1 focus:border-purple-600 focus:border-b-2 transition-colors placeholder-transparent"
          placeholder="Confirmar Contraseña"
          minlength="6"
          autocomplete="new-password"
          required
        />
        <i
          class="bx bx-show p-2.5 bg-inherit border-b border-[#6b7280] text-gray-600 cursor-pointer focus:border-b-2 focus:border-purple-600 transition-colors peer"
        ></i>
        <label
          id="password-label"
          for="confirm-password"
          class="absolute left-0 -top-4 text-purple-600 cursor-text peer-focus:-top-4 peer-placeholder-shown:top-1 peer-placeholder-shown:text-gray-600 transition-all"
          >Confirmar Contraseña</label
        >
        
      </div>
      <div class="flex justify-start mt-1">
          <p class="text-red-500 text-sm mt-2" id="error"></p>
      </div>
      <div class="flex justify-start mt-6">
            <input class="appearance-none h-4 w-4 border border-gray-300 rounded-sm bg-white checked:bg-blue-600 checked:border-blue-600 focus:outline-none transition duration-200 self-center mr-2 cursor-pointer" type="checkbox" value="true" id="terms" required>
            <label class="inline-block text-gray-800 text-sm self-center" for="terms">
              Acepto los <a href="#" class="text-purple-600">Términos y Condiciones</a>
            </label>
      </div>

      <button
        class="p-2 text-white mt-8 rounded-3xl bg-[#1D5BFB] font-semibold text-xl"
        id="signup"
        type="submit"
      >
        Crear cuenta
      </button>
    </form>
    <div class="text-left text-xs mt-4">
      <p class="mt-4 py-2  text-center">
        ¿Ya tienes una cuenta? <a class="hover:text-secondary transition-colors" href="../../../public/view/login.html">Iniciar sesión</a>
      </p>
      
    </div>
  </div>
</div>
</section>
`
